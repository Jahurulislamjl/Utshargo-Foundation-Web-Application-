using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using FinalProjectApplication.Models;
using System.Web.Security;

namespace FinalProjectApplication.Getway
{
    public class LoginGetway:DBConnection
    {
        public int SaveUser(User user)
        {
            Query = "INSERT INTO tb_Login(UserName,Email,Password,RuleId,VerificationStatus,VerificationCode,Phone) VALUES('" + user.UserName + "','" + user.Email + "','" + user.Password + "','" + user.Rule + "','" + user.VerificationStatus + "','" + user.VerificationCode + "','" + user.PhoneNo + "')";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Count = Command.ExecuteNonQuery();
            Connection.Close();
            return Count;
        }

        public int CheckUser(Login login)
        {
            try
            {
                

                Query = "SELECT * FROM tb_Login WHERE Email=@Email AND Password=@Password";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Close();
                Connection.Open();
                Command.Parameters.AddWithValue("@Email",login.Email);
                Command.Parameters.AddWithValue("@Password",login.Password);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    return 1;
                }

                Reader.Close();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int CheckUserName(Login login)
        {
            try
            {
                login.UserName = login.Email;

                Query = "SELECT * FROM tb_Login WHERE UserName=@UserName AND Password=@Password";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Close();
                Connection.Open();
                Command.Parameters.AddWithValue("@UserName", login.UserName);
                Command.Parameters.AddWithValue("@Password", login.Password);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {

                    return 1;
                }

                Reader.Close();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int CheckPhone(Login login)
        {
            try
            {

                login.PhoneNo = login.Email;
                Query = "SELECT * FROM tb_Login WHERE Phone=@Phone AND Password=@Password";
                
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                Command.Parameters.AddWithValue("@Phone", login.PhoneNo);
                Command.Parameters.AddWithValue("@Password", login.Password);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {

                    return 1;
                }

                Reader.Close();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int IsPasswordChanged(Login login)
        {
            try
            {

                Query = "SELECT * FROM tb_Login WHERE Phone=@Phone AND Password=@Password";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                Command.Parameters.AddWithValue("@Phone", login.PhoneNo);
                Command.Parameters.AddWithValue("@Password", login.Password);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {

                    return 1;
                }

                Reader.Close();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int VerifyAccount(string phoneNo)
        {
            try
            {

                Query = "UPDATE tb_Login SET VerificationStatus=@VerificationStatus WHERE Phone=@Phone";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                Command.Parameters.AddWithValue("@VerificationStatus", true);
                Command.Parameters.AddWithValue("@Phone",phoneNo);
                Count = Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return Count;
            

        }

        public int VerifyPhoneAndEmail(ForgetPassword forgetPassword)
        {
            try
            {
                Query = "SELECT * FROM tb_Login WHERE Phone='" + forgetPassword.Phone + "' AND Email='" + forgetPassword.UserName + "'";
                Command = new SqlCommand(Query, Connection);
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {

                    return 1;
                }

                Reader.Close();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int VerifyPhoneAndUserName(ForgetPassword forgetPassword)
        {
            try { 
            Query = "SELECT * FROM tb_Login WHERE Phone=@Phone AND UserName=@UserName";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Connection.Open();
            Command.Parameters.AddWithValue("@Phone",forgetPassword.Phone);
            Command.Parameters.AddWithValue("@UserName",forgetPassword.UserName);
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {

                return 1;
            }

            Reader.Close();
            Connection.Close();
            }
            catch (Exception)
            {

            }
            return 0;


        }

        public int SaveForgetPassword(string password,string phoneNo)
        {
            try
            {
                Query = "UPDATE tb_Login SET Password=@Password WHERE Phone=@Phone";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Close();
                Connection.Open();
                Command.Parameters.AddWithValue("@Password", password);
                Command.Parameters.AddWithValue("@Phone", phoneNo);
                Count = Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return Count;
        }

        public bool CheckVerifyAccountByEmail(Login login)
        {
            try { 
            Query = "SELECT * FROM tb_Login WHERE Email=@Email AND VerificationStatus=@VerificationStatus";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Connection.Open();
            Command.Parameters.AddWithValue("@Email", login.Email);
            Command.Parameters.AddWithValue("@VerificationStatus", false);
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {

                return false;
            }

            Reader.Close();
            Connection.Close();
            }
            catch (Exception)
            {

            }
            return true;
        }

        public bool CheckVerifyAccountByPhone(Login login)
        {
            try { 
            login.PhoneNo = "+88"+login.Email;

            Query = "SELECT * FROM tb_Login WHERE Phone=@Phone AND VerificationStatus=VerificationStatus";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Connection.Close();
            Connection.Open();
                Command.Parameters.AddWithValue("@Phone", login.Email);
                Command.Parameters.AddWithValue("@VerificationStatus", false);
                Reader = Command.ExecuteReader();
            while (Reader.Read())
            {

                return false;
            }

            Reader.Close();
            Connection.Close();
            }
            catch (Exception)
            {

            }
            return true;
        }

        public bool CheckVerifyAccountByUserName(Login login)
        {
            try { 
            Query = "SELECT * FROM tb_Login WHERE UserName=@UserName AND VerificationStatus=@VerificationStatus";
            Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
            Connection.Close();
            Connection.Open();
                Command.Parameters.AddWithValue("@UserName", login.Email);
                Command.Parameters.AddWithValue("@VerificationStatus", false);
                Reader = Command.ExecuteReader();
            while (Reader.Read())
            {

                return false;
            }

            Reader.Close();
            Connection.Close();
            }
            catch (Exception)
            {

            }
            return true;
        }

        public User GetSessionByEmail(Login login)
        {
            User user = new User();
            try { 
            Query = "select * from tb_User join tb_Login on tb_User.UserId=tb_Login.UserId WHERE tb_Login.Email='" + login.Email+"'";
            Command = new SqlCommand(Query, Connection);
            Connection.Close();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                user.UserId = (int)Reader["UserId"];
                user.Name = Reader["FullName"].ToString();
                user.UserName = Reader["UserName"].ToString();
                user.BloodGroup = Reader["BloodGroup"].ToString();
                user.PhoneNo= Reader["Phone"].ToString();
                user.Email= Reader["Email"].ToString();
                user.Password= Reader["Password"].ToString();
                user.Rule= (int)Reader["RuleId"];
              
            }

            Reader.Close();
            Connection.Close();
            }
            catch (Exception)
            {

            }
            return user;
        }

        public User GetSessionByPhone(Login login)
        {

            User user = new User();
            try { 

             Query = "select * from tb_User join tb_Login on tb_User.UserId=tb_Login.UserId WHERE tb_Login.Phone='" + login.Email+"'";
            Command = new SqlCommand(Query, Connection);
            Connection.Close();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                user.UserId = (int)Reader["UserId"];
                user.Name = Reader["FullName"].ToString();
                user.UserName = Reader["UserName"].ToString();
                user.BloodGroup = Reader["BloodGroup"].ToString();
                user.PhoneNo = Reader["Phone"].ToString();
                user.Password = Reader["Password"].ToString();
                user.Email = Reader["Email"].ToString();
                user.Rule = (int)Reader["RuleId"];

            }

            Reader.Close();
            Connection.Close();
            }
            catch (Exception)
            {

            }
            return user;
        }

        public User GetSessionByUserName(Login login)
        {
            User user = new User();
            try { 
            Query = "select * from tb_User join tb_Login on tb_User.UserId=tb_Login.UserId WHERE tb_Login.UserName='" + login.Email+"'";
            Command = new SqlCommand(Query, Connection);
            Connection.Close();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                user.UserId = (int)Reader["UserId"];
                user.Name = Reader["FullName"].ToString();
                user.UserName = Reader["UserName"].ToString();
                user.BloodGroup = Reader["BloodGroup"].ToString();
                user.PhoneNo = Reader["Phone"].ToString();
                user.Password = Reader["Password"].ToString();
                user.Email = Reader["Email"].ToString();
                user.Rule = (int)Reader["RuleId"];

            }

            Reader.Close();
            Connection.Close();
            }
            catch (Exception)
            {

            }
            return user;
        }

        public UpdateProfile UpdateProfile(int userId)
        {

            UpdateProfile user = new UpdateProfile();
            try
            {
                Query = "select * from tb_User join tb_Login on tb_User.UserId=tb_Login.UserId WHERE tb_Login.UserId='" + userId + "'";
                Command = new SqlCommand(Query, Connection);
                Connection.Close();
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    user.UserId = (int)Reader["UserId"];
                    user.Name = Reader["FullName"].ToString();
                    user.UserName = Reader["UserName"].ToString();
                    user.BloodGroup = Reader["BloodGroup"].ToString();
                    user.PhoneNo = Reader["Phone"].ToString();
                    user.Email = Reader["Email"].ToString();
                    user.Address = Reader["Address"].ToString();



                }

                Reader.Close();
                Connection.Close();
            } 
            catch (Exception)
            {

            }
            return user;
        }

        public int SaveUpdateProfile(UpdateProfile profile)
        {
            try
            {

                Query = "UPDATE tb_User SET FullName=@FullName, Address=@Address, BloodGroup=@BloodGroup WHERE UserId=@UserId  UPDATE tb_Login SET Email=Email , UserName=@UserName where UserId=@LUserId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                Command.Parameters.AddWithValue("@FullName", profile.Name);
                Command.Parameters.AddWithValue("@Address", profile.Address);
                Command.Parameters.AddWithValue("@BloodGroup", profile.BloodGroup);
                Command.Parameters.AddWithValue("@UserId", profile.UserId);
                Command.Parameters.AddWithValue("@Email", profile.Email);
                Command.Parameters.AddWithValue("@UserName", profile.UserName);
                Command.Parameters.AddWithValue("@LUserId", profile.UserId);
                
                Count = Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception)
            {

            }

            return Count;

        }

        public bool CheckPasswordForResetPassword(string password,int userId)
        {
            try { 
            Query = "SELECT * FROM tb_Login WHERE Password=@Password AND UserId=@UserId";
            Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
            Connection.Close();
            Connection.Open();
                Command.Parameters.AddWithValue("@Password", password);
                Command.Parameters.AddWithValue("@UserId",userId);
                Reader = Command.ExecuteReader();
            while (Reader.Read())
            {

                return true;
            }

            Reader.Close();
            Connection.Close();
            }
            catch (Exception)
            {

            }
            return false;
        }

        public int SaveResetPassword(string password,int userId)
        {
            try
            {
                Query = "UPDATE tb_Login SET Password=@Password WHERE UserId=@UserId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Close();
                Connection.Open();
                Command.Parameters.AddWithValue("@Password", password);
                Command.Parameters.AddWithValue("@UserId", userId);
                Count = Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return Count;

        }


        public bool CheckEmailExistForUpdateProfile(string email,int userId)
        {
            try
            {
                Query = "SELECT Email FROM tb_Login WHERE Email=@Email And UserId!=@Email";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Close();
                Connection.Open();
                Command.Parameters.AddWithValue("@Email", email);
                Command.Parameters.AddWithValue("@UserId", userId);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {

                    return false;

                }
                Reader.Close();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            
            return true;

        }

   

        public bool CheckUserNameExistForUpdateProfile(string userName,int userId)
        {
            try
            {
                Query = "SELECT UserName FROM tb_Login WHERE UserName=@UserName And UserId!=@UserId";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Close();
                Connection.Open();
                Command.Parameters.AddWithValue("@UserName", userName);
                Command.Parameters.AddWithValue("@UserId", userId);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {

                    return false;
                }
                Reader.Close();
                Connection.Close();
            }
            catch (Exception)
            {

            }
           
            return true;

        }

        public bool CheckPhoneExistForUpdateProfile(string phone, int userId)
        {
            try { 
            Query = "SELECT Phone FROM tb_Login WHERE Phone=@ And UserId!=@UserId";
            Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
            Connection.Close();
            Connection.Open();
                Command.Parameters.AddWithValue("@Phone", phone);
                Command.Parameters.AddWithValue("@UserId", userId);
                Reader = Command.ExecuteReader();
            while (Reader.Read())
            {

                return false;

            }
            Reader.Close();
            Connection.Close();
            }
            catch (Exception)
            {

            }
            return true;

        }

        public int UpdatePhoneNo(string phoneNo,int userId)
        {
            try { 

            Query = "UPDATE tb_Login SET Phone=@Phone WHERE UserId=@UserId";
            Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
            Connection.Open();
            Count = Command.ExecuteNonQuery();
                Command.Parameters.AddWithValue("@Phone", phoneNo);
                Command.Parameters.AddWithValue("@UserId", userId);
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return Count;


        }





    }
}