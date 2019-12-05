using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeautyBusiness.Models;
using System.Data;
using System.Data.SqlClient;

namespace BeautyBusiness.DataAccess
{
    public class UserDB:Base
    {
        EmailTemplate e = new EmailTemplate();

        //Validation for user login 

        public string IsUSerActivate(string activationCode)
        {
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand("DELETE FROM UserActivation WHERE ActivationCode = @ActivationCode");

            SqlDataAdapter sda = new SqlDataAdapter();
            string str = string.Empty;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ActivationCode", activationCode);
            cmd.Connection = con;
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();
            if (rowsAffected == 1)
            {
                str = "Activation successful.";
           }
            else
            {
                str = "Invalid Activation code.";
            }
            return str;
        }

        public Boolean isactivated(int id)
        {
            SqlConnection con = new SqlConnection(conn);
            SqlDataAdapter adpt = new SqlDataAdapter("select * from UserActivation where UserId=" + id + "", con);
            DataSet ds = new DataSet();
            adpt.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            return true;

        }

        public Boolean AdminValidate(User u)
        {

            SqlConnection con = new SqlConnection(conn);
            //for checking  password is wrong 
            string sql2 = "select * from Users inner join roles on Users.UserRoleId=roles.roleID   where UserEmail='" + u.UserEmail + "' and UserPwd='" + u.UserPwd + "' and roles.roleId=1 ";
            SqlDataAdapter adpt2 = new SqlDataAdapter(sql2, con);
            DataSet ds2 = new DataSet();
            adpt2.Fill(ds2);

            if (ds2.Tables[0].Rows.Count <= 0)
            {
                return false;
            }

            return true; 
        }



        public int  UserValidate(User u)
        {

            //checking user exist or not 
            SqlConnection con = new SqlConnection(conn);
            string sql1="select * from  Users where UserEmail='"+u.UserEmail+"'";
            SqlDataAdapter adpt1 = new SqlDataAdapter(sql1, con);
            DataSet ds = new DataSet();
            adpt1.Fill(ds);
            if(ds.Tables[0].Rows.Count<0)
            {
                return 1;
            }


            //for checking  password is wrong 
            string sql2="select * from Users  where UserEmail='"+u.UserEmail+"' and UserPwd='"+u.UserPwd+"'";
            SqlDataAdapter adpt2 = new SqlDataAdapter(sql2, con);
            DataSet ds2 = new DataSet();
            adpt2.Fill(ds2);

            if(ds2.Tables[0].Rows.Count<=0)
            {
                return 2;
            }
            else
            {
                u.userId = Convert.ToInt32(ds2.Tables[0].Rows[0]["UserId"].ToString());
                u.UserStatus = Convert.ToChar(ds2.Tables[0].Rows[0]["UserStatus"].ToString());
            }


            //for checking activation link 

            SqlDataAdapter adpt3 = new SqlDataAdapter("select * from UserActivation where UserId=" + u.userId + "", con);
            DataSet ds3 = new DataSet();
            adpt3.Fill(ds3);

            if(ds3.Tables[0].Rows.Count>0)
            {
                return 3;
            }

            if(u.UserStatus!='A')
            {
                return 5;
            }


            return 4;
        
           
        }
        public Boolean CheckUser_Email(customer c)
        {
            string qry = string.Empty;

            qry="select * from Users where UserEmail='"+c.UserEmail+"'";

            SqlDataAdapter adpt = new SqlDataAdapter(qry, conn);
            DataSet ds = new DataSet();
            adpt.Fill(ds);

            if(ds.Tables[0].Rows.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }           
        }

        //get customer +"'";
      
        public customer GetCustomer(int id)
        {
            customer objcustomerMaster = new customer();
            SqlConnection scon = new SqlConnection(conn);
         
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT * FROM users inner join roles on users.UserRoleid=roles.roleid where roles.roleid=2 and users.userid="+id+"";
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
             if (sdr.Read())
                {
                   
                    objcustomerMaster.userId = Convert.ToInt32(sdr["Userid"]);
                    objcustomerMaster.UserName = sdr["UserName"].ToString();
                    objcustomerMaster.UserEmail = sdr["UserEmail"].ToString();
                    objcustomerMaster.UserPhone = sdr["Userphone"].ToString();
                    objcustomerMaster.UserStatus = Convert.ToChar(sdr["Userstatus"].ToString());


                   
                }

                if (sdr != null)
                {
                    sdr.Dispose();
                    sdr.Close();
                }
                scon.Close();
                return objcustomerMaster;
            }



        }


        //get Customer list
        public List<customer> GetCustomerlist()
        {

            SqlConnection scon = new SqlConnection(conn);
            List<customer> objcustomerList = new List<customer>();
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT * FROM users inner join roles on users.UserRoleid=roles.roleid where roles.roleid=2";
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
                while (sdr.Read())
                {
                    customer objcustomerMaster = new customer();
                    objcustomerMaster.userId = Convert.ToInt32(sdr["Userid"]);
                    objcustomerMaster.UserName = sdr["UserName"].ToString();
                    objcustomerMaster.UserEmail = sdr["UserEmail"].ToString();
                    objcustomerMaster.UserPhone = sdr["Userphone"].ToString();
                    objcustomerMaster.UserStatus = Convert.ToChar(sdr["Userstatus"].ToString());
                    

                    objcustomerList.Add(objcustomerMaster);
                }

                if (sdr != null)
                {
                    sdr.Dispose();
                    sdr.Close();
                }
                scon.Close();
                return objcustomerList.ToList(); ;
            }



        }

        public void DeleteCustomer(int id)
        {

            SqlConnection scon = new SqlConnection(conn);
            List<customer> objcustomerList = new List<customer>();
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "delete from users where userid="+id+"";
                scon.Open();
                scmd.ExecuteNonQuery();
                scon.Close();

            }

        }

        public void ChangeStatus(int id)
        {
            customer c = GetCustomer(id);

             if(c.UserStatus=='A')
            {
                SqlConnection scon = new SqlConnection(conn);
               // List<customer> objcustomerList = new List<customer>();
                using (SqlCommand scmd = new SqlCommand())
                {
                    scmd.Connection = scon;
                    scmd.CommandType = CommandType.Text;
                    scmd.CommandText = "update users set Userstatus='I' where userid=" + id + "";
                    scon.Open();
                    scmd.ExecuteNonQuery();
                    scon.Close();

                }
            }
             else
            {
                SqlConnection scon = new SqlConnection(conn);
                // List<customer> objcustomerList = new List<customer>();
                using (SqlCommand scmd = new SqlCommand())
                {
                    scmd.Connection = scon;
                    scmd.CommandType = CommandType.Text;
                    scmd.CommandText = "update users set Userstatus='A' where userid=" + id + "";
                    scon.Open();
                    scmd.ExecuteNonQuery();
                    scon.Close();

                }
            }

        }
        //customer Registration 
        public string CustomerRegistration(customer c)
        {
            string message = string.Empty;
            if (CheckUser_Email(c))
            {
                message = "Sorry this email address is taken please try again";
            }
            else
            {


                int userId = 0;

                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("CustomerRegistration"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@RoleId", c.RoleId);
                            cmd.Parameters.AddWithValue("@UserName", c.UserName);
                            cmd.Parameters.AddWithValue("@UserPwd", c.UserPwd);
                            cmd.Parameters.AddWithValue("@UserFirstName", c.UserFirstName);
                            cmd.Parameters.AddWithValue("@UserLastName", c.UserLastName);
                            cmd.Parameters.AddWithValue("@UserEmail", c.UserEmail);
                            cmd.Parameters.AddWithValue("@UserPhone", c.UserPhone);
                            cmd.Parameters.AddWithValue("@UserGender", c.Gender);
                            cmd.Parameters.AddWithValue("@UserStatus", c.UserStatus);
                            cmd.Connection = con;
                            con.Open();
                            userId = Convert.ToInt32(cmd.ExecuteScalar());
                            message = "please check your email for activation link";
                            con.Close();
                        }
                    }
                }

                c.userId = userId;

              //  string message = string.Empty;
                switch (userId)
                {
                    case -1:
                       
                        break;
                    case -2:
                      
                        break;
                    default:
                      
                        e.SendActivationEmail(c);
                        break;
                }


            }

            return message;

        }


    }
}