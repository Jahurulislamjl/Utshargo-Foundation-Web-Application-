using FinalProjectApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProjectApplication.Manager;
namespace FinalProjectApplication.Controllers
{
    public class BloodController : Controller
    {
        BloodManager bloodManager = new BloodManager();
        NotificationManager notificationManager = new NotificationManager();
        // GET: Blood
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult BloodRequest()
        {
            if (Session["UserId"] == null)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
           
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BloodRequest(Blood blood)
        {
            if (Convert.ToInt32(Session["UserId"]) == 0)
            {
                blood.PhoneNo = "+88"+ blood.PhoneNo;
            }
            // string a= DateTime.Now.Month.ToString();
            ViewBag.Message = bloodManager.SendBloodRequest(blood);
            
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult BloodSearch()
        {
            if (Session["UserId"] == null)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            return View();
        }

        [HttpPost]
        public ActionResult BloodSearch(string  searchBlood)
        {
            ViewBag.Show = "Show";
            return View(bloodManager.GetBloodDonorByBloodGroup(searchBlood));
        }
        public JsonResult CheckPhone(string phoneNo)
        {
            if (phoneNo.StartsWith("01"))
            {
                return Json("Invalid Contact no", JsonRequestBehavior.AllowGet);
            }
            return Json(true);

        }

        public ActionResult ShowAllBloodRequest()
        {
            if (Session["UserId"] == null)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            return View(bloodManager.GetAllBloodRequest());
        }
    }
}