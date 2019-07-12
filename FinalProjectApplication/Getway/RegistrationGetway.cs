using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProjectApplication.Models;
using System.Data.SqlClient;

namespace FinalProjectApplication.Getway
{
    public class RegistrationGetway : DBConnection
    {
        public int SaveUser(User user)
        {
            try
            {
                Query = "INSERT INTO tb_User(FullName,BloodGroup,Address,DateOfBirth) VALUES(@FullName,@BloodGroup,@Address,@DateOfBirth)  INSERT INTO tb_Login(UserName,Email,Password,RuleId,VerificationStatus,VerificationCode,Phone) VALUES(@UserName,@Email,@Password,@RuleId,@VerificationStatus,@VerificationCode,@Phone)";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                Command.Parameters.AddWithValue("@FullName", user.Name);
                Command.Parameters.AddWithValue("@BloodGroup", user.BloodGroup);
                Command.Parameters.AddWithValue("@Address", user.Address);
                Command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                Command.Parameters.AddWithValue("@UserName", user.UserName);
                Command.Parameters.AddWithValue("@Email", user.Email);
                Command.Parameters.AddWithValue("@Password", user.Password);
                Command.Parameters.AddWithValue("@RuleId", user.Rule);
                Command.Parameters.AddWithValue("@VerificationStatus", user.VerificationStatus);
                Command.Parameters.AddWithValue("@VerificationCode", user.VerificationCode);
                Command.Parameters.AddWithValue("@Phone", user.PhoneNo);
                Count = Command.ExecuteNonQuery();
                Connection.Close();
                
            }
            catch (Exception)
            {

            }
            return Count;
        }

        public bool IsEmailExist(string email)
        {
            try
            {
                Query = "SELECT Email FROM tb_Login WHERE Email=@Email";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                Command.Parameters.AddWithValue("@Email", email);
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

        public bool IsUserNameExist(string userName)
        {
            try
            {
                Query = "SELECT UserName FROM tb_Login WHERE UserName=@UserName";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                Command.Parameters.AddWithValue("@UserName", userName);
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

        public bool IsPhoneExist(string phone)
        {
            try
            {
                Query = "SELECT * FROM tb_Login WHERE Phone=@Phone";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                Command.Parameters.AddWithValue("@Phone", phone);
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

        public bool CheckIsPhoneVerify(string phone)
        {
            try
            {
                Query = "SELECT * FROM tb_Login WHERE Phone=@Phone AND VerificationStatus=@VerificationStatus";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Close();
                Connection.Open();
                Command.Parameters.AddWithValue("@Phone", phone);
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Connection.Close();
                    return false;
                }
                Reader.Close();
                Connection.Close();
            }
            catch (Exception )
            {
                
            }
            return true;

        }
        public List<User> GetAllPhoneNo()
        {
            User user = new User();
            List<User> users = new List<User>();
            try
            {
               
                Query = "SELECT Phone FROM tb_Login";
                Command = new SqlCommand(Query, Connection);
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    user.PhoneNo = Reader["Phone"].ToString();
                    users.Add(new User()
                    {
                        PhoneNo = Reader["Phone"].ToString()

                    });
                }
                Reader.Close();
                Connection.Close();
                
            }
            catch (Exception)
            {

            }
            return users;

        }

        public List<User> GetAllMemberList()
        {
            User user = new User();
            List<User> users = new List<User>();
            try
            {
                Query = "select * from tb_User join tb_Login on tb_User.UserId=tb_Login.UserId";
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
                    user.Password = Reader["Password"].ToString();
                    user.Address = Reader["Address"].ToString();
                    // user.Rule = (int)Reader["RuleId"];
                    users.Add(
                        new User()
                        {
                            UserId = (int)Reader["UserId"],
                            Name = Reader["FullName"].ToString(),
                            UserName = Reader["UserName"].ToString(),
                            BloodGroup = Reader["BloodGroup"].ToString(),
                            PhoneNo = Reader["Phone"].ToString(),
                            Address = Reader["Address"].ToString(),
                            Email = Reader["Email"].ToString(),
                        //Rule = (int)Reader["RuleId"]

                    }
                        );

                }

                Reader.Close();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return users;
        }

    }
}