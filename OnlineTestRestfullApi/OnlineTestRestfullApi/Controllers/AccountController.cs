using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineTestRestfullApi.DTO;
using OnlineTestRestfullApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestRestfullApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<RegisterModel>> Register(RegisterModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                int count = _context.Users.Count();
                if (count == 1)
                {
                    return RedirectToAction("CreateRole", "Administrator");
                }
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return BadRequest();
                }
            }

            return model;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<object>> Login(LoginModel model, string returnUrl)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("GetPersons", "Persons");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
