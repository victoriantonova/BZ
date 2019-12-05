using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeautyBusiness.Models;
using System.Data;
using System.Data.SqlClient;


namespace BeautyBusiness.DataAccess
{
    public class BookAppointmentService:Base
    {



        BeautyServices bs = new BeautyServices();

            public string getBookedAppointmentlist(string appstatus)
            {
                        string qry;
                       string output = string.Empty;
            SqlConnection con = new SqlConnection(conn);

                if(appstatus=="cancel")
                    {
                qry = @"select  app_Id,app_date,UserName,Timeslot,subservicename,cost,duration from ServiceAppointment 
                         inner join Service_booked on Service_booked.appointment_Id=ServiceAppointment.app_ID
                         inner join subservice_category on Service_booked.subservice_Id=subservice_category.subserviceID
                         inner join service_category on  service_category.serviceId=subservice_category.serviceid
                         inner join users on users.userid=serviceappointment.cust_ID 
                         inner join TimeSlot on ServiceAppointment.TimeslotId=Timeslot.timeslotID
                         where canceled is not null";
            }
                else
                    {
                qry = @"select  app_Id,app_date,UserName,Timeslot,subServiceId,subservicename,cost,duration from ServiceAppointment 
                         inner join Service_booked on Service_booked.appointment_Id=ServiceAppointment.app_ID
                         inner join subservice_category on Service_booked.subservice_Id=subservice_category.subserviceID
                         inner join service_category on  service_category.serviceId=subservice_category.serviceid
                         inner join users on users.userid=serviceappointment.cust_ID 
                         inner join TimeSlot on ServiceAppointment.TimeslotId=Timeslot.timeslotID
                         where canceled is null";
            }


            
            

            SqlDataAdapter adpt = new SqlDataAdapter(qry,con);
            DataSet ds = new DataSet();
            adpt.Fill(ds);

            if (appstatus == "cancel")
            {

                output = "<table class='table'><tr><th>ID</th><th>date</th><th> Service Name </th><th>Time Slot</th><th>cost</th><th> Duration </th><th> customer Name</th></tr> ";

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    output += "<tr><td>" + dr["app_Id"].ToString() + "</td><td>" + dr["app_date"].ToString() + "</td><td>" + dr["subservicename"].ToString() + "</td><td>" + dr["Timeslot"].ToString() + "</td><td>" + dr["cost"].ToString() + "</td><td>" + dr["duration"].ToString() + "</td><td>" + dr["UserName"].ToString() + "</td></tr>";
                }

            }
            else
            {
                output = "<table class='table'><tr><th>ID</th><th>date</th><th> Service Name </th><th>Time Slot</th><th>cost</th><th> Duration </th><th> customer Name</th><th>Is Service Done?</th></tr> ";

                foreach (DataRow dr in ds.Tables[0].Rows)
                {



                    output += "<tr><td>" + dr["app_Id"].ToString() + "</td><td>" + dr["app_date"].ToString() + "</td><td>" + dr["subservicename"].ToString() + "</td><td>" + dr["Timeslot"].ToString() + "</td><td>" + dr["cost"].ToString() + "</td><td>" + dr["duration"].ToString() + "</td><td>" + dr["UserName"].ToString() + "</td><td><button class='btn btn-info' id='servicedone"+dr["subServiceId"].ToString()+"' onclick='updateServicedone("+dr["app_Id"].ToString()+","+dr["subServiceId"].ToString()+")'>Service Completed</button></td></tr>";
                }

            }




            output += "</table>";
            






            return output;
            }

             public string servicedoneappointmentlist()
            {
            SqlConnection con = new SqlConnection(conn);
            string output = string.Empty;
            string qry = @"select  app_Id,app_date,UserName,Timeslot,subservicename,cost,duration from ServiceAppointment 
                         inner join Service_Provided on service_provided.appointment_Id=ServiceAppointment.app_ID
                         inner join subservice_category on Service_Provided.subservice_Id=subservice_category.subserviceID
                         inner join service_category on  service_category.serviceId=subservice_category.serviceid
                         inner join users on users.userid=serviceappointment.cust_ID 
                         inner join TimeSlot on ServiceAppointment.TimeslotId=Timeslot.timeslotID
                         where canceled is  null";
            SqlDataAdapter adpt = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            adpt.Fill(ds);

          

                output = "<table class='table'><tr><th>ID</th><th>date</th><th> Service Name </th><th>Time Slot</th><th>cost</th><th> Duration </th><th> customer Name</th></tr> ";

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    output += "<tr><td>" + dr["app_Id"].ToString() + "</td><td>" + dr["app_date"].ToString() + "</td><td>" + dr["subservicename"].ToString() + "</td><td>" + dr["Timeslot"].ToString() + "</td><td>" + dr["cost"].ToString() + "</td><td>" + dr["duration"].ToString() + "</td><td>" + dr["UserName"].ToString() + "</td></tr>";
                }
            output += "</table>";

            return output;

        }
            

            public void CancelAppointment(int appid,string reason)
            {
            SqlConnection con = new SqlConnection(conn);
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = con;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "update ServiceAppointment set canceled='yes', cancellation_reason='"+reason+"' where app_Id="+appid+"";
               
                con.Open();
                int status = scmd.ExecuteNonQuery();
                con.Close();
                //return status;
            }


        }

        public List<ServiceAppointment> getAppointmentServiceList(int cid)
            {

            List<ServiceAppointment> salist = new List<ServiceAppointment>();

            SqlConnection scon = new SqlConnection(conn);
            string qry = "select distinct app_Id,App_date,cust_Id,Timeslot,UserName,UserEmail,canceled  from ServiceAppointment  inner join Service_booked on ServiceAppointment.App_Id=Service_booked.Appointment_Id"
            +" inner join Timeslot on timeslot.TimeSlotId = ServiceAppointment.TimeslotId"
            +" inner join Users on users.userId = ServiceAppointment.cust_Id "
            +" where ServiceAppointment.cust_ID = "+cid+ "";
           
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText =qry;
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
                while (sdr.Read())
                {
                    ServiceAppointment sa = new ServiceAppointment();
                    sa.App_Id = Convert.ToInt32(sdr["app_Id"]);
                    sa.App_date = sdr["App_date"].ToString();
                    sa.timeslot = sdr["TimeSlot"].ToString();
                    sa.custId = cid;
                    string str= sdr["canceled"].ToString();
                    str = str.Trim();
                    sa.appintmentstatus = str;
                    sa.cust_Name = sdr["UserName"].ToString();
                    sa.cust_Email = sdr["UserEmail"].ToString();

                    List<ServiceCart> subservicelist = bs.getServiceCartList1(sa.App_Id);

                    sa.subservicecart = subservicelist;

                    salist.Add(sa);

                }

                if (sdr != null)
                {
                    sdr.Dispose();
                    sdr.Close();
                }
                scon.Close();
               
            }



            return salist;
            }

         public void BookedServicedone(int appid,int subsid)
        {
            SqlConnection scon = new SqlConnection(conn);

            string qry="select * from service_booked where appointment_id="+appid+" and subservice_Id="+subsid+"";
            SqlDataAdapter adpt = new SqlDataAdapter(qry, scon);
            DataSet ds = new DataSet();
            adpt.Fill(ds);



            
            scon.Open();


            try
            {
                SqlCommand objCmd1 = new SqlCommand("delete from service_Booked where subservice_Id=" + subsid + "", scon);
                objCmd1.ExecuteNonQuery();
            }
            catch(Exception ex)
            {

            }
            SqlCommand objCmd2 = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

               
                float p = float.Parse(dr["price"].ToString());
                objCmd2 = new SqlCommand("insert into service_provided(appointment_id,subservice_id,price) values("+appid+","+subsid+","+p+")", scon);
                objCmd2.ExecuteNonQuery();
            }
            scon.Close();
            

        }

            public void SaveAppointmentdb(string date, int timeslotid , int cid)
            {

            List<ServiceCart> lst = bs.getServiceCartList(cid);


            SqlConnection con = new SqlConnection(conn);
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = con;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "INSERT INTO ServiceAppointment(App_date,cust_Id,TimeslotId) VALUES(@App_date,@cust_Id,@TimeslotId)";
                scmd.Parameters.AddWithValue("@App_date", date);
                scmd.Parameters.AddWithValue("@cust_Id", cid);
               // scmd.Parameters.AddWithValue("@SubserviceId", s.subServiceId);
                scmd.Parameters.AddWithValue("@TimeslotId", timeslotid);
                con.Open();
                int status = scmd.ExecuteNonQuery();
                con.Close();
                //return status;
            }

            int App_Id = getLastAppointmentID();

            foreach (ServiceCart s in lst)
            {


                SqlConnection con1 = new SqlConnection(conn);
                using (SqlCommand scmd = new SqlCommand())
                {
                    scmd.Connection = con1;
                    scmd.CommandType = CommandType.Text;
                    scmd.CommandText = "INSERT INTO service_booked(appointment_id,subservice_id,price) VALUES(@appointment_id,@subservice_id,@price)";
                    scmd.Parameters.AddWithValue("@appointment_id", App_Id);
                    scmd.Parameters.AddWithValue("@subservice_id", s.subServiceId);
                    // scmd.Parameters.AddWithValue("@SubserviceId", s.subServiceId);
                    scmd.Parameters.AddWithValue("@price", s.Cost);
                    con1.Open();
                    int status = scmd.ExecuteNonQuery();
                    con1.Close();
                    //return status;
                }


                SqlConnection con2 = new SqlConnection(conn);
                using (SqlCommand scmd1 = new SqlCommand())
                {
                    scmd1.Connection = con2;
                    scmd1.CommandType = CommandType.Text;
                    scmd1.CommandText = "INSERT INTO SubserviceBookedappointment(subserviceid,subserviceName,cost,duration,appid) VALUES(@subservice_id,@subserviceName,@cost,@duration,@appointment_id)";
                    scmd1.Parameters.AddWithValue("@appointment_id", App_Id);
                    scmd1.Parameters.AddWithValue("@subservice_id", s.subServiceId);
                    scmd1.Parameters.AddWithValue("@subserviceName", s.SubServiceName);
                    scmd1.Parameters.AddWithValue("@cost", s.Cost);
                    scmd1.Parameters.AddWithValue("@duration", s.duration);
                    con2.Open();
                    int status = scmd1.ExecuteNonQuery();
                    con2.Close();


                }


            }



            //For deleting cart----
            SqlConnection con11 = new SqlConnection(conn);
            string sqldelete = "delete from  servicecart where customerid=" + cid + "";
            SqlCommand cmddelete = new SqlCommand(sqldelete, con11);
            con11.Open();
            cmddelete.ExecuteNonQuery();
            con11.Close();




        }

            public int getLastAppointmentID()
           {
            int appid = 0;
            SqlConnection scon = new SqlConnection(conn);
            
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT MAX(App_ID) AS AppID FROM ServiceAppointment";
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
                if (sdr.Read())
                {

                    appid = Convert.ToInt32(sdr["AppID"]);
                   
                }

                if (sdr != null)
                {
                    sdr.Dispose();
                    sdr.Close();
                }
                scon.Close();
                return appid;
            }





        }

            public List<Timeslot>  getTimeSlotList()
            {

            SqlConnection scon = new SqlConnection(conn);
            List<Timeslot> objTimeslotList = new List<Timeslot>();
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT * FROM TimeSlot";
                scon.Open();
                SqlDataReader sdr = scmd.ExecuteReader();
                while (sdr.Read())
                {
                    Timeslot objTimeslotMaster = new Timeslot();
                    objTimeslotMaster.timeslotId = Convert.ToInt32(sdr["timeslotId"]);
                    objTimeslotMaster.timeslot = sdr["timeslot"].ToString();
                  

                    objTimeslotList.Add(objTimeslotMaster);
                }

                if (sdr != null)
                {
                    sdr.Dispose();
                    sdr.Close();
                }
                scon.Close();
                return objTimeslotList.ToList(); ;
            }



        }






    }
}