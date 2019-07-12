using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProjectApplication.Getway;
using FinalProjectApplication.Models;

namespace FinalProjectApplication.Manager
{
    public class ImageManager
    {
        ImageGetway imageGetway = new ImageGetway();

        public string SaveImage(Image image)
        {
            if (imageGetway.SaveImage(image) > 0)
            {
                return "Successfully Uploaded Image";
            }
            return "Failed to Upload Image";
        }
        public List<Image> ShowImage()
        {
            return imageGetway.ShowImage();
        }
        public Image DetailImage(int id)
        {
            return imageGetway.DetailImage(id);
        }
        public string DeleteImage(int id)
        {
            if (imageGetway.DeleteImage(id) > 0)
            {
                return "Image Deleted Successfuly";
            }
            return "Failed To Delete";
        }

    }
}