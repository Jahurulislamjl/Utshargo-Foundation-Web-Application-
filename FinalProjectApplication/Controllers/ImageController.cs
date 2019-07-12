using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProjectApplication.Models;
using FinalProjectApplication.Manager;
using System.IO;

namespace FinalProjectApplication.Controllers
{
    public class ImageController : Controller
    {
        ImageManager imageManager = new ImageManager();
        //Image oImage = new Image();
        //List<Image> o_Images = new List<Image>();
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadImage()
        {
            if (Session["UserId"] == null || Convert.ToInt32(Session["Rule"]) != 1)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(Image image, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string imagePath = Path.Combine(Server.MapPath("~/Image/"), fileName);
                file.SaveAs(imagePath);
            }
            image.ImagePath = "~/Image/" + file.FileName;
            ViewBag.Message = imageManager.SaveImage(image);
            return View();
        }

        public ActionResult ShowImage()
        {
            if (Session["UserId"] == null)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            ViewBag.Message = Session["Message"];
            Session["Message"] = null;
            return View(imageManager.ShowImage());
        }

        public ActionResult DeleteImage(int? id)
        {
            if (Session["UserId"] == null || Convert.ToInt32(Session["Rule"]) != 1)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
            Image image = imageManager.DetailImage(Convert.ToInt32(id));
            ViewBag.Image = image.ImagePath;
            // Session["Message"] = imageManager.DeleteImage(Convert.ToInt32(id));
            return View(image);
        }
        [HttpPost]
        public ActionResult DeleteImage(Image image)
        {
            if (Session["UserId"] == null || Convert.ToInt32(Session["Rule"]) != 1)
            {
                return new HttpNotFoundResult("opps,This page is not for you");
            }
          //  Image image = imageManager.DetailImage(Convert.ToInt32(id));
             Session["Message"] = imageManager.DeleteImage(image.Id);
            return RedirectToAction("ShowImage");
        }
    }
}