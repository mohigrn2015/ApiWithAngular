using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestRestfullApi.ViewModels
{
    public class RegisterModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Passwprd")]
        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
