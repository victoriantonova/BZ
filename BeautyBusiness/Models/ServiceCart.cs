using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyBusiness.Models
{
    public class ServiceCart
    {
        public int CartID { get; set; }
        public int subServiceId { get; set; }
        public string SubServiceName { get; set; }
        public float Cost { get; set; }
        public float duration { get; set; }
        public int customerId { get; set; }
       
    }
}