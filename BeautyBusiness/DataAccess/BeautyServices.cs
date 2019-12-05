using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeautyBusiness.Models;
using System.Data;
using System.Data.SqlClient;
namespace BeautyBusiness.DataAccess
{
    public class BeautyServices:Base
    {                        
        public List<Service>  GetServices()
        {
            SqlConnection scon = new SqlConnection(conn);
            List<Service> objServiceList = new List<Service>();
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT * FROM Service_category";
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
                while (sdr.Read())
                {
                    Service objServiceMaster = new Service();
                    objServiceMaster.serviceId = Convert.ToInt32(sdr["serviceId"]);
                    objServiceMaster.serviceName = sdr["serviceName"].ToString();                   
                    objServiceList.Add(objServiceMaster);
                }
                if (sdr != null)
                {
                    sdr.Dispose();
                    sdr.Close();
                }
                scon.Close();
                return objServiceList.ToList(); ;
            }
        }

        public void updateService(int id, string name)
        {

            SqlConnection con = new SqlConnection(conn);
            string qry = "update service_category set serviceName='"+name+"' where serviceId="+id+"";

            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

            }

        }

        public void AddSubServices(string subsname, float cost, float duration, int sid)
        {

            SqlConnection con = new SqlConnection(conn);
            string qry = "insert into subService_category(subserviceName,cost,duration,serviceid)values('" + subsname + "','" + cost + "','" + duration + "'," + sid + ")";

            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

            }

        }

        public void editSubServices(int subsid,string subsname, float cost, float duration, int sid)
        {

            SqlConnection con = new SqlConnection(conn);
            string qry = "update subService_category  set subserviceName='" + subsname + "',cost='" + cost + "',duration='" + duration + "',serviceid=" + sid + "   where subserviceid="+ subsid + "";

            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

            }

        }









        public void AddServices(string name)
        {

            SqlConnection con = new SqlConnection(conn);
            string qry = "insert into Service_category(serviceName)values('"+name+"')";

            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

            }

        }

        public void getDeleteSubService(int id)
        {
            SqlConnection con = new SqlConnection(conn);
            string qry = "delete from SubService_category where SubServiceID=" + id + "";

            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

            }
        }

        public void getDeleteService(int id)
        {
            SqlConnection con = new SqlConnection(conn);
            string qry = "delete from Service_category where ServiceID=" + id + "";

            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

            }
        }


        public void RemoveCartService(int id)
        {
            SqlConnection con = new SqlConnection(conn);
            string qry = "delete from ServiceCart where CartId="+id+"";

            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

            }

        }
        public void Addtocart(int id,int cid)
        {
            subService s = GetSubServiceById(id);
            if (getServiceCart(id))
            {

            }
            else
            {
                SqlConnection con = new SqlConnection(conn);
                string qry = "insert into ServiceCart(SubServiceId,Name,cost,duration,customerId)values(" + s.subServiceId + ",'" + s.SubServiceName + "'," + s.Cost + "," + s.duration + "," + cid + ")";

                try
                {
                    SqlCommand cmd = new SqlCommand(qry, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception e)
                {

                }
            }
        }
        public void DeleteCart(int cid)
        {


            SqlConnection con = new SqlConnection(conn);
            string qry = "delete from ServiceCart where CustomerId="+cid+"";

            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

            }

        }
        public Boolean  getServiceCart(int id)
        {

            SqlConnection scon = new SqlConnection(conn);
            ServiceCart objServiceCartMaster = new ServiceCart();
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT * FROM ServiceCart where subServiceId=" + id + "";
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
                if (sdr.Read())
                {

                    return true;


                }

                if (sdr != null)
                {
                    sdr.Dispose();
                    sdr.Close();
                }
                scon.Close();
                return false;
            }






        }

        public List<ServiceCart> getServiceCartList1(int cid)
        {

            SqlConnection scon = new SqlConnection(conn);
            List<ServiceCart> objServiceCartList = new List<ServiceCart>();
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT * FROM SubserviceBookedappointment where appid=" + cid + "";
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
                while (sdr.Read())
                {
                    ServiceCart objServicecartMaster = new ServiceCart();
                    objServicecartMaster.CartID = Convert.ToInt32(sdr["subservicebookid"]);
                    objServicecartMaster.subServiceId = Convert.ToInt32(sdr["subServiceId"]);
                    objServicecartMaster.SubServiceName = sdr["subserviceName"].ToString();
                    objServicecartMaster.Cost = float.Parse(sdr["Cost"].ToString());
                    objServicecartMaster.duration = float.Parse(sdr["duration"].ToString());
                    //objServicecartMaster.customerId = Convert.ToInt32(sdr["Customerid"]);

                    objServiceCartList.Add(objServicecartMaster);
                }

                if (sdr != null)
                {
                    sdr.Dispose();
                    sdr.Close();
                }
                scon.Close();
                return objServiceCartList.ToList(); ;

            }
        }



        public List<ServiceCart> getServiceCartList(int cid)
        {

            SqlConnection scon = new SqlConnection(conn);
            List<ServiceCart> objServiceCartList = new List<ServiceCart>();
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT * FROM ServiceCart";
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
                while (sdr.Read())
                {
                    ServiceCart objServicecartMaster = new ServiceCart();
                    objServicecartMaster.CartID= Convert.ToInt32(sdr["cartid"]);
                    objServicecartMaster.subServiceId = Convert.ToInt32(sdr["subServiceId"]);
                    objServicecartMaster.SubServiceName = sdr["Name"].ToString();
                    objServicecartMaster.Cost = float.Parse(sdr["Cost"].ToString());
                    objServicecartMaster.duration = float.Parse(sdr["duration"].ToString());
                    objServicecartMaster.customerId = Convert.ToInt32(sdr["customerid"]);


                    objServiceCartList.Add(objServicecartMaster);
                }

                if (sdr != null)
                {
                    sdr.Dispose();
                    sdr.Close();
                }
                scon.Close();
                return objServiceCartList.ToList(); ;

            }
        }
        public List<subService> GetSubServices()
        {

            SqlConnection scon = new SqlConnection(conn);
            List<subService> objSubServiceList = new List<subService>();
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT * FROM SubService_category";
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
                while (sdr.Read())
                {
                    subService objsubServiceMaster = new subService();
                    objsubServiceMaster.subServiceId = Convert.ToInt32(sdr["subServiceId"]);
                    objsubServiceMaster.SubServiceName = sdr["SubServiceName"].ToString();
                    objsubServiceMaster.Cost = float.Parse(sdr["Cost"].ToString());
                    objsubServiceMaster.duration = float.Parse(sdr["duration"].ToString());
                    objsubServiceMaster.ServiceID = Convert.ToInt32(sdr["ServiceID"]);

                    objSubServiceList.Add(objsubServiceMaster);
                }

                if (sdr != null)
                {
                    sdr.Dispose();
                    sdr.Close();
                }
                scon.Close();
                return objSubServiceList.ToList(); ;
            }



        }
        public List<subService> GetSubServices(int id)
        {

            SqlConnection scon = new SqlConnection(conn);
            List<subService> objSubServiceList = new List<subService>();
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT * FROM SubService_category where serviceId="+id+"";
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
                while (sdr.Read())
                {
                    subService objsubServiceMaster = new subService();
                    objsubServiceMaster.subServiceId = Convert.ToInt32(sdr["subServiceId"]);
                    objsubServiceMaster.SubServiceName = sdr["SubServiceName"].ToString();
                    objsubServiceMaster.Cost = float.Parse(sdr["Cost"].ToString());
                    objsubServiceMaster.duration = float.Parse(sdr["duration"].ToString());
                    objsubServiceMaster.ServiceID = Convert.ToInt32(sdr["ServiceID"]);

                    objSubServiceList.Add(objsubServiceMaster);
                }

                if (sdr != null)
                {
                    sdr.Dispose();
                    sdr.Close();
                }
                scon.Close();
                return objSubServiceList.ToList(); ;
            }



        }



        public subService GetSubServiceById(int id)
        {

            SqlConnection scon = new SqlConnection(conn);
            subService objsubServiceMaster = new subService();
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT * FROM SubService_category where subServiceId=" + id + "";
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
                if (sdr.Read())
                {
                
                    objsubServiceMaster.subServiceId = Convert.ToInt32(sdr["subServiceId"]);
                    objsubServiceMaster.SubServiceName = sdr["SubServiceName"].ToString();
                    objsubServiceMaster.Cost = float.Parse(sdr["Cost"].ToString());
                    objsubServiceMaster.duration = float.Parse(sdr["duration"].ToString());
                    objsubServiceMaster.ServiceID = Convert.ToInt32(sdr["ServiceID"]);

                   
                }

                if (sdr != null)
                {
                    sdr.Dispose();
                    sdr.Close();
                }
                scon.Close();
                return objsubServiceMaster;
            }



        }
    }
}