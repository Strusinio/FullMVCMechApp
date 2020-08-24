using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MechAppProject.Models
{
    public class WorkshopSitePanelModel
    {
        public WorkshopSitePanelModel()
        {
            WorkshopServices = new List<WorkshopServiceModel>();
        }

        public string Email { get; set; }
        public string WorkshopName { get; set; }
        public string OwnerName { get; set; }
        public string PhoneNbr { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string StreetNbr { get; set; }
        public string ZipCode { get; set; }
        public List<WorkshopServiceModel> WorkshopServices { get; set; }

        public WorkshopDescriptionModel WorkshopDescription { get; set; }

    }
}
