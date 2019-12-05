using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeautyBusiness.Models;
using BeautyBusiness.DataAccess;

namespace BeautyBusiness.Controllers
{
    public class CustomerController : Controller
    {
        UserDB d = new UserDB();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CustomerLogin()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult CustomerLogin(customerlogin cl)
        {
            
            User u = new User();
            u.UserEmail = cl.Email;
            u.UserPwd = cl.Password;

            int choice = d.UserValidate(u);


            switch(choice)
            {
                case 1:
                    //for user does not exist ;
                    Session["error"] = "User does not exist";
                    break;
                case 2:
                    //for user's email or password is incorrect ;
                    Session["error"] = "Incorrect Password or Email please try again";
                    break;
                case 3:
                    Session["error"] = "Your account has not been activated, please check your email";
                    //for user's not activating link 
                    break;
                case 5:
                    Session["error"] = "User is inactive.....contact Admin  Thanks.";
                    //for user's not activating link 
                    break;
                default:                                        
                        Session["CustomerID"] = u.userId;                        
                        return RedirectToAction("Index", "CustomerService");                                      
            }
        
            return RedirectToAction("CustomerLogin");               
        }

        //Registration
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(customer c)
        {        
            c.RoleId = 2;
            c.UserStatus = 'A';
            if (c.Gender== "Male")
            {
                c.Gender = "m";
            }
            else
            {
                c.Gender = "F";
            }

            string str=d.CustomerRegistration(c);

            if (string.IsNullOrEmpty(str))
            {
                return RedirectToAction("CustomerLogin");
            }
            else
            {
                Session["strmsg"] = str;
                return View();               
            }
        }

        public ActionResult Activation(string activationcode)
        {
  string activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();

           string str= d.IsUSerActivate(activationCode);

            Session["msg"] = str;

            return View();
        }
    }
}