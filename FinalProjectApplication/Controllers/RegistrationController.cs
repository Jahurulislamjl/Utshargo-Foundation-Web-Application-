using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProjectApplication.Models;
using FinalProjectApplication.Manager;

namespace FinalProjectApplication.Controllers
{
    public class RegistrationController : Controller
    {
        LoginManager loginManager = new LoginManager();

        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult CheckEmailExistForUpdateProfile(UpdateProfile updateProfile)
        {
            bool check = loginManager.CheckEmailExistForUpdateProfile(updateProfile.Email, Convert.ToInt32(Session["UserId"].ToString()));
            return Json(check);
        }
    }
}