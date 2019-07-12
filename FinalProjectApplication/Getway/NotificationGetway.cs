using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using FinalProjectApplication.Models;


namespace FinalProjectApplication.Getway
{
    public class NotificationGetway:DBConnection
    {
        public List<Notification> GetNotifications(Notification notification)
        {
            List<Notification> notifications = new List<Notification>();
            try
            {
                Query = "SELECT * FROM Notification  ORDER BY Id DESC ";

                SqlCommand Command = new SqlCommand(Query, Connection);

                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    notification.NotificationId = (int)Reader["Id"];

                    notification.NotificationTitle = Reader["Title"].ToString();
                    notification.NotificationDescription = Reader["Description"].ToString();
                    notification.NotificationDate = Reader["Date"].ToString();

                    notifications.Add(new Notification() { NotificationId = (int)Reader["Id"], NotificationTitle = Reader["Title"].ToString(), NotificationDescription = Reader["Description"].ToString(), NotificationDate = Reader["Date"].ToString() });
                }

                Reader.Close();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return notifications;

        }

        public Notification DeatilNotification(int id)
        {
            Notification notification = new Notification();
            try
            {
                Query = "SELECT * FROM Notification WHERE Id='" + id + "' ";

                SqlCommand Command = new SqlCommand(Query, Connection);

                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    notification.NotificationId = (int)Reader["Id"];

                    notification.NotificationTitle = Reader["Title"].ToString();
                    notification.NotificationDescription = Reader["Description"].ToString();
                    notification.NotificationDate = Reader["Date"].ToString();


                }

                Reader.Close();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return notification;

        }

        public int SaveNotification(Notification notification)
        {
            try
            {
                Query = "INSERT INTO Notification(Title,Description,Date) VALUES(@Title,@Description,@Date)";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                Command.Parameters.AddWithValue("@Title", notification.NotificationTitle);
                Command.Parameters.AddWithValue("@Description", notification.NotificationDescription);
                Command.Parameters.AddWithValue("@Date", notification.NotificationDate);
                Count = Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return Count;
        }

        public void IncrementNotification()
        {
            try
            {
                Query = "UPDATE tb_NotificationCounter SET Count=Count+1";
                Command = new SqlCommand(Query, Connection);
                Connection.Open();
                Count = Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception)
            {

            }
          
        }

        public void NullNotification(int userId)
        {
            try
            {
                Query = "UPDATE tb_NotificationCounter SET Count=0 where UserId='" + userId + "'";
                Command = new SqlCommand(Query, Connection);
                Connection.Open();
                Count = Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception)
            {

            }
         
        }

        public void SaveUserToNotificationCounter()
        {
            try
            {
                Query = "INSERT INTO tb_NotificationCounter (Count) VALUES('" + 0 + "')";
                Command = new SqlCommand(Query, Connection);
                Connection.Close();
                Connection.Open();
                Count = Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception)
            {

            }
         
        }

        public int ShowCount(int userId)
        {
            try
            {
                Query = "Select Count from tb_NotificationCounter where UserId='" + userId + "'";
                SqlCommand Command = new SqlCommand(Query, Connection);
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Count = (int)Reader["Count"];
                }
                Reader.Close();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return Count;

        }

    }
}