using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MechAppProject.DBModule;
using MechAppProject.Models;
using PagedList;

namespace MechAppProject.Controllers
{
    public class HomeController : Controller
    {

        private MechAppProjectEntities db = new MechAppProjectEntities();
        public ActionResult Index()
        {
            ViewBag.Message = "POTRZEBUJESZ POMOCY?";

            return View();
        }

        public ActionResult Search(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var model = new List<WorkshopModel>();
            var workshops = db.Workshops.ToList();

            foreach (var workshop in workshops)
            {
                var workshopModel = new WorkshopModel()
                {
                    WorkshopId = workshop.WorkshopId,
                    City = workshop.City,
                    Email = workshop.Email,
                    PhoneNbr = workshop.PhoneNbr,
                    Street = workshop.Street,
                    StreetNbr = workshop.StreetNbr,
                    WorkshopName = workshop.WorkshopName,
                    ZipCode = workshop.ZipCode
                };

                model.Add(workshopModel);
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var workshopsView = from w in model
                                select w;

            if (!String.IsNullOrEmpty(searchString))
            {
                workshopsView = workshopsView.Where(w => w.WorkshopName.Contains(searchString) || w.City.Contains(searchString)); ;
            }

            switch (sortOrder)
            {
                case "name_desc":
                    workshopsView = workshopsView.OrderByDescending(s => s.WorkshopName);
                    break;
                default:  // Name ascending 
                    workshopsView = workshopsView.OrderBy(s => s.WorkshopName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(workshopsView.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? workshopId)

        {

            if (workshopId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Workshop workshop = db.Workshops.Find(workshopId);


            if (workshop == null)
            {
                return HttpNotFound();
            }
            return View(workshop);
        }
    }
}