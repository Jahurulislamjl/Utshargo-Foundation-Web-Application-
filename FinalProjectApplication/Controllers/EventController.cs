using FinalProjectApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FinalProjectApplication.Manager;

namespace FinalProjectApplication.Controllers
{
    public class EventController : Controller
    {
        EventManager eventManager = new EventManager();
        NotificationManager notificationManager = new NotificationManager();
    
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateEvent()
        {
            if (Session["UserId"] == null ||Convert.ToInt32(Session["Rule"])!=1)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateEvent(Event aEvent)
        {
            if (aEvent.FromTime == aEvent.ToTime)
            {
                ViewBag.ErrorMessage = "FromTime and ToTime can not be same";
                return View();
            }
            else if (eventManager.IsEventDateAndFromTimeExist(aEvent)!=0)
            {
                ViewBag.ErrorMessage = "Start time is already exist";
                return View();
            }
            else if (eventManager.IsEventDateAndToTimeExist(aEvent)!=0)
            {
                ViewBag.ErrorMessage = "End Time is already exist";
                return View();
            }
            ViewBag.Message =eventManager.SaveEvent(aEvent);

            ModelState.Clear();
            return View();
        }

        public ActionResult ShowEvent()
        {
            if (Session["UserId"] == null)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            ViewBag.Message = Session["Message"];
            Session["Message"] = null;
            return View(eventManager.ShowEvent());
        }

        public ActionResult ShowRecentEvent()
        {
            if (Session["UserId"] ==null)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            ViewBag.Message = Session["Message"];
            Session["Message"] = null;
            return View(eventManager.ShowRecentEvent());
        }
        
        public ActionResult DetailEvent(int? id)
        {
            if (Session["UserId"] == null )
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            return View(eventManager.DetailEvent(Convert.ToInt32(id)));
        }

        public ActionResult DeleteEvent(int? eventId)
        {
            if (Session["UserId"] == null || Convert.ToInt32(Session["Rule"]) != 1)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
           //Event aEvent=eventManager.DetailEvent(Convert.ToInt32(id));
            return View(eventManager.DetailEvent(Convert.ToInt32(eventId)));
        }
        [HttpPost]
        public ActionResult DeleteEvent(int eventId)
        {
           
            Session["Message"]= eventManager.DeleteEvent(eventId);
            return RedirectToAction("ShowEvent");
        }

      
    }
}