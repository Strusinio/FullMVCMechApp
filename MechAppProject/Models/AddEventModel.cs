using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MechAppProject.Models
{
    public class AddEventModel
    {
        [DisplayName("Wybierz datę")]
        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Data jest wymagana")]
        public DateTime ServiceDate { get; set; }
        public SelectListItem ServiceHours { get; set; }
        [DisplayName("Wybierz godzinę")]
        public SelectList ServiceHourSelectList { get; set; }
        public SelectListItem WorkshopService { get; set; }
        [DisplayName("Wybierz usługę")]
        public SelectList WorkshopServicesSelectList { get; set; }
        public SelectListItem CustomerCar { get; set; }
        [DisplayName("Wybierz samochód")]
        public SelectList CustomerCarsSelectList { get; set; }
        public List<CalendarEventJson> CalendarEventsJson { get; set; }

    }

    public class CalendarEventJson
    {
        public string summary { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }

    public enum OrderStatus
    {
        [Display(Name = "Złożono")]
        OrderReceived = 1,
        [Display(Name = "W trakcie")]
        OrderPending = 2,
        [Display(Name = "Odrzucono")]
        OrderRefuse = 3,
        [Display(Name = "Gotowy")]
        OrderReadyToCollect = 4,
        [Display(Name = "Zakończony")]
        OrderDone = 5

    }
}