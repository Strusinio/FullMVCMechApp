using MechAppProject.DBModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MechAppProject.Models
{
    public class CarModel
    {
       

        public int CarId { get; set; }
        public int CustomerId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Marka jest wymagana")]
        [Display(Name = "Marka: ")]
        public string Brand { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Marka jest wymagana")]
        [Display(Name = "Model: ")]
        public string Model { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Marka jest wymagana")]
        [Display(Name = "Typ silnika: ")]
        public string EngineType { get; set; }

        public virtual Customer Customer { get; set; }
    }
}