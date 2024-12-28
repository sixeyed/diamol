using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;
using PagedList;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {
        private NerdDinnerContext db = new NerdDinnerContext();

        //
        // GET: /Dinners/
        private const int PageSize = 25;
        public ActionResult Index(int? page)
        {
            int pageIndex = page ?? 1;

            var dinners = db.Dinners.Where(d => d.EventDate >= DateTime.Now).OrderBy(d => d.EventDate);
            return View(dinners.ToPagedList(pageIndex, PageSize));
        }

        //
        // GET: /Dinners/Details/5

        public ActionResult Details(int id = 0)
        {
            Dinner dinner = db.Dinners.Find(id);
            if (dinner == null)
            {
                return HttpNotFound();
            }
            return View(dinner);
        }

        //
        // GET: /Dinners/Create

        [Authorize]
        public ActionResult Create()
        {
            var dinner = new Dinner()
            {
                EventDate = DateTime.Now.AddDays(7),
                HostedBy = User.Identity.Name
            };

            return View(dinner);
        }

        //
        // POST: /Dinners/Create

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                dinner.HostedBy = User.Identity.Name;

                RSVP rsvp = new RSVP();
                rsvp.AttendeeName = User.Identity.Name;

                dinner.RSVPs = new List<RSVP>();
                dinner.RSVPs.Add(rsvp);

                db.Dinners.Add(dinner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dinner);
        }

        //
        // GET: /Dinners/Edit/5

        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Dinner dinner = db.Dinners.Find(id);
            if (dinner == null)
            {
                return HttpNotFound();
            }
            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }
            return View(dinner);
        }

        //
        // POST: /Dinners/Edit/5

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult Edit(Dinner dinner)
        {
            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }

            if (ModelState.IsValid)
            {
                db.Entry(dinner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dinner);
        }

        //
        // GET: /Dinners/Delete/5

        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Dinner dinner = db.Dinners.Find(id);
            if (dinner == null)
            {
                return HttpNotFound();
            }
            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }
            return View(dinner);
        }

        //
        // POST: /Dinners/Delete/5

        [HttpPost, ActionName("Delete"), Authorize, ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dinner dinner = db.Dinners.Find(id);

            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }

            db.Dinners.Remove(dinner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult WebSlicePopular()
        {
            ViewData["Title"] = "Popular Nerd Dinners";
            var model = from dinner in db.Dinners
                        where dinner.EventDate >= DateTime.Now
                        orderby dinner.RSVPs.Count descending
                        select dinner;
            return View("WebSlice", model.Take(5));
        }

        public ActionResult WebSliceUpcoming()
        {
            ViewData["Title"] = "Upcoming Nerd Dinners";
            DateTime d = DateTime.Now.AddMonths(2);
            var model = from dinner in db.Dinners
                        where dinner.EventDate < d
                        orderby dinner.EventDate descending
                        select dinner;
            return View("WebSlice", model.Take(5));
        }
    }
}