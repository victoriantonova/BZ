using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyBusiness.Models
{
    public class ServiceAppointment
    {

        public int App_Id { get; set; }
        public string App_date { get; set; }
        public string timeslot { get; set; }
        public string appintmentstatus { get; set; }
        public int custId { get; set; }
        public string cust_Name { get; set; }
        public string cust_Email { get; set; }
        public List<ServiceCart> subservicecart { get; set; }
    }
}