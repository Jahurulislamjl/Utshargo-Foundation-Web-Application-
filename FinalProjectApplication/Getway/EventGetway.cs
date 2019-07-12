using FinalProjectApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using FinalProjectApplication.Manager;

namespace FinalProjectApplication.Getway
{
    public class EventGetway : DBConnection
    {
        NotificationManager notificationManager = new NotificationManager();
        SmsManager smsManager = new SmsManager();
        RegistrationGetway registrationGetway = new RegistrationGetway();
        
        public int SaveEvent(Event aEvent)
        {
            try
            {
                Query = "INSERT INTO tbl_Event(UserId,EventTitle,EventDescription,Date,FromTime,Location,CreateDate,ToTime,NotificationStatus) VALUES(@UserId,@EventTitle,@EventDescription,@Date,@FromTime,@Location,@CreateDate,@ToTime,@NotificationStatus)";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                Command.Parameters.AddWithValue("@UserId", aEvent.UserId);
                Command.Parameters.AddWithValue("@EventTitle", aEvent.EventTitle);
                Command.Parameters.AddWithValue("@EventDescription", aEvent.EventDescription);
                Command.Parameters.AddWithValue("@Date", aEvent.Date);
                Command.Parameters.AddWithValue("@FromTime", aEvent.FromTime);
                Command.Parameters.AddWithValue("@Location", aEvent.Location);
                Command.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                Command.Parameters.AddWithValue("@ToTime", aEvent.ToTime);
                Command.Parameters.AddWithValue("@NotificationStatus", false.ToString());
                Count = Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return Count;
        }

        public List<Event> ShowEvent()
        {
          
           
            Event aEvent = new Event();

            List<Event> events = new List<Event>();
            try
            {

                Query = "SELECT * FROM tbl_Event WHERE Date>='" + DateTime.Now + "'  ORDER BY Date ASC ";

                Command = new SqlCommand(Query, Connection);

                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {

                    aEvent.EventId = (int)Reader["EventId"];
                    aEvent.UserId = (int)Reader["UserId"];
                    aEvent.EventTitle = Reader["EventTitle"].ToString();
                    aEvent.EventDescription = Reader["EventDescription"].ToString();
                    aEvent.Date = Reader["Date"].ToString();
                    aEvent.FromTime = Reader["FromTime"].ToString();
                    aEvent.ToTime = Reader["ToTime"].ToString();
                    aEvent.Location = Reader["Location"].ToString();
                    aEvent.EventCreateTime = Reader["CreateDate"].ToString();

                    events.Add(new Event()
                    {

                        EventId = (int)Reader["EventId"],
                        UserId = (int)Reader["UserId"],
                        EventTitle = Reader["EventTitle"].ToString(),
                        EventDescription = Reader["EventDescription"].ToString(),
                        Date = Reader["Date"].ToString(),
                        FromTime = Reader["FromTime"].ToString(),
                        ToTime = Reader["ToTime"].ToString(),
                        Location = Reader["Location"].ToString(),
                        EventCreateTime = Reader["CreateDate"].ToString()

                    });
                    //notifications.Add(new Notification() { NotificationId = (int)Reader["Id"], NotificationTitle = Reader["Title"].ToString(), NotificationDate = Reader["Date"].ToString() });
                }
                Reader.Close();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return events;

        }

        public List<Event> ShowRecentEvent()
        {
          string year= DateTime.Now.Year.ToString();
          string month = DateTime.Now.Month.ToString();
          string day = DateTime.Now.Day.ToString();
          int yearInt= Convert.ToInt32(year);
          int monthInt = Convert.ToInt32(month);
          int dayInt= Convert.ToInt32(day);
            Event aEvent = new Event();

            List<Event> events = new List<Event>();
            try
            {

                Query = "SELECT * from tbl_Event where month(Date)='" + monthInt + "' and Year(Date)='" + yearInt + "' and Date>'" + DateTime.Now + "'  ORDER BY Date ASC ";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {

                    aEvent.EventId = (int)Reader["EventId"];
                    aEvent.UserId = (int)Reader["UserId"];
                    aEvent.EventTitle = Reader["EventTitle"].ToString();
                    aEvent.EventDescription = Reader["EventDescription"].ToString();
                    aEvent.Date = Reader["Date"].ToString();
                    aEvent.FromTime = Reader["FromTime"].ToString();
                    aEvent.ToTime = Reader["ToTime"].ToString();
                    aEvent.Location = Reader["Location"].ToString();
                    aEvent.EventCreateTime = Reader["CreateDate"].ToString();

                    events.Add(new Event()
                    {
                        EventId = (int)Reader["EventId"],
                        UserId = (int)Reader["UserId"],
                        EventTitle = Reader["EventTitle"].ToString(),
                        EventDescription = Reader["EventDescription"].ToString(),
                        Date = Reader["Date"].ToString(),
                        FromTime = Reader["FromTime"].ToString(),
                        ToTime = Reader["ToTime"].ToString(),
                        Location = Reader["Location"].ToString(),
                        EventCreateTime = Reader["CreateDate"].ToString()

                    });
                    //notifications.Add(new Notification() { NotificationId = (int)Reader["Id"], NotificationTitle = Reader["Title"].ToString(), NotificationDate = Reader["Date"].ToString() });
                }
                Reader.Close();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return events;

        }

        public Event DetailEvent(int id)
        {
           
            Event aEvent = new Event();
            try
            {
                Query = "SELECT * FROM tbl_Event WHERE EventId=@EventId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                Command.Parameters.AddWithValue("@EventId",id);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    aEvent.EventId = (int)Reader["EventId"];
                    aEvent.UserId = (int)Reader["UserId"];
                    aEvent.EventTitle = Reader["EventTitle"].ToString();
                    aEvent.EventDescription = Reader["EventDescription"].ToString();
                    aEvent.Date = Reader["Date"].ToString();
                    aEvent.FromTime = Reader["FromTime"].ToString();
                    aEvent.ToTime = Reader["ToTime"].ToString();
                    aEvent.Location = Reader["Location"].ToString();
                    aEvent.EventCreateTime = Reader["CreateDate"].ToString();
                    //notifications.Add(new Notification() { NotificationId = (int)Reader["Id"], NotificationTitle = Reader["Title"].ToString(), NotificationDate = Reader["Date"].ToString() });
                }
                Reader.Close();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return aEvent;
        }

        public int DeleteEvent(int eventId)
        {
            try
            {
                Query = "Delete tbl_Event WHERE EventId=@EventId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                Command.Parameters.AddWithValue("@EventId",eventId);
                Count = Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return Count;

        }

        public int IsEventDateAndFromTimeExist(Event aEvent)
        {
            try
            {
                Query = "SELECT * FROM tbl_Event WHERE Date='" + aEvent.Date + "' AND FromTime BETWEEN '" + aEvent.FromTime + "' AND '" + aEvent.ToTime + "'";
                Command = new SqlCommand(Query, Connection);
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Connection.Close();
                    return 1;
                }
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int IsEventDateAndToTimeExist(Event aEvent)
        {
            try
            {
                Query = "SELECT * FROM tbl_Event WHERE Date='" + aEvent.Date + "' AND ToTime BETWEEN '" + aEvent.FromTime + "' AND '" + aEvent.ToTime + "' ";
                Command = new SqlCommand(Query, Connection);
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Connection.Close();
                    return 1;
                }
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public void GetEventNotificationBeforeDay()
        {
            Event aEvent = new Event();
            Notification notification = new Notification();
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            int yearInt = Convert.ToInt32(year);
            int monthInt = Convert.ToInt32(month);
            int dayInt = Convert.ToInt32(day)+1;
            try
            {
                Query = "select * from tbl_Event where year(Date)='" + yearInt + "' and month(Date)='" + monthInt + "' and day(Date)='" + dayInt + "' AND NotificationStatus!='" + true + "' ";
                Command = new SqlCommand(Query, Connection);
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {

                    aEvent.EventId = (int)Reader["EventId"];
                    aEvent.EventTitle = Reader["EventTitle"].ToString();
                    aEvent.EventDescription = Reader["EventDescription"].ToString();
                    aEvent.Date = Reader["Date"].ToString();
                    aEvent.FromTime = Reader["FromTime"].ToString();
                    aEvent.ToTime = Reader["ToTime"].ToString();
                    aEvent.Location = Reader["Location"].ToString();
                    UpdateEventNotificationStatus(aEvent.EventId);

                    notification.NotificationTitle = "Tomorrow will held an Event.Title:" + aEvent.EventTitle;
                    notification.NotificationDescription = "Tomorrow will held an Event." + "\n" + "Title: " + aEvent.EventTitle + ",\nDate: " + aEvent.Date + ",\nTime- From: " + aEvent.FromTime + ",To: " + aEvent.ToTime + ",\nLocation: " + aEvent.Location;
                    notification.NotificationDate = DateTime.Now.ToString();
                    notificationManager.SaveNotification(notification);
                    notificationManager.IncrementNotification();

                    foreach (var phone in registrationGetway.GetAllPhoneNo())
                    {
                        smsManager.SendNotificationToPhone(notification.NotificationDescription, phone.PhoneNo);
                    }
                    Connection.Open();
                    Reader = Command.ExecuteReader();
                }
                Reader.Close();
                Connection.Close();
            }
            catch (Exception)
            {

            }
        }
        public void UpdateEventNotificationStatus(int id)
        {
            try
            {
                Query = "Update tbl_Event SET NotificationStatus='" + true.ToString() + "' WHERE EventId='" + id + "'";
                Command = new SqlCommand(Query, Connection);
                Connection.Close();
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception)
            {

            }
        }
    }
}