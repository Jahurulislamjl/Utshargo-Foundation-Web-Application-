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
using System.Text;

namespace FinalProjectApplication.Manager
{
    public class LoginManager
    {
        LoginGetway loginGetway = new LoginGetway();
        public string SaveUser(User user)
        {
            string message = "";
            int count = loginGetway.SaveUser(user);
            if (count > 0)
                message = "Saved";
            else
                message = "Don't saved";
            return message;
        }

        public int CheckUser(Login login)
        {
            
            int count=loginGetway.CheckUser(login);
            return count;
            
        }
        public int CheckUserName(Login login)
        {

            int count = loginGetway.CheckUserName(login);
            return count;

        }
        public int CheckPhone(Login login)
        {

            int count = loginGetway.CheckPhone(login);
            return count;

        }

        public int IsPasswordChanged(Login login)
        {

            int count = loginGetway.IsPasswordChanged(login);
            return count;

        }

        public int VerifyAccount(string phoneNo)
        {

            int count = loginGetway.VerifyAccount(phoneNo);
            return count;

        }

        public int VerifyPhoneAndEmail(ForgetPassword forgetPassword)
        {
          return  loginGetway.VerifyPhoneAndEmail(forgetPassword);
        }

        public int VerifyPhoneAndUserName(ForgetPassword forgetPassword)
        {
          return  loginGetway.VerifyPhoneAndUserName(forgetPassword);
        }

        public bool CheckEmailExistForUpdateProfile(string email,int UserId)
        {
          return  loginGetway.CheckEmailExistForUpdateProfile(email,UserId);
        }

        public bool CheckIsUserNameExistForUpdateProfile(string userName,int UserId)
        {
          return  loginGetway.CheckUserNameExistForUpdateProfile(userName,UserId);
        }
        public bool CheckPhoneExistForUpdateProfile(string phone, int userId)
        {
            return loginGetway.CheckPhoneExistForUpdateProfile(phone,userId);
        }
        public string UpdatePhoneNo(string phoneNo,int userId)
        {
            if (loginGetway.UpdatePhoneNo(phoneNo, userId)>0)
            {
                return "Phone number Updated successfuly";
            }

            return "Failed";
        }


        //public int GetPassword()
        //{
        //    Random random = new Random();
        //    int value = random.Next(10000001,99999999 );
        //    return value;
        //}

        public string GetPassword()
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            int length = 8;
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public int SaveForgetPassword(string phoneNo, string password)
        {
           return loginGetway.SaveForgetPassword(phoneNo,password);
        }

        public bool CheckVerifyAccountByEmail(Login login)
        {
            return loginGetway.CheckVerifyAccountByEmail(login);
        }

        public bool CheckVerifyAccountByPhone(Login login)
        {
            return loginGetway.CheckVerifyAccountByPhone(login);
        }

        public bool CheckVerifyAccountByUserName(Login login)
        {
            return loginGetway.CheckVerifyAccountByUserName(login);
        }
        public User GetSessionByEmail(Login login)
        {
            return loginGetway.GetSessionByEmail(login);
        }

        public User GetSessionByPhone(Login login)
        {
            return loginGetway.GetSessionByPhone(login);
        }

        public User GetSessionByUserName(Login login)
        {
            return loginGetway.GetSessionByUserName(login);
        }
        public UpdateProfile UpdateProfile(int userId)
        {
            return loginGetway.UpdateProfile(userId);
        }
        public string SaveUpdateProfile(UpdateProfile profile)
        {
            if (loginGetway.SaveUpdateProfile(profile)>0)
            {
                return "Updated Profile";
            }
            return "Failed to Update";
        }

        public bool CheckPasswordForResetPassword(string password, int userId)
        {
            return loginGetway.CheckPasswordForResetPassword(password,userId);
        }

        public string SaveResetPassword(string password, int userId)
        {
             
            if (loginGetway.SaveResetPassword(password,userId)> 0)
            {
                return "Successfully Reset Password";
            }
            return "Failed";
        }


    }
}