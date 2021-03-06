using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTestRestfullApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestRestfullApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdministratorController(RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpPost]
        [Route("CreateRole")]
        public async Task<ActionResult<object>> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListOfRoles", "Administrator");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return model;
        }
        [HttpGet]
        [Route("ListOfRoles")]
        public async Task<ActionResult<object>> ListOfRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }
        [HttpGet]
        [Route("EditRole")]
        public async Task<ActionResult<object>> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in _userManager.Users)
            {
                model.Users.Add(user.UserName);
            }
            return model;
        }
        [HttpPost]
        [Route("EditRole")]
        public async Task<ActionResult<object>> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);


            role.Name = model.RoleName;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("ListOfRoles");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return model;

        }
        [HttpGet]
        [Route("EditUseInRole")]
        public async Task<ActionResult<object>> EditUseInRole(string roleId)
        {

            var role = await _roleManager.FindByIdAsync(roleId);

            var model = new List<UserRoleViewModel>();
            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return model;

        }
        [HttpPost]
        [Route("EditUseInRole")]
        public async Task<IActionResult> EditUseInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                    {
                        return RedirectToAction("EditRole", new { Id = role.Id });
                    }
                }
            }
            return RedirectToAction("EditRole", new { Id = role.Id });
        }

    }
}
