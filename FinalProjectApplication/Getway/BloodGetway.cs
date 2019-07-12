using FinalProjectApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace FinalProjectApplication.Getway
{
    public class BloodGetway:DBConnection
    {
        public int SendBloodRequest(Blood blood)
        {
            try
            {
                Query = "INSERT INTO tb_BloodRequest(UserId,Name,BloodGroup,Location,NeedDate,Time,Phone) VALUES(@UserId,@Name,@BloodGroup,@Location,@NeedDate,@Time,@Phone)";
                Command = new SqlCommand(Query, Connection);
                Command.Parameters.Clear();
                Connection.Open();
                Command.Parameters.AddWithValue("@UserId", blood.UserId);
                Command.Parameters.AddWithValue("@Name", blood.Name);
                Command.Parameters.AddWithValue("@BloodGroup", blood.BloodId);
                Command.Parameters.AddWithValue("@Location", blood.Location);
                Command.Parameters.AddWithValue("@NeedDate", blood.Date);
                Command.Parameters.AddWithValue("@Time", blood.Time);
                Command.Parameters.AddWithValue("@Phone", blood.PhoneNo);
                Count = Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception)
            {

            }
            return Count;
        }

        public List<User> GetBloodDonorByBloodGroup(string searchBlood)
        {
            User user = new User();
            List<User> users = new List<User>();
            try
            {
                
                Query = "select * from tb_User inner join tb_Login on tb_User.UserId=tb_Login.UserId where tb_User.BloodGroup='" + searchBlood + "'";
                SqlCommand Command = new SqlCommand(Query, Connection);

                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    user.Name = Reader["FullName"].ToString();
                    user.Address = Reader["Address"].ToString();
                    user.DateOfBirth = Reader["DateOfBirth"].ToString();
                    user.PhoneNo = Reader["Phone"].ToString();
                    user.BloodGroup = Reader["BloodGroup"].ToString();

                    users.Add(new User()
                    {
                        Name = Reader["FullName"].ToString(),
                        Address = Reader["Address"].ToString(),
                        DateOfBirth = Reader["DateOfBirth"].ToString(),
                        PhoneNo = Reader["Phone"].ToString(),
                        BloodGroup = Reader["BloodGroup"].ToString()
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


        public List<Blood> GetAllBloodRequest()
        {
            Blood blood = new Blood();
            List<Blood> bloods = new List<Blood>();
            try
            {
              
                Query = "select * from tb_BloodRequest where NeedDate>='" + DateTime.Now + "' order by NeedDate ASC";

                SqlCommand Command = new SqlCommand(Query, Connection);

                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    blood.BloodId = (int)Reader["Id"];
                    blood.RequiredBloodGroup = Reader["BloodGroup"].ToString();
                    blood.Date = Reader["NeedDate"].ToString();
                    blood.Time = Reader["Time"].ToString();
                    blood.Location = Reader["Location"].ToString();
                    blood.Name = Reader["Name"].ToString();
                    blood.PhoneNo = Reader["Phone"].ToString();


                    bloods.Add(new Blood()
                    {
                        BloodId = (int)Reader["Id"],
                        RequiredBloodGroup = Reader["BloodGroup"].ToString(),
                        Date = Reader["NeedDate"].ToString(),
                        Time = Reader["Time"].ToString(),
                        Name = Reader["Time"].ToString(),
                        PhoneNo = Reader["Phone"].ToString(),
                        Location = Reader["Location"].ToString(),

                    });

                }

                Reader.Close();
                Connection.Close();
            }
            catch(Exception)
            {

            }
       
            return bloods;

        }
    }
}