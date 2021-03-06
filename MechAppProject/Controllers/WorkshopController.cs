﻿using MechAppProject.DBModule;
using MechAppProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MechAppProject.Code.Helpers;
using PagedList;

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
                        DurationInHours = viewModel.DurationInHours.HasValue ? viewModel.DurationInHours.Value : 0,
                        DurationInMinutes = viewModel.DurationInMinutes.HasValue ? viewModel.DurationInMinutes.Value : 0
                    };

                    db.WorkshopServices.Add(workshopService);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("YourServices");
        }
        public ActionResult YourServices(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var viewModel = new List<WorkshopServiceModel>();
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
                            ServiceId = workshopService.ServiceId,
                            WorkshopId = session.WorkshopId,
                            Title = workshopService.Title,
                            Description = workshopService.Description,
                            Price = workshopService.Price,
                            PriceDecimal = workshopService.PriceDecimal,
                            DurationInHours = workshopService.DurationInHours,
                            DurationInMinutes = workshopService.DurationInMinutes,
                        };

                        viewModel.Add(workshopServiceModel);
                    }

                }
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var eventsView = from w in viewModel
                select w;

            if (!String.IsNullOrEmpty(searchString))
            {
                eventsView = eventsView.Where(w => w.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    eventsView = eventsView.OrderByDescending(w => w.Title);
                    break;
                default:  // Name ascending 
                    eventsView = eventsView.OrderBy(w => w.Title);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(eventsView.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DeleteWorkshopService(int workshopServiceId)
        {
            using (var db = new MechAppProjectEntities())
            {
                var service = db.WorkshopServices.First(x => x.ServiceId == workshopServiceId);

                db.WorkshopServices.Remove(service);
                db.SaveChanges();
            }

            return Json(new { success = true, workshopServiceId = workshopServiceId }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplayWorkshopEvents(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var session = Session["LoginWorkshop"] as SessionModel;
            var viewModel = new List<DisplayEventModel>();

            if (session != null)
            {
                using (var db = new MechAppProjectEntities())
                {
                    var workshopEvents = db.ServiceEvents.Where(x => x.WorkshopService.WorkshopId == session.WorkshopId).ToList();

                    workshopEvents.ForEach(x =>
                    {
                        viewModel.Add(new DisplayEventModel()
                        {
                            StartDate = x.StartDate,
                            EndDate = x.EndDate,
                            WorkshopName = x.WorkshopService.Workshop.WorkshopName,
                            Status = EventsHelper.ConvertEventStatus(x.OrderStatus),
                            CustomerName = x.Customer.Name,
                            StatusId = (OrderStatus)x.OrderStatus,
                            ServiceEventId = x.ServiceEventId
                        });
                    });
                }
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var eventsView = from w in viewModel
                             select w;

            if (!String.IsNullOrEmpty(searchString))
            {
                eventsView = eventsView.Where(w => w.WorkshopName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    eventsView = eventsView.OrderByDescending(w => w.Status);
                    break;
                default:  // Name ascending 
                    eventsView = eventsView.OrderBy(w => w.Status);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(eventsView.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult WorkshopSitePanel(int workshopId)
        {
            var viewModel = new WorkshopSitePanelModel();

            using (var db = new MechAppProjectEntities())
            {
                var workshop = db.Workshops.First(x => x.WorkshopId == workshopId);
                var workshopServices = db.WorkshopServices.Where(x => x.WorkshopId == workshopId).ToList();

                viewModel.ZipCode = workshop.ZipCode;
                viewModel.WorkshopName = workshop.WorkshopName;
                viewModel.StreetNbr = workshop.StreetNbr;
                viewModel.Street = workshop.Street;
                viewModel.PhoneNbr = workshop.PhoneNbr;
                viewModel.OwnerName = workshop.OwerName;
                viewModel.Email = workshop.Email;
                viewModel.City = workshop.City;
                
                foreach (var workshopService in workshopServices)
                {
                    viewModel.WorkshopServices.Add(new WorkshopServiceModel()
                    {
                        Description = workshopService.Description,
                        DurationInHours = workshopService.DurationInHours,
                        DurationInMinutes = workshopService.DurationInMinutes,
                        Price = workshopService.Price,
                        PriceDecimal = workshopService.PriceDecimal,
                        Title = workshopService.Title,
                        WorkshopId = workshopService.WorkshopId
                    });
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult AddWorkshopDescription()
        {
            WorkshopDescriptionModel objWorkshopDescriptionModel = new WorkshopDescriptionModel();
            return View(objWorkshopDescriptionModel);
        }
        [HttpPost]
        public ActionResult AddWorkshopDescription(WorkshopDescriptionModel objWorkshopDescriptionModel)
        {

            var userSession = Session["LoginWorkshop"] as SessionModel;

            if (userSession != null)
            {


                using (var db = new MechAppProjectEntities())
                {
                    var WorkshopDecrtiptionModel = db.WorkshopDescriptions.Where(x => x.WorkshopDescriptionID == userSession.WorkshopId);
                    MechAppProjectEntities objMechAppProjectEntities = new MechAppProjectEntities();
                    WorkshopDescription objWorkshopDescription = new WorkshopDescription
                    {
                        WorkshopId = userSession.WorkshopId,
                        WorkshopDes = objWorkshopDescriptionModel.WorkshopDes
                    };

                    db.WorkshopDescriptions.Add(objWorkshopDescription);
                    db.SaveChanges();

                    ModelState.Clear();
                    ViewBag.SuccessMessage = "Opis dodany";
                    return RedirectToAction("Index", "Home");
                }

            }

            return View();
        }
        public ActionResult YourDescription()
        {
            var model = new WorkshopDescriptionModel();
            var userSession = Session["LoginWorkshop"] as SessionModel;

            
                using (var db = new MechAppProjectEntities())
                {
                    var workshopDescription = db.WorkshopDescriptions.FirstOrDefault(x => x.WorkshopId == userSession.WorkshopId);

                    model.WorkshopDes = workshopDescription.WorkshopDes;

                }

                return View(model);
        }



    }
}