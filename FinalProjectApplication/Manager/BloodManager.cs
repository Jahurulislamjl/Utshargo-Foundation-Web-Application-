using FinalProjectApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProjectApplication.Getway;
using FinalProjectApplication.Controllers;


namespace FinalProjectApplication.Manager
{
    public class BloodManager
    {
        BloodGetway bloodGetway = new BloodGetway();
        Notification notification = new Notification();
        NotificationManager notificationManager = new NotificationManager();
        RegistrationGetway registrationGetway = new RegistrationGetway();
        SmsManager smsManager = new SmsManager();
        public string SendBloodRequest(Blood blood)
        {
            string message = "Request failed";
            if (bloodGetway.SendBloodRequest(blood) > 0)
            {
                
                message = "Successfully sent blood Request";
                notification.NotificationTitle = "Need "+blood.RequiredBloodGroup+" Blood";
                notification.NotificationDescription = "Required Blood Group: " + blood.RequiredBloodGroup + ",\n Location: " + blood.Location + ", \nDate: " + blood.Date + ",Time: " + blood.Time + ",\nContactNo: " + blood.PhoneNo;
                notification.NotificationDate = DateTime.Now.ToString();
                notificationManager.SaveNotification(notification);
               notificationManager.IncrementNotification();
                foreach (var phone in registrationGetway.GetAllPhoneNo())
                {
                    smsManager.SendNotificationToPhone(notification.NotificationDescription, phone.PhoneNo);

                }
                return message;
            }
            return message;
        }
        public List<User> GetBloodDonorByBloodGroup(string searchBlood)
        {
          return  bloodGetway.GetBloodDonorByBloodGroup(searchBlood);
        }
        public List<Blood> GetAllBloodRequest()
        {
            return bloodGetway.GetAllBloodRequest();
        }
    }
}