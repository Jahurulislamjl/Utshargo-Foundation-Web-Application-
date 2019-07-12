using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;
using FinalProjectApplication.Models;
using FinalProjectApplication.Getway;

namespace FinalProjectApplication.Manager
{
    public class RegistrationManager
    {
        RegistrationGetway registrationGetway = new RegistrationGetway();
        NotificationManager notificationManager = new NotificationManager();
        Notification notification = new Notification();
        SmsManager smsManager = new SmsManager();
        public string SaveUser(User user)
        {
            string message = "";
            int count = registrationGetway.SaveUser(user);
            if (count > 0)
            {
                message = "Saved";
                notification.NotificationTitle =user.Name+" signIn to Utsorgo foundation";
                notification.NotificationDate = DateTime.Now.ToString();
                notificationManager.SaveNotification(notification);
                notificationManager.IncrementNotification();
                foreach (var phone in registrationGetway.GetAllPhoneNo())
                {
                    smsManager.SendNotificationToPhone(notification.NotificationTitle, phone.PhoneNo);

                }



            }
            else
                message = "Don't saved";
            return message;
        }
        public bool IsEmailExist(string email)
        {
            bool isExist=registrationGetway.IsEmailExist(email);
            return isExist;
        }

        public bool IsUserNameExist(string userName)
        {
            bool isExist = registrationGetway.IsUserNameExist(userName);
            return isExist;
        }

        public bool IsPhoneExist(string phone)
        {
            bool isExist = registrationGetway.IsPhoneExist(phone);
            return isExist;
        }

        public bool CheckIsVerifyPhone(string phone)
        {
            bool isExist = registrationGetway.CheckIsPhoneVerify(phone);
            return isExist;
        }

       public int GetVerificationCode()
        {
            Random random = new Random();
            int value = random.Next(1001, 9999);
            return value;
        }

        public List<User> GetAllMemberList()
        {
            return registrationGetway.GetAllMemberList();
        }
        public int SendVerificationCode(int code,string phone)
        {
            try
            {
                const string accountSid = "AC60f74728e755040e34bed253c0c30aa4";
                const string authToken = "79b654f8fd83aef6428290c85d978206";

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: code.ToString(),
                    from: new Twilio.Types.PhoneNumber("+15752148775"),
                    to: new Twilio.Types.PhoneNumber(phone)
                );
            }
            catch (Exception)
            {
                return 2;
            }
            return 1;
         
        }
    }
}