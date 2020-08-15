using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MechAppProject.Models
{
    public class WorkshopModel
    {
        public int WorkshopId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Login jest wymagany")]
        [Display(Name = "Login: ")]
        public string Login { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nazwa warsztatu jest wymagana")]
        [Display(Name = "Nazwa Warsztatu: ")]
        public string WorkshopName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Właściciel jest wymagany")]
        [Display(Name = "Właściciel: ")]
        public string OwnerName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Adres jest wymagany")]
        [Display(Name = "Adres Email: ")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Miasto jest wymagany")]
        [Display(Name = "Miasto: ")]
        public string City { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ulica jest wymagany")]
        [Display(Name = "Ulica: ")]
        public string Street { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Numer domu jest wymagany")]
        [Display(Name = "Numer domu: ")]
        public string StreetNbr { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Kod pocztowy jest wymagany")]
        [Display(Name = "Kod pocztowy: ")]
        public string ZipCode { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Haslo jest wymagane")]
        [Display(Name = "Haslo: ")]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Potwierdz haslo jest wymagane")]
        [Display(Name = "Potwierdz haslo: ")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasla sa rozne")]
        public string ConfirmPassword { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Numer telefonu jest wymagany")]
        [Display(Name = "Numer Telefonu: ")]
        public string PhoneNbr { get; set; }

       
    }
}