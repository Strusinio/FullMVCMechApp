using MechAppProject.DBModule;
using MechAppProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MechAppProject.Controllers
{
    public class AddCarController : Controller
    {
        MechAppProjectEntities objMechAppProjectEntities = new MechAppProjectEntities();

        public int CustomerId { get; private set; }

        // GET: AddCar
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddNewCar()
        {
            CarModel objCarModel = new CarModel();
            return View(objCarModel);
        }
        [HttpPost]
        public ActionResult AddNewCar(CarModel objCarModel)
        {
            var userSession = Session["Login"] as SessionModel;

            if (userSession != null)
            {


                using (var db = new MechAppProjectEntities())
                {
                    var CarModel = db.Cars.FirstOrDefault(x => x.CarId == userSession.UserId);

                    Car objCar = new Car
                    {
                        CustomerId = userSession.UserId,
                        Brand = objCarModel.Brand,
                        Model = objCarModel.Model,
                        EngineType = objCarModel.EngineType
                    };

                    if (objMechAppProjectEntities.Cars.Any(x => x.Brand == objCarModel.Brand
                           && x.Model == objCarModel.Model
                           && x.EngineType == x.EngineType))
                    {
                        ViewBag.DuplicateCar = "Ten samochód został już wczesniej dodany!!";
                        return View("AddNewCar", objCarModel);
                    }

                    db.Cars.Add(objCar);
                    db.SaveChanges();

                    ModelState.Clear();
                    ViewBag.SuccessMessage = "Samochód dodany poprawnie";
                    return RedirectToAction("Index", "Home");

                }

            }
            return View();
        }

        public ActionResult YourCarDetails()
        {
            var model = new List<CarModel>();
            var userSession = Session["Login"] as SessionModel;

            if (userSession != null)
            {
                using (var db = new MechAppProjectEntities())   
                {                    
                    var cars = db.Cars.Where(x => x.CustomerId == userSession.UserId).ToList();
                   
                    foreach (var car in cars)
                    {
                        var carModel = new CarModel()
                        {
                            CustomerId = userSession.UserId,                                                   
                            Brand = car.Brand,
                            Model = car.Model,
                            EngineType = car.EngineType,
                        };

                        model.Add(carModel);
                    }

                }
            }

            return View(model);
        }
    }
}