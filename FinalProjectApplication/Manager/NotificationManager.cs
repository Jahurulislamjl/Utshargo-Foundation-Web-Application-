using FinalProjectApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProjectApplication.Getway;

namespace FinalProjectApplication.Manager
{
    
    public class NotificationManager
    {
        NotificationGetway notificationGetway = new NotificationGetway();

        public List<Notification> GetNotifications(Notification notification)
        {
          return  notificationGetway.GetNotifications(notification);
        }
        public void SaveNotification(Notification notification)
        {
            notificationGetway.SaveNotification(notification); 
        }

        public void IncrementNotification()
        {
            notificationGetway.IncrementNotification();
        }
        public void DoNotificationNull(int userId)
        {
            notificationGetway.NullNotification(userId);
        }

        public int ShowCount(int userId)
        {
           return notificationGetway.ShowCount(userId);
        }
        public Notification DeatailNotification(int id)
        {
            return (notificationGetway.DeatilNotification(id));
        }

        public void SaveUserToNotificationCounter()
        {
            notificationGetway.SaveUserToNotificationCounter();
        }
    }
}