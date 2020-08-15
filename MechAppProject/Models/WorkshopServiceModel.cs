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

        [DisplayName("Cena (zł)")]
        public int? Price { get; set; }

        [DisplayName("Cena (gł)")]
        public int? PriceDecimal { get; set; }

        [DisplayName("Czas trwania usługi (godz)")]
        public int? DurationInHours { get; set; }

        [DisplayName("Czas trwania usługi (min)")]
        public int? DurationInMinutes{ get; set; }
        public int WorkshopServices { get; internal set; }
        public int WorkshopId { get; internal set; }
    }
}