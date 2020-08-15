using MechAppProject.DBModule;
using MechAppProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MechAppProject.Controllers
{
    public class WorkshopController : Controller
    {
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
    }
}