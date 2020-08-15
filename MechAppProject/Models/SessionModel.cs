using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MechAppProject.Models
{
    public class SessionModel
    {
        public string UserLogin { get; set; }
        public int UserId { get; set; }
        public string WorkshopLogin { get; set; }
        public int WorkshopId { get; set; }

    }
}