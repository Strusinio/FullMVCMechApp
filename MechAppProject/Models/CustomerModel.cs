using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MechAppProject.Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Login jest wymagany")]
        [Display(Name = "Login: ")]
        [StringLength(20, ErrorMessage = "{0} dlugosc loginu musi się mieścić między {2} a {1} znaków.", MinimumLength = 6)]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Imię jest wymagany")]
        [Display(Name = "Imię: ")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email jest wymagany")]
        [Display(Name = "Adres Email: ")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Haslo jest wymagane")]
        [Display(Name = "Haslo: ")]
        [StringLength(20, ErrorMessage = "{0} dlugosc hasla musi się mieścić między {2} a {1} znaków.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Potwierdz haslo jest wymagane")]
        [Display(Name = "Potwierdz haslo: ")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Podane hasla sa rozne")]
        public string ConfirmPassword { get; set; }
        
    }
}