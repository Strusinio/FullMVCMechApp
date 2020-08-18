using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MechAppProject.DBModule;

namespace MechAppProject.Controllers
{
    public class WorkshopServicesController : Controller
    {
        private MechAppProjectEntities db = new MechAppProjectEntities();

        // GET: WorkshopServices
        public async Task<ActionResult> Index()
        {
            var workshopServices = db.WorkshopServices.Include(w => w.Workshop);
            return View(await workshopServices.ToListAsync());
        }

        // GET: WorkshopServices/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkshopService workshopService = await db.WorkshopServices.FindAsync(id);
            if (workshopService == null)
            {
                return HttpNotFound();
            }
            return View(workshopService);
        }

        // GET: WorkshopServices/Create
        public ActionResult Create()
        {
            ViewBag.WorkshopId = new SelectList(db.Workshops, "WorkshopId", "Login");
            return View();
        }

        // POST: WorkshopServices/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ServiceId,WorkshopId,Title,Price,DurationInHrs,Description,DurationInMinutes,PriceDecimal")] WorkshopService workshopService)
        {
            if (ModelState.IsValid)
            {
                db.WorkshopServices.Add(workshopService);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.WorkshopId = new SelectList(db.Workshops, "WorkshopId", "Login", workshopService.WorkshopId);
            return View(workshopService);
        }

        // GET: WorkshopServices/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkshopService workshopService = await db.WorkshopServices.FindAsync(id);
            if (workshopService == null)
            {
                return HttpNotFound();
            }
            ViewBag.WorkshopId = new SelectList(db.Workshops, "WorkshopId", "Login", workshopService.WorkshopId);
            return View(workshopService);
        }

        // POST: WorkshopServices/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ServiceId,WorkshopId,Title,Price,DurationInHrs,Description,DurationInMinutes,PriceDecimal")] WorkshopService workshopService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workshopService).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.WorkshopId = new SelectList(db.Workshops, "WorkshopId", "Login", workshopService.WorkshopId);
            return View(workshopService);
        }

        // GET: WorkshopServices/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkshopService workshopService = await db.WorkshopServices.FindAsync(id);
            if (workshopService == null)
            {
                return HttpNotFound();
            }
            return View(workshopService);
        }

        // POST: WorkshopServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WorkshopService workshopService = await db.WorkshopServices.FindAsync(id);
            db.WorkshopServices.Remove(workshopService);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
