using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MechAppProject.Models
{
    public class WorkshopLoginModel
    {
       [Required(ErrorMessage = "Poprawny login jest wymagany")]
        public string Login { get; set; }
       [Required(ErrorMessage = "Poprawne haslo jest wymagane")]
       [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}