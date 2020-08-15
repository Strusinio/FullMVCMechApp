using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MechAppProject.Models
{
    public class LoginModel
    {
       [Required(ErrorMessage = "Poprawny login jest wymagany")]
       [Display(Name = "Login")]
        public string Login { get; set; }

       [Required(ErrorMessage = "Poprawne hasło jest wymagane")]
       [Display(Name = "Haslo")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}