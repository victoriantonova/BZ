
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeautyBusiness.Authenticcation;
using BeautyBusiness.DataAccess;
using BeautyBusiness.Models;

namespace BeautyBusiness.Controllers
{
    
    public class AdminController : Controller
    {
        UserDB db = new UserDB();
        BeautyServices Bs = new BeautyServices();
        BookAppointmentService bas = new BookAppointmentService();
        EmailTemplate E = new EmailTemplate();
        CheckSession cs = new CheckSession();
        // GET: test1
        //public ActionResult Index()
        //{
        //    if(cs.IsAdminLogin())
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
            
        //}

        //Manage Service
        public ActionResult serviceview()
        {
            List<Service> lst = Bs.GetServices();
            if (cs.IsAdminLogin())
            {
                return View(lst);
            }
            else
            {
                return RedirectToAction("Login");
            }                   
        }


        public string getServiceById(int id)
        {
            List<Service> lst = Bs.GetServices();
            string sname = string.Empty;
            foreach(Service s in lst)
            {
                    if(s.serviceId==id)
                    {
                    sname = s.serviceName;
                    break;
                    }
            }

            return sname;
        }


        public string getsubServiceById(int id)
        {
            string output = string.Empty;
            List<subService> lst = Bs.GetSubServices();
            string sname = string.Empty;
            foreach (subService s in lst)
            {
                if (s.subServiceId == id)
                {
                    output += s.SubServiceName + "," + s.Cost + "," + s.duration + "," + s.ServiceID +"," + s.subServiceId;
                    break;
                }
            }


            return output;
        }




        public string updateService(int sid, string sname)
        {
            Bs.updateService(sid, sname);

            return "";
        }


        //get service list 
        [HttpGet]
        public string getServiceList()
        {

            string output = string.Empty;
            List<Service> lst = Bs.GetServices();

            output = "<table class='table table-bordered table-inverse'><tr><th>ID</th><th>Name</th><th>Action</th><th>action</th></tr>";

            foreach (Service s in lst)
            {

                output += "<tr><td>" + s.serviceId + "</td><td>" + s.serviceName + "</td><td><button onclick='deleteservice(" + s.serviceId + ")' class='btn btn-info'>Delete</button>|<td><button onclick='editservice(" + s.serviceId + ")' class='btn btn-info'>Edit</button></td></tr>";
            }

            output += "</table>";

            return output;
            
        }


        public string Addsubservices(string subsname, float cost, float duration, int sid)
        {

            Bs.AddSubServices(subsname, cost, duration, sid);

            return "";
        }     
        public string editsubservices(int subsid,string subsname, float cost, float duration, int sid)
        {
            Bs.editSubServices(subsid, subsname, cost, duration, sid);

            return "";
        }
        //get Sub service list 
        [HttpGet]
        public string getSubservicelist()
        {
            string output = string.Empty;
            List<subService> lst = Bs.GetSubServices();

            output = "<table class='table table-bordered table-inverse'><tr><th>ID</th><th>Name</th><th>Cost</th><th>Duration</th><th>Action</th><th>Action</th></tr>";

            foreach (subService s in lst)
            {

                output += "<tr><td>" + s.subServiceId + "</td><td>" + s.SubServiceName + "</td><td>" + s.Cost + "</td><td>" + s.duration + "</td><td><button onclick='deleteSubservice("+s.subServiceId+","+s.ServiceID+ ")' class='btn btn-info'>Delete</button></td><td><button onclick='editSubservice(" + s.subServiceId + "," + s.ServiceID + ")' class='btn btn-info'>Edit</button></td></tr>";
            }

            output += "</table>";

            return output;
        }       
        public string getDeleteservice(int id)
        {
            Bs.getDeleteService(id);

            return "";
        }

        public string getDeleteSubservice(int id)
        {
            Bs.getDeleteSubService(id);

            return "";
        }


        // add Services 

         public string Addservices(string name)
        {

            Bs.AddServices(name);
            return "Service Added Successfully";
        }

        //Manage Customer 
        // get customer list
        public string getCustomerList()
        {
            string output = string.Empty;
            List<customer> lst = db.GetCustomerlist();
            string status = string.Empty;
            output = "<table class='table'><tr><th>ID</th><th>Name</th><th>Email</th><th>Contact</th><th>User Status</th><th>Action</th><th>select</th></tr>";

            foreach (customer s in lst)
            {
                if (s.UserStatus == 'A')
                {
                    status = "Active";
                }
                else
                {
                    status = "Inactive";
                }
                output += "<tr><td>" + s.userId + "</td><td>" + s.UserName + "</td><td>" + s.UserEmail + "</td><td>" + s.UserPhone + "</td><td><button class='btn btn-info' onclick='getchangestatus(" + s.userId + ")'>" + status + "</button></td><td><button onclick='getdelete(" + s.userId + ")' class='btn btn-info'>delete</button></td><td><input type='checkbox' name='sel[]' value='" + s.userId + "' id='sel' /></td></tr>";
            }

            output += "</table>";


            return output;
        }

        //delete customer 
        public string DeleteCustomer(int cid)
        {

            db.DeleteCustomer(cid);
            return "";
        }

        public string getChangeStatus(int cid)
        {
            db.ChangeStatus(cid);

            return "";
        }

        //Send Email 
        public string SendEmailtoCustomers(string sub, string msg, int uid)
        {

            customer c = db.GetCustomer(uid);

            E.SendEmail(c, sub, msg);

            return "";
        }


        //Manage Appointment 

        public ActionResult AppointmentView()
        {
            return View();
        }


        public string getBookedAppointmentlist()
        {
            return bas.getBookedAppointmentlist("N");
        }

        public string getCancelAppointmentlist()
        {

            return bas.getBookedAppointmentlist("cancel");
        }

        public string GetBookedServicedone(int id,int sid)
        {

            bas.BookedServicedone(id,sid);
            return "";
        }
        public string getServicedoneAppointmentlist()
        {
            return bas.servicedoneappointmentlist();
        }

        //Manage Galary 
        public ActionResult Galaryview()
        {


            if (cs.IsAdminLogin())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }


           // return View();
        }

        // Manage Customer 
        public ActionResult Customerview()
        {

            if (cs.IsAdminLogin())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }


           // return View();
        }

        public ActionResult Home()
        {


            if (cs.IsAdminLogin())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            //return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AdminLogin Login)
        {
            User u = new User();
            u.UserEmail = Login.Email;
            u.UserPwd = Login.Password;
            if (db.AdminValidate(u))
            {
                
                    Session["AdminEmail"] =Login.Email;                  
                    return RedirectToAction("Home", "Admin");
                
            }
            else
            {
                Session["adminerror"] = "Either admin Email or password is incorrect....";
                return RedirectToAction("Login");
            }

           
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear(); 

            return RedirectToAction("Login");
        }


    }
}