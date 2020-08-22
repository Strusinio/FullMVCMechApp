using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MechAppProject.Models
{
    public class DisplayEventModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string WorkshopName { get; set; }
        public string CustomerName { get; set; }
        public int ServiceEventId { get; set; }
        public string Status { get; set; }
        public OrderStatus StatusId { get; set; }
    }
}