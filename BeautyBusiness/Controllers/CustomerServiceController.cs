using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeautyBusiness.DataAccess;
using BeautyBusiness.Models;

namespace BeautyBusiness.Controllers
{
    public class CustomerServiceController : Controller
    {
        BeautyServices bs = new BeautyServices();
        // GET: CustomerService
        public ActionResult Index()
        {
            return View();
        }
        public string getCustomerSubService(int id)
        {
           List<subService> lst=bs.GetSubServices(id);
            string output = string.Empty;
            output = "<tr><th> Service Name </th><th>Cost</th><th> Duration </th><th>Action </th> </t> ";
            foreach (subService s in lst)
            {

                output += @"<tr onmouseover = 'this.style.backgroundColor='#ffff66';' onmouseout = 'this.style.backgroundColor='#d4e3e5';' >";
                output += "<td>" + s.SubServiceName + " </td> <td> " + "£" + +s.Cost + " </td> <td> " + s.duration + "(hr/s)" + " </td><td><button  onclick='bookAppointment  (" + s.subServiceId + ")' >Book Appointment</button></td> </tr>";
            }
            return output;
        }






    }
}