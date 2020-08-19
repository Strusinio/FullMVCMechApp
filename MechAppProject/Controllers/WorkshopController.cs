using MechAppProject.DBModule;
using MechAppProject.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MechAppProject.Controllers
{
    public class WorkshopController : Controller
    {
        private MechAppProjectEntities db = new MechAppProjectEntities();

        // GET: Workshop
        public ActionResult Index()
        {
            var model = new List<WorkshopModel>();

            using (var db = new MechAppProjectEntities())
            {
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
            }

            return View(model);
        }

        public ActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddService(WorkshopServiceModel viewModel)
        {
            var session = Session["LoginWorkshop"] as SessionModel;

            if (session != null)
            {
                using (var db = new MechAppProjectEntities())
                {
                    var workshopService = new WorkshopService()
                    {
                        WorkshopId = session.WorkshopId,
                        Title = viewModel.Title,
                        Description = viewModel.Description,
                        Price = viewModel.Price.HasValue ? viewModel.Price.Value : 0,
                        PriceDecimal = viewModel.PriceDecimal.HasValue ? viewModel.PriceDecimal.Value : 0,
                        DurationInHrs = viewModel.DurationInHours.HasValue ? viewModel.DurationInHours.Value : 0,
                        DurationInMinutes = viewModel.DurationInMinutes.HasValue ? viewModel.DurationInMinutes.Value : 0
                    };

                    db.WorkshopServices.Add(workshopService);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult YourServices()
        {
            var model = new List<WorkshopServiceModel>();
            var session = Session["LoginWorkshop"] as SessionModel;

            if (session != null)
            {
                using (var db = new MechAppProjectEntities())
                {
                    var services = db.WorkshopServices.Where(x => x.WorkshopId == session.WorkshopId).ToList();

                    foreach (var workshopService in services)
                    {
                        var workshopServiceModel = new WorkshopServiceModel()
                        {
                            WorkshopId = session.WorkshopId,
                            Title = workshopService.Title,
                            Description = workshopService.Description,
                            Price = workshopService.Price,
                            PriceDecimal = workshopService.PriceDecimal,
                            DurationInHours = workshopService.DurationInHrs,
                            DurationInMinutes = workshopService.DurationInMinutes,
                        };

                        model.Add(workshopServiceModel);
                    }

                }
            }

            return View(model);
        }

        public ActionResult Table()
        {
            return View();
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

        public ActionResult ServiceDetails(int? workshopId)

        {

            if (workshopId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WorkshopService workshopService = db.WorkshopServices.Find(workshopId);

            if (workshopService == null)
            {
                return HttpNotFound();
            }
            return View(workshopService);
        }


    }
}
