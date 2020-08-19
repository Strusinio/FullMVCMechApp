using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MechAppProject.DBModule;
using MechAppProject.Models;

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

        public ActionResult Search(string searchString, string workshopGenre)
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

            var genreLst = new List<string>();

            var genreQry = from d in db.Workshops
                           orderby d.WorkshopName
                           select d.WorkshopName;

            genreLst.AddRange(genreQry.Distinct());
            ViewBag.WorkshopGenre = new SelectList(genreLst);

            var movies = from m in db.Workshops
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.WorkshopName.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(workshopGenre))
            {
                movies = movies.Where(x => x.WorkshopName == workshopGenre);
            }

           





            return View(model);
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