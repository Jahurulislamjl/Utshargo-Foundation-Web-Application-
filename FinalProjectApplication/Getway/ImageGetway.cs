using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using FinalProjectApplication.Models;

namespace FinalProjectApplication.Getway
{
    public class ImageGetway : DBConnection
    {
        public int SaveImage(Image image)
        {
            try
            {
                Query = "INSERT INTO tb_Image(Title,ImagePath) VALUES(@Title,@ImagePath)";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                Command.Parameters.AddWithValue("@Title", image.Title);
                Command.Parameters.AddWithValue("@ImagePath", image.ImagePath);
                Count = Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return Count;
        }

        public List<Image> ShowImage()
        {
            Image image = new Image();
            List<Image> images = new List<Image>();
            try
            {
                Query = "SELECT * FROM tb_Image ORDER BY Id DESC";
                Command = new SqlCommand(Query, Connection);
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    image.Id = (int)Reader["Id"];
                    image.Title = Reader["Title"].ToString();
                    image.ImagePath = Reader["ImagePath"].ToString();
                    images.Add(
                        new Image()
                        {
                            Id = (int)Reader["Id"],
                            Title = Reader["Title"].ToString(),
                            ImagePath = Reader["ImagePath"].ToString()
                        }
                        );
                }
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return images;
        }

        public Image DetailImage(int id)
        {
            Image image = new Image();
            try
            {
                Query = "SELECT * FROM tb_Image WHERE Id=@Id";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                Command.Parameters.AddWithValue("@Id",id);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    image.Id = (int)Reader["Id"];
                    image.Title = Reader["Title"].ToString();
                    image.ImagePath = Reader["ImagePath"].ToString();

                }
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return image;
        }

        public int DeleteImage(int imageId)
        {
            try
            {
                Query = "Delete tb_Image WHERE Id=@Id ";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                Command.Parameters.AddWithValue("@Id",imageId);
                Count = Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return Count;

        }
    }
}