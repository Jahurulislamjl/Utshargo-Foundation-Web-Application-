using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProjectApplication.Getway;
using FinalProjectApplication.Models;
using FinalProjectApplication.Manager;

namespace FinalProjectApplication.Manager
{
    public class EventManager
    {
        EventGetway eventGetway = new EventGetway();
        NotificationManager notificationManager = new NotificationManager();
        Notification notification = new Notification();
        RegistrationGetway registrationGetway = new RegistrationGetway();
        SmsManager smsManager = new SmsManager();
        
        public string SaveEvent(Event aEvent)
        {
            string message = "";
            int count = eventGetway.SaveEvent(aEvent);
            if (count > 0)
            {
                message = "Saved successfuly";
                notification.NotificationTitle = "Utsorgo foundation created "+aEvent.EventTitle+" event";
                notification.NotificationDescription ="\nUtsorgo foundation Created an Event."+"\n"+"Title: "+aEvent.EventTitle+",\nDate: "+aEvent.Date+",\nTime- From: "+aEvent.FromTime+",To: "+aEvent.ToTime+",\nLocation: "+aEvent.Location;
                notification.NotificationDate = DateTime.Now.ToString();
                notificationManager.SaveNotification(notification);
                notificationManager.IncrementNotification();

                foreach ( var phone in registrationGetway.GetAllPhoneNo())
                {
                    smsManager.SendNotificationToPhone(notification.NotificationDescription,phone.PhoneNo);
                    
                }
                
            }
            else
                message = "Don't saved";
            return message;
        }

        public List<Event> ShowEvent()
        {
            return eventGetway.ShowEvent();
        }
        public List<Event> ShowRecentEvent()
        {
            return eventGetway.ShowRecentEvent();
        }

        public Event DetailEvent(int id)
        {
            return (eventGetway.DetailEvent(id));
        }

        public string DeleteEvent(int eventId)
        {
            Event aEvent = new Event();
            aEvent = eventGetway.DetailEvent(eventId);
            int count= eventGetway.DeleteEvent(eventId);
            if (count > 0)
            {
                
                notification.NotificationTitle = "Utsorgo foundation Canceled " + aEvent.EventTitle + " event";
                notification.NotificationDescription = "\nUtsorgo foundation Canceled an Event." + "\n" + "Title: " + aEvent.EventTitle + ",\nDate: " + aEvent.Date + ",\nTime- From: " + aEvent.FromTime + ",To: " + aEvent.ToTime + ",\nLocation: " + aEvent.Location;
                notification.NotificationDate = DateTime.Now.ToString();
                notificationManager.SaveNotification(notification);
                notificationManager.IncrementNotification();

                foreach (var phone in registrationGetway.GetAllPhoneNo())
                {
                    smsManager.SendNotificationToPhone(notification.NotificationDescription, phone.PhoneNo);

                }
                return "Deleted Event";
            }
            return "";
           
        }

        public int IsEventDateAndFromTimeExist(Event aEvent)
        {
            return eventGetway.IsEventDateAndFromTimeExist(aEvent);

        }

        public int IsEventDateAndToTimeExist(Event aEvent)
        {
            return eventGetway.IsEventDateAndToTimeExist(aEvent);

        }

        public void GetEventNotificationBeforeDay()
        {
            eventGetway.GetEventNotificationBeforeDay();
        }
    }
}