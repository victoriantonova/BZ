using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeautyBusiness.DataAccess;
using BeautyBusiness.Models;


namespace BeautyBusiness.Controllers
{
    public class BookAppointmentController : Controller
    {
        BeautyServices bs = new BeautyServices();
        BookAppointmentService bas = new BookAppointmentService();

        // GET: BookAppointment
        public ActionResult Index(string id)
        {

            

            if (Session["CustomerID"] != null)
            {
                string cid = Session["CustomerID"].ToString();
                // string id=Request.QueryString["subsid"].ToString();
                int ccid = Convert.ToInt32(cid);
                int subsid = Convert.ToInt32(id);

                if (subsid != 0)
                {
                    bs.Addtocart(subsid, ccid);
                }

              
                List<ServiceCart> s = bs.getServiceCartList(ccid);
                return View(s);
            }
            else
            {
                return RedirectToAction("CustomerLogin", "Customer");
            }
            
        }


        public string CancelAppointment(int appid,string r)
        {
            bas.CancelAppointment(appid, r);
            return "";
        }

        public ActionResult RemoveCartService(int id)
        {
            bs.RemoveCartService(id);
            return RedirectToAction("Index", new { id = "0" });
            
        }

        //get Time slot 
        public string getTimeSlot()
        {
            List<Timeslot> lst = bas.getTimeSlotList();
            string output = string.Empty;
        output = "<div class='form-group'>";
            output+="<label class='col-md-2 control-label'>Time Slots</label>";        
          output+="<div class='col-md-10'>";
            foreach (Timeslot t in lst)
            {
 output += "<label class='checkbox-inline'><input name='radio' value='"+t.timeslotId+"' type='checkbox'>"+t.timeslot+"</label>";



            }

            output += "</div></div>";

            return output;
        }
        public string SaveAppointment(string adate,int tid)
        {

            string id = Session["CustomerID"].ToString();
            int cid = Convert.ToInt32(id);
            bas.SaveAppointmentdb(adate, tid, cid);

            return "";
        }
    }
}