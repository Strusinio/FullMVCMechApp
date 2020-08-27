using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MechAppProject.Models
{
    public class WorkshopServiceModel
    {
        [DisplayName("Nazwa usługi")]
        public string Title { get; set; }

        [DisplayName("Opis")]
        public string Description { get; set; }

        [Required, Range(1, 10000, ErrorMessage = "Wybież cenę w przedziale 1 - 10000")]
        [DisplayName("Cena (zł)")]
        public int? Price { get; set; }
        [Required, Range(0, 99, ErrorMessage = "Wybież cenę w przedziale 0 - 99")]
        [DisplayName("Cena (gr)")]
        public int? PriceDecimal { get; set; }
        [Required, Range(0, 24, ErrorMessage = "Wybież godzinę w przedziale 0 - 24")]
        [DisplayName("Czas trwania usługi (godz)")]
        public int? DurationInHours { get; set; }
        [Required, Range(0, 59, ErrorMessage = "Wybież minuty w przedziale 0 - 59")]
        [DisplayName("Czas trwania usługi (min)")]
        public int? DurationInMinutes { get; set; }
        public int WorkshopId { get; internal set; }
    }
}