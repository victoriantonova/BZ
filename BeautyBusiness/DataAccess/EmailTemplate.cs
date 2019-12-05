using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Net;

using System.Net.Mail;
using BeautyBusiness.Models;

namespace BeautyBusiness.DataAccess
{
    public class EmailTemplate:Base
    {


        public void sendbuklEmail(List<customer> cl)
        {
                
            foreach(customer c in cl)
            {
               // SendEmail(c);
            }
        

        }

        public void SendEmail(customer c,string sub, string msg)
        {

            using (MailMessage mm = new MailMessage("cariadnail123@gmail.com", c.UserEmail))
            {
                mm.Subject = sub;
                string body = msg;
                    /*"Hello " + c.UserName + ",";
                body += "<br /><br />Please click the following link to activate your account";
                body += "<br /><a href = '" + string.Format("{0}://{1}/Home/Activation/{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, "activationCode") + "'>Click here to activate your account.</a>";
                body += "<br /><br />Thanks"; */
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("cariadnail123@gmail.com", "loulou123");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }

        }


        //Generate activation code and email it to the customer
        public void SendActivationEmail(customer c)
        {
            // Guid activationCode = Guid.NewGuid();

           // SqlConnection scon = new SqlConnection(conn);
           // string constr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            string activationCode = Guid.NewGuid().ToString();
            using (SqlConnection con = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO UserActivation VALUES(@UserId, @ActivationCode)"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@UserId", c.userId);
                        cmd.Parameters.AddWithValue("@ActivationCode", activationCode);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }




            


            using (MailMessage mm = new MailMessage("cariadnail123@gmail.com", c.UserEmail))
            {
                mm.Subject = "Account Activation";
                string body = "Hello " + c.UserFirstName + ",";
                body += "<br /><br />Please click the following link to activate your account";
                body += "<br /><a href = '" + HttpContext.Current.Request.Url.AbsoluteUri.Replace("Customer/Signup", "Customer/Activation?ActivationCode=" + activationCode) + "'>Click here to activate your account.</a>";
                body += "<br /><br />Thanks";
                mm.IsBodyHtml = true;
                mm.Body = body;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("cariadnail123@gmail.com", "loulou123");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

                
            }
        }
}
}