using MechAppProject.DBModule;
using MechAppProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MechAppProject.Controllers
{
    public class AccountController : Controller
    {
        //////////////////////////////Customer Part/////////////////////////////////////////////
        MechAppProjectEntities objMechAppProjectEntities = new MechAppProjectEntities();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CustomerRegister()
        {
            CustomerModel objCustomerModel = new CustomerModel();
            return View(objCustomerModel);
        }
        [HttpPost]
        public ActionResult CustomerRegister(CustomerModel objCustomerModel)
        {
            if (ModelState.IsValid)
            {

                Customer objCustomer = new Customer();
                objCustomer.Login = objCustomerModel.Login;
                if (objMechAppProjectEntities.Customers.Any(x => x.Login == objCustomer.Login))
                {
                    ViewBag.DuplicateMessageLogin = "Ten Login jest już zajęty!!";
                    return View("CustomerRegister", objCustomerModel);
                }
                objCustomer.Password = objCustomerModel.Password;
                objCustomer.Email = objCustomerModel.Email;
                if (objMechAppProjectEntities.Customers.Any(x => x.Email == objCustomer.Email))
                {
                    ViewBag.DuplicateMessageEmail = "Ten Email jest już zajęty!!";
                    return View("CustomerRegister", objCustomerModel);
                }

                objCustomer.Name = objCustomerModel.Name;
                objCustomer.Surname = objCustomerModel.Surname;
                objCustomer.City = objCustomerModel.City;
                objCustomer.Street = objCustomerModel.Street;
                objCustomer.StreetNbr = objCustomerModel.StreetNbr;
                objCustomer.ZipCode = objCustomerModel.ZipCode;
                objCustomer.PhoneNbr = objCustomerModel.PhoneNbr;
                objMechAppProjectEntities.Customers.Add(objCustomer);
                objMechAppProjectEntities.SaveChanges();
                ModelState.Clear();
                ViewBag.SuccessMessage = "Użytkownik dodany poprawnie";
                return RedirectToAction("Index", "Home");
            }

            return View();

        }
        public ActionResult CustomerLogin()
        {
            LoginModel objCustmerLoginModel = new LoginModel();
            return View(objCustmerLoginModel);
        }

        [HttpPost]
        public ActionResult CustomerLogin(LoginModel objCustmerLoginModel)
        {
            ActionResult result = View();

            if (ModelState.IsValid)
            {
                var currentUser = objMechAppProjectEntities.Customers.FirstOrDefault(x => x.Login == objCustmerLoginModel.Login && x.Password == objCustmerLoginModel.Password);

                if (currentUser != null)
                {
                    Session["Login"] = new SessionModel() { UserId = currentUser.CustomerId, UserLogin = currentUser.Login };
                    result = RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Error", "Login i haslo nie są poprawne");
                    result = View("CustomerLogin");
                }
            }

            return result;
        }

        public ActionResult CustomerLogout()
        {
            Session.Abandon();
            return View("CustomerLogin");
        }
        public ActionResult CustomerProfile()
        {
            var model = new CustomerModel();
            var userSession = Session["Login"] as SessionModel;

            if (userSession != null)
            {
                using (var db = new MechAppProjectEntities())
                {
                    var customer = db.Customers.FirstOrDefault(x => x.CustomerId == userSession.UserId);

                    model.Login = customer.Login;
                    model.Password = customer.Password;
                    model.Email = customer.Email;
                    model.Name = customer.Name;
                    model.Surname = customer.Surname;
                    model.City = customer.City;
                    model.Street = customer.Street;
                    model.StreetNbr = customer.StreetNbr;
                    model.ZipCode = customer.ZipCode;
                    model.PhoneNbr = customer.PhoneNbr;
                }
            }

            return View(model);
        }

        public ActionResult CustomerProfileEdit()

        {
            var model = new CustomerModel();
            var userSession = Session["Login"] as SessionModel;
            if (userSession != null)
            {
                using (var db = new MechAppProjectEntities())
                {
                    var customer = db.Customers.FirstOrDefault(x => x.CustomerId == userSession.UserId);

                    model.Login = customer.Login;
                    model.Password = customer.Password;
                    model.Email = customer.Email;
                    model.Name = customer.Name;
                    model.Surname = customer.Surname;
                    model.City = customer.City;
                    model.Street = customer.Street;
                    model.StreetNbr = customer.StreetNbr;
                    model.ZipCode = customer.ZipCode;
                    model.PhoneNbr = customer.PhoneNbr;
                }
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult CustomerProfileEdit(CustomerModel model)
        {
            var userSession = Session["Login"] as SessionModel;

            if (userSession != null)
            {
                using (var db = new MechAppProjectEntities())
                {
                    var customerModel = db.Customers.FirstOrDefault(x => x.CustomerId == userSession.UserId);

                    
                    customerModel.Password = model.Password;
                    customerModel.Email = model.Email;
                    customerModel.Name = model.Name;
                    customerModel.Surname = model.Surname;
                    customerModel.City = model.City;
                    customerModel.Street = model.Street;
                    customerModel.StreetNbr = model.StreetNbr;
                    customerModel.ZipCode = model.ZipCode;
                    customerModel.PhoneNbr = model.PhoneNbr;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("CustomerProfile");
        }

        //////////////////////////////Workshop Part/////////////////////////////////////////////
        [HttpGet]
        public ActionResult WorkshopRegister()
        {
            WorkshopModel objWorkshopModel = new WorkshopModel();
            return View(objWorkshopModel);
        }

        [HttpPost]
        public ActionResult WorkshopRegister(WorkshopModel objWorkshopModel)
        {
            if (ModelState.IsValid)
            {

                Workshop objWorkshop = new Workshop();
                objWorkshop.Login = objWorkshopModel.Login;
                if (objMechAppProjectEntities.Workshops.Any(x => x.Login == objWorkshopModel.Login))
                {
                    ViewBag.DuplicateMessageLogin = "Ten Login jest już zajęty!!";
                    return View("WorkshopRegister", objWorkshopModel);
                }
                objWorkshop.Password = objWorkshopModel.Password;
                objWorkshop.Email = objWorkshopModel.Email;
                if (objMechAppProjectEntities.Workshops.Any(x => x.Email == objWorkshop.Email))
                {
                    ViewBag.DuplicateMessageEmail = "Ten Email jest już zajęty!!";
                    return View("WorkshopRegister", objWorkshopModel);
                }

                objWorkshop.WorkshopName = objWorkshopModel.WorkshopName;
                objWorkshop.OwerName = objWorkshopModel.OwnerName;
                objWorkshop.City = objWorkshopModel.City;
                objWorkshop.Street = objWorkshopModel.Street;
                objWorkshop.StreetNbr = objWorkshopModel.StreetNbr;
                objWorkshop.ZipCode = objWorkshopModel.ZipCode;
                objWorkshop.PhoneNbr = objWorkshopModel.PhoneNbr;
                objMechAppProjectEntities.Workshops.Add(objWorkshop);
                objMechAppProjectEntities.SaveChanges();
                ModelState.Clear();
                ViewBag.SuccessMessage = "Warsztat dodany poprawnie";
                return RedirectToAction("Index", "Home");
            }

            return View();

        }
        public ActionResult WorkshopLogin()
        {
            LoginModel objWorkshopLoginModel = new LoginModel();
            return View(objWorkshopLoginModel);
        }
        [HttpPost]
        public ActionResult WorkshopLogin(LoginModel objWorkshopLoginModel)
        {
            ActionResult result = View();

            if (ModelState.IsValid)
            {
                var currentWorkshop = objMechAppProjectEntities.Workshops.FirstOrDefault(x => x.Login == objWorkshopLoginModel.Login && x.Password == objWorkshopLoginModel.Password);

                if (currentWorkshop != null)
                {
                    Session["LoginWorkshop"] = new SessionModel() { WorkshopId = currentWorkshop.WorkshopId, WorkshopLogin = currentWorkshop.Login };
                    result = RedirectToAction("Index", "Workshop");
                }
                else
                {
                    ModelState.AddModelError("Error", "Login i haslo nie są poprawne");
                    result = View("WorkshopLogin");
                }
            }

            return result;
        }

        public ActionResult LogoutWorkshop()
        {
            Session.Abandon();
            return View("WorkshopLogin");
        }
        public ActionResult WorkshopProfile()
        {
            var model = new WorkshopModel();
            var workshopSession = Session["LoginWorkshop"] as SessionModel;

            if (workshopSession != null)
            {
                using (var db = new MechAppProjectEntities())
                {
                    var workshop = db.Workshops.FirstOrDefault(x => x.WorkshopId == workshopSession.WorkshopId);

                    model.Login = workshop.Login;
                    model.Password = workshop.Password;
                    model.Email = workshop.Email;
                    model.WorkshopName = workshop.WorkshopName;
                    model.OwnerName = workshop.OwerName;
                    model.PhoneNbr = workshop.PhoneNbr;
                    model.City = workshop.City;
                    model.Street = workshop.Street;
                    model.StreetNbr = workshop.StreetNbr;
                    model.ZipCode = workshop.ZipCode;
                }
            }

            return View(model);
        }

        public ActionResult WorkshopProfileEdit()
        {

            {
                var model = new WorkshopModel();
                var workshopSession = Session["LoginWorkshop"] as SessionModel;
                if (workshopSession != null)
                {
                    using (var db = new MechAppProjectEntities())
                    {
                        var workshop = db.Workshops.FirstOrDefault(x => x.WorkshopId == workshopSession.WorkshopId);

                        model.Login = workshop.Login;
                        model.Password = workshop.Password;
                        model.Email = workshop.Email;
                        model.WorkshopName = workshop.WorkshopName;
                        model.OwnerName = workshop.OwerName;
                        model.City = workshop.City;
                        model.Street = workshop.Street;
                        model.StreetNbr = workshop.StreetNbr;
                        model.ZipCode = workshop.ZipCode;
                        model.PhoneNbr = workshop.PhoneNbr;
                    }
                    return View(model);
                }
                return View();
            }
        }

        [HttpPost]
        public ActionResult WorkshopProfileEdit(WorkshopModel model)
        {
            var workshopSession = Session["LoginWorkshop"] as SessionModel;

            if (workshopSession != null)
            {
                using (var db = new MechAppProjectEntities())
                {
                    var workshopModel = db.Workshops.FirstOrDefault(x => x.WorkshopId == workshopSession.WorkshopId);

                    workshopModel.Password = model.Password;
                    workshopModel.Email = model.Email;
                    workshopModel.WorkshopName = model.WorkshopName;
                    workshopModel.OwerName = model.OwnerName;
                    workshopModel.PhoneNbr = model.PhoneNbr;
                    workshopModel.City = model.City;
                    workshopModel.Street = model.Street;
                    workshopModel.StreetNbr = model.StreetNbr;
                    workshopModel.ZipCode = model.ZipCode;

                    db.SaveChanges();
                }
            }

            return RedirectToAction("WorkshopProfile");
        }

    }
}