using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyBusiness.Models
{
    public class subService
    {
        public int subServiceId { get; set; }
        public string SubServiceName { get; set; }
        public float Cost { get; set; }
        public float duration { get; set; }
        public int ServiceID { get; set; }
    }
}