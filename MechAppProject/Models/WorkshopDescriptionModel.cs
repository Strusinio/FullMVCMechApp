using MechAppProject.DBModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MechAppProject.Models
{
    public class WorkshopDescriptionModel
    {
        public int WorkshopDescriptionID { get; set; }
        public int WorkshopId { get; set; }
        public string WorkshopDes { get; set; }

        public virtual Workshop Workshop { get; set; }
    }
}