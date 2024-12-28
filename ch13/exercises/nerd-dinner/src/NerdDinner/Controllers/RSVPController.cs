using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using NerdDinner.Helpers;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class RSVPController : Controller
    {
        private NerdDinnerContext db = new NerdDinnerContext();

        //
        // HTTP: /RSVP/Register/1
        [Authorize]
        public ActionResult Register(int id)
        {
            RegisterForDinner(id);
            return RedirectToAction("Details", "Dinners", new { id = id });
        }

        //
        // AJAX: /Dinners/RegisterAjax/1
        [Authorize, HttpPost]
        public ActionResult RegisterAjax(int id)
        {
            RegisterForDinner(id);
            return Content("Thanks - we'll see you there!");
        }

        private void RegisterForDinner(int id)
        {
            Dinner dinner = db.Dinners.Find(id);

            if (!dinner.IsUserRegistered(User.Identity.Name))
            {
                RSVP rsvp = new RSVP();
                rsvp.AttendeeName = User.Identity.Name;

                dinner.RSVPs.Add(rsvp);
                db.SaveChanges();
            }
        }

        //
        // AJAX: /RSVP/CancelAjax/1

        [Authorize, HttpPost]
        public ActionResult CancelAjax(int id)
        {
            Dinner dinner = db.Dinners.Find(id);

            RSVP rsvp = dinner.RSVPs.SingleOrDefault(r => this.User.Identity.Name ==  r.AttendeeName);
            if (rsvp != null)
            {
                db.RSVPs.Remove(rsvp);
                db.SaveChanges();
            }

            return Content("Sorry you can't make it!");
        }
    }
}
