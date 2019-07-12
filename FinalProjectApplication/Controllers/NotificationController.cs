using FinalProjectApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProjectApplication.Manager;

namespace FinalProjectApplication.Controllers
{
    public class NotificationController : Controller
    {
        NotificationManager notificationManager = new NotificationManager();
        EventManager eventManager = new EventManager();
        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }
         
        public ActionResult Notification(Notification notification )
        {
            if (Session["UserId"] == null)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            notificationManager.DoNotificationNull(Convert.ToInt32(Session["UserId"]));
            return View(notificationManager.GetNotifications(notification));
        }
        public ActionResult DetailNotification(int? id)
        {
            if (Session["UserId"] == null)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            return View(notificationManager.DeatailNotification(Convert.ToInt32(id)));
        }
        public int ShowCount()
        {
            //eventManager.GetEventNotificationBeforeDay();   
            return notificationManager.ShowCount(Convert.ToInt32(Session["UserId"]));
        }

    }
}