using MechAppProject.Code.Helpers;
using MechAppProject.DBModule;
using MechAppProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MechAppProject.Controllers
{
    public class EventsController : Controller
    {
        public ActionResult DisplayCustomerEvents()
        {
            var session = Session["Login"] as SessionModel;
            var viewModel = new List<DisplayEventModel>();

            if (session != null)
            {
                using (var db = new MechAppProjectEntities())
                {
                    var customerEvenets = db.ServiceEvents.Where(x => x.CustomerId == session.UserId).ToList();

                    customerEvenets.ForEach(x =>
                    {
                        viewModel.Add(new DisplayEventModel()
                        {
                            StartDate = x.StartDate,
                            EndDate = x.EndDate,
                            WorkshopName = x.WorkshopService.Workshop.WorkshopName,
                            Status = EventsHelper.ConvertEventStatus(x.OrderStatus)
                        });
                    });
                }
            }

            return View(viewModel);
        }

        public ActionResult DisplayWorkshopEvents()
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

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ChangeEventStatus(List<DisplayEventModel> viewModel)
        {
            using (var db = new MechAppProjectEntities())
            {
                var serviceId = viewModel.First().ServiceEventId;
                var serviceEvent = db.ServiceEvents.First(x => x.ServiceEventId == serviceId);

                serviceEvent.OrderStatus = (int)viewModel.First().StatusId;

                db.SaveChanges();
            }

            return RedirectToAction("DisplayWorkshopEvents");
        }

        // GET: Events
        public ActionResult AddEvent(int workshopId)
        {
            var session = Session["Login"] as SessionModel;
            var viewModel = new AddEventModel();

            if (session != null)
            {
                using (var db = new MechAppProjectEntities())
                {
                    var serviceEvents = db.ServiceEvents.Where(x => x.WorkshopService.WorkshopId == workshopId).ToList();

                    viewModel.CalendarEventsJson = serviceEvents.Select(x => new CalendarEventJson() { startDate = x.StartDate, endDate = x.EndDate, summary = x.WorkshopService.Title }).ToList();

                    var customerCars = db.Cars
                        .Where(x => x.CustomerId == session.UserId)
                        .Select(x => new SelectListItem() { Text = x.Model, Value = x.CarId.ToString() })
                        .ToList();

                    var workshopServices = db.WorkshopServices
                        .Where(x => x.WorkshopId == workshopId)
                        .Select(x => new SelectListItem() { Text = x.Title, Value = x.ServiceId.ToString() })
                        .ToList();

                    viewModel.CustomerCarsSelectList = new SelectList(customerCars, "Value", "Text");
                    viewModel.WorkshopServicesSelectList = new SelectList(workshopServices, "Value", "Text");
                    viewModel.ServiceHourSelectList = new SelectList(EventsHelper.GetHoursToSelect(6, 21, 15), "Value", "Text");
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddEvent(AddEventModel viewModel)
        {
            var session = Session["Login"] as SessionModel;

            if (session != null)
            {
                using (var db = new MechAppProjectEntities())
                {
                    var serviceId = Convert.ToInt32(viewModel.WorkshopService.Value);
                    var service = db.WorkshopServices.First(x => x.ServiceId == serviceId);
                    var serviceStartTime = viewModel.ServiceHours.Value.Split(':');

                    var startDate = viewModel.ServiceDate + new TimeSpan(Convert.ToInt32(serviceStartTime[0]), Convert.ToInt32(serviceStartTime[1]), 0);
                    var endDate = startDate + new TimeSpan(service.DurationInHrs, service.DurationInMinutes, 0);

                    var serviceEventModel = new ServiceEvent()
                    {
                        CustomerId = session.UserId,
                        ServiceId = service.ServiceId,
                        OrderStatus = (int)OrderStatus.OrderReceived,
                        StartDate = startDate,
                        EndDate = endDate
                    };

                    db.ServiceEvents.Add(serviceEventModel);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Home");
        }

    }
}