using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeautyBusiness.DataAccess;
using BeautyBusiness.Models;

namespace BeautyBusiness.Controllers
{
    public class CustomAccountController : Controller
    {
        BookAppointmentService bas = new BookAppointmentService();
        // GET: CustomAccount
        public ActionResult Index()
        {
            if(Session["CustomerID"] !=null)
            {
                string cid = Session["CustomerID"].ToString();
                // string id=Request.QueryString["subsid"].ToString();
                int ccid = Convert.ToInt32(cid);
                
                
                
                
                //this is need to modify 
                //action ,  by passing customer id  
                // we will collect appointment id 
                // by appointment id , we will collect subserice for each appointpointmen t

       
                List<ServiceAppointment> sa = bas.getAppointmentServiceList(ccid);
                return View(sa);
            }
            else
            {
                return RedirectToAction("CustomerLogin", "Customer");
            }

            
        }
        public ActionResult CustomerLogout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("CustomerLogin", "Customer");
        }
    }
}