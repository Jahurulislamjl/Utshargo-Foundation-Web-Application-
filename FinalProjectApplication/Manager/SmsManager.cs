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
namespace FinalProjectApplication.Manager
{
    public class SmsManager
    {
        public int SendPasswordToPhone(string password, string phone)
        {
            try
            {
                const string accountSid = "AC60f74728e755040e34bed253c0c30aa4";
                const string authToken = "79b654f8fd83aef6428290c85d978206";

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body:".\n Welcome to Utsorgo foundation\nYour password is:  "+password,
                    from: new Twilio.Types.PhoneNumber("+15752148775"),
                    to: new Twilio.Types.PhoneNumber(phone)
                );
            }
            catch (Exception)
            {
                return 0;
            }
         
            return 1;

        }


        public int SendNotificationToPhone(string notificationMessage, string phone)
        {
            try
            {
                const string accountSid = "AC60f74728e755040e34bed253c0c30aa4";
                const string authToken = "79b654f8fd83aef6428290c85d978206";

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: ".\nUtsorgo foundation\n" + notificationMessage,
                    from: new Twilio.Types.PhoneNumber("+15752148775"),
                    to: new Twilio.Types.PhoneNumber(phone)
                );
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
          

        }

        //public int SendNotificationToPhone(int code, string phone)
        //{
        //    try
        //    {
        //        const string accountSid = "AC60f74728e755040e34bed253c0c30aa4";
        //        const string authToken = "79b654f8fd83aef6428290c85d978206";

        //        TwilioClient.Init(accountSid, authToken);

        //        var message = MessageResource.Create(
        //            body: ".\n Welcome to Utsorgo foundation\nYour password is:  " + code.ToString(),
        //            from: new Twilio.Types.PhoneNumber("+15752148775"),
        //            to: new Twilio.Types.PhoneNumber(phone)
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //    return 1;

        //}

    }
}