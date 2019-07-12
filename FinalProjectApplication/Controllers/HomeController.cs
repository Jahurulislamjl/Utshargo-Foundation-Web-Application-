using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProjectApplication.Manager;
using FinalProjectApplication.Models;
using System.Web.Security;
using FinalProjectApplication.Getway;
using Rotativa;




namespace FinalProjectApplication.Controllers
{
   // [RequireHttps]
    public class HomeController : Controller
    {
        RegistrationManager registrationManager = new RegistrationManager();
        LoginManager loginManager = new LoginManager();
        SmsManager smsManager = new SmsManager();
        NotificationManager notificationManager = new NotificationManager();
        EventManager eventManager = new EventManager();

        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("InitialIndex");
            }
            eventManager.GetEventNotificationBeforeDay();
            ViewBag.Message = Session["Message"];
            Session["Message"] = null;
            Login login = new Login();
            if (Session["UserId"] != null && Convert.ToInt32(Session["Rule"]) != 4)
            {
                
                //Login login = new Login();
                login.PhoneNo = Session["Phone"].ToString();
                login.Password = Session["Password"].ToString();
                if (loginManager.IsPasswordChanged(login) == 0)
                {
                    return RedirectToAction("Logout");
                }
            }
            return View();
        }
        public ActionResult InitialIndex()
        {
            ViewBag.Message = Session["LogoutMessage"];
            Session["LogoutMessage"] = null;
            return View();
        }

        public ActionResult About()
        {
            if (Session["UserId"] == null)
            {
                return new HttpNotFoundResult("oppa,This page is not for you");
            }
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Registration()
        {
            if (Session["UserId"] != null && Convert.ToInt32(Session["Rule"]) != 1)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Registration(User user)
        {
            ViewBag.Message = null;
            Session["PhoneNo"] = "+88" + user.PhoneNo;
            user.PhoneNo = Session["PhoneNo"].ToString();
        
            ViewBag.Message = registrationManager.SaveUser(user);
            //ViewBag.Message = loginManager.SaveUser(user);

            notificationManager.SaveUserToNotificationCounter();
            ModelState.Clear();
            return RedirectToAction("VerifyPhoneNo");
        }

        public ActionResult Login()
        {
            if (Session["UserId"] != null || Convert.ToInt32(Session["Rule"]) == 2)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            return View();
        }


        [HttpPost]
        public ActionResult Login(Login login)
        {
            User user = new User();

            login.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "SHA1");

            login.PhoneNo = "+88" + login.PhoneNo;
            if (loginManager.CheckUser(login) > 0 || loginManager.CheckUserName(login) > 0 || loginManager.CheckPhone(login) > 0)
            {
                Session["Message"] = "Welcome!!Login Successfull";
                if (loginManager.CheckUser(login) > 0)
                {
                    user = loginManager.GetSessionByEmail(login);
                    Session["UserId"] = user.UserId;
                    Session["Name"] = user.Name;
                    Session["UserName"] = user.UserName;
                    Session["BloodGroup"] = user.BloodGroup;
                    Session["Password"] = user.Password;
                    Session["Phone"] = user.PhoneNo;
                    Session["Email"] = user.Email;
                    Session["Rule"] = user.Rule;
                }
                else if (loginManager.CheckUserName(login) > 0)
                {
                    user = loginManager.GetSessionByUserName(login);
                    Session["UserId"] = user.UserId;
                    Session["Name"] = user.Name;
                    Session["UserName"] = user.UserName;
                    Session["BloodGroup"] = user.BloodGroup;
                    Session["Phone"] = user.PhoneNo;
                    Session["Password"] = user.Password;
                    Session["Email"] = user.Email;
                    Session["Rule"] = user.Rule;

                }

                else if (loginManager.CheckPhone(login) > 0)
                {
                    user = loginManager.GetSessionByPhone(login);
                    Session["UserId"] = user.UserId;
                    Session["Name"] = user.Name;
                    Session["UserName"] = user.UserName;
                    Session["BloodGroup"] = user.BloodGroup;
                    Session["Phone"] = user.PhoneNo;
                    Session["Password"] = user.Password;
                    Session["Email"] = user.Email;
                    Session["Rule"] = user.Rule;

                }

                return RedirectToAction("Index");

            }

            else
                ViewBag.Message = "!!Email or Password is wrong!!";
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult IsEmailExist(string email)
        {
            bool check = registrationManager.IsEmailExist(email);
            return Json(check);
        }



        [HttpPost]
        public ActionResult IsUserNameExist(string userName)
        {
            bool check = registrationManager.IsUserNameExist(userName);
            return Json(check);
        }
        [HttpPost]
        public ActionResult IsPhoneExist(Login login)
        {
            if (!login.PhoneNo.StartsWith("01"))
            {
                return Json("Invalid Contact no", JsonRequestBehavior.AllowGet);
            }
            login.PhoneNo = "+88" + login.PhoneNo;
            bool check = registrationManager.IsPhoneExist(login.PhoneNo);
            if (check == false)
            {
                return Json("Phone Number already exist", JsonRequestBehavior.AllowGet);
            }
            return Json(check);
        }

        public ActionResult IsAccountVerify(string phone)
        {
            bool check = registrationManager.IsPhoneExist(phone);
            return Json(check);
        }

        [HttpGet]
        public ActionResult VerifyPhoneNo()
        {
            if (Session["UserId"] != null)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            int code = registrationManager.GetVerificationCode();
            Session["VerificationCode"] = code;
            string phone = Session["PhoneNo"].ToString();
            registrationManager.SendVerificationCode(code, phone);
            return View();
        }

        [HttpPost]
        public ActionResult VerifyPhoneNo(string code)
        {
            if (code == Session["VerificationCode"].ToString() && Session["UserId"] != null)
            {
                Session["Message"] = loginManager.UpdatePhoneNo(Session["PhoneNo"].ToString(), Convert.ToInt32(Session["UserId"]));
                Session["Phone"] = Session["PhoneNo"];
                Session["PhoneNo"] = null;
                return RedirectToAction("UpdateProfile");
            }

            if (code == Session["VerificationCode"].ToString())
            {
                Session["Message"] = "Successfuly verified your account.you can login now";
                loginManager.VerifyAccount(Session["PhoneNo"].ToString());
                return RedirectToAction("Login");
            }

            else
                ViewBag.Message2 = "Code do not match";
            return View();
        }

        public ActionResult VerifyUserPhoneNo()
        {
            if (Session["UserId"] != null)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            return View();
        }

        [HttpPost]
        public ActionResult VerifyUserPhoneNo(string phone)
        {
            string phone2 = "+88" + phone;

            if (registrationManager.IsPhoneExist(phone2) == false)
            {
                if (registrationManager.CheckIsVerifyPhone(phone2) == false)
                {
                    ViewBag.Message = "Your phone no is already verified";
                    return View();
                }
                Session["PhoneNo"] = phone2;
                return RedirectToAction("VerifyPhoneNo");
            }

            ViewBag.Message = "Phone no is not exist in the system";
            return View();
        }


        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(ForgetPassword forgetPassword)
        {
            forgetPassword.Phone = "+88" + forgetPassword.Phone;
            if (loginManager.VerifyPhoneAndEmail(forgetPassword) > 0 || loginManager.VerifyPhoneAndUserName(forgetPassword) > 0)
            {
                string password = loginManager.GetPassword();
                if (smsManager.SendPasswordToPhone(password, forgetPassword.Phone) > 0)
                {
                    string SetPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password.ToString(), "SHA1");
                    int count = loginManager.SaveForgetPassword(SetPassword, forgetPassword.Phone);

                    if (count>0)
                    {
                        ViewBag.Message = "Password sent to your phone.You can login with that.";
                    }
                }
            }
            else
                ViewBag.Message2 = "Invalid request";
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPassword resetPassword)
        {

            resetPassword.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(resetPassword.Password, "SHA1");
            bool check = loginManager.CheckPasswordForResetPassword(resetPassword.Password, Convert.ToInt32(Session["UserId"]));
            if (check)
            {
                resetPassword.NewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(resetPassword.NewPassword, "SHA1");
                ViewBag.Message = loginManager.SaveResetPassword(resetPassword.NewPassword, Convert.ToInt32(Session["UserId"]));
                ModelState.Clear();
            }
            else
            {
                ViewBag.Message2 = "Invalid old password";
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult IsUserVerify(Login login)
        {


            if (!loginManager.CheckVerifyAccountByEmail(login) || !loginManager.CheckVerifyAccountByPhone(login) || !loginManager.CheckVerifyAccountByUserName(login))
            {
                return Json(false);
            }
            return Json(true);
        }

        public ActionResult UpdateProfile()
        {
            if (Session["UserId"] == null || Convert.ToInt32(Session["Rule"]) == 4)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            ViewBag.Message = Session["Message"];
            Session["Message"] = null;
            //UpdateProfile profile= loginManager.UpdateProfile(Convert.ToInt32(Session["UserId"]));
            return View(loginManager.UpdateProfile(Convert.ToInt32(Session["UserId"])));
        }
        [HttpPost]
        public ActionResult UpdateProfile(UpdateProfile profile)
        {
            Session["BloodGroup"] = profile.BloodGroup;
            ViewBag.Message = loginManager.SaveUpdateProfile(profile);
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult CheckEmailExistForUpdateProfile(UpdateProfile updateProfile)
        {
            bool check = loginManager.CheckEmailExistForUpdateProfile(updateProfile.Email, Convert.ToInt32(Session["UserId"].ToString()));
            return Json(check);
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult CheckUserNameExistForUpdateProfile(UpdateProfile updateProfile)
        {
            bool check = loginManager.CheckIsUserNameExistForUpdateProfile(updateProfile.UserName, Convert.ToInt32(Session["UserId"].ToString()));
            return Json(check);
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult CheckPhoneExistForUpdateProfile(UpdateProfile updateProfile)
        {
            if (!updateProfile.PhoneNo.StartsWith("01"))
            {
                return Json("Invalid Contact no", JsonRequestBehavior.AllowGet);
            }
            updateProfile.UserId =Convert.ToInt32(Session["UserId"]);
            bool check = loginManager.CheckPhoneExistForUpdateProfile(updateProfile.PhoneNo,updateProfile.UserId);
            if (check == false)
            {
                return Json("Phone Number already exist", JsonRequestBehavior.AllowGet);
            }
            if (Session["Phone"].ToString()=="+88"+updateProfile.PhoneNo)
            {
                return Json("Enter new phone number", JsonRequestBehavior.AllowGet);
            }
            return Json(true);
        }
        [HttpGet] 
        public ActionResult UpdatePhone()
        {
            if (Session["UserId"] == null || Convert.ToInt32(Session["Rule"]) == 4)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult UpdatePhone(UpdateProfile profile)
        { 
            Session["PhoneNo"] = "+88" + profile.PhoneNo;  
            return RedirectToAction("VerifyPhoneNo");
        }


        public ActionResult MemberList()
        {
            if (Session["UserId"] == null || Convert.ToInt32(Session["Rule"]) != 1)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            return View(registrationManager.GetAllMemberList());
        }

        public ActionResult ExportPdf()
        {
            if (Session["UserId"] == null || Convert.ToInt32(Session["Rule"]) !=1)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            return new ActionAsPdf("MemberList")
            {
                FileName = Server.MapPath("~/Content/MemberList.pdf")
            };
        }
        
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult IsOldPasswordMatch(ResetPassword password)
        {
            return Json(loginManager.CheckPasswordForResetPassword(password.Password,Convert.ToInt32(Session["UserId"])));
        }
        public ActionResult Logout()
        {
            
            Session["UserId"] = null;
            Session["Name"] = null;
            Session["UserName"] =null;
            Session["BloodGroup"] = null;
            Session["Phone"] = null;
            Session["Email"] = null;
            Session["Password"] = null;
            Session["Rule"] =null;
            Session["LogoutMessage"] = "Successfuly Logout.Thanks for Visiting us";
            return RedirectToAction("InitialIndex");
        }

        public ActionResult GuestLogin()
        {
            Session["UserId"] =0;
            Session["Rule"] = 4;
            return RedirectToAction("InitialIndex");
        }


    }
}