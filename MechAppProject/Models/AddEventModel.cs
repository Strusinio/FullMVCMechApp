using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MechAppProject.Models
{
    public class AddEventModel
    {
        [DisplayName("Wybierz datę")]
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
        OrderReceived = 1,
        OrderPending = 2,
        OrderRefuse = 3,
        OrderReadyToCollect = 3
        
    }
}