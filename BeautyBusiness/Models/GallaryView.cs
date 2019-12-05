using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyBusiness.Models
{
    public class GallaryView
    {      
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public byte[] Image { get; set; }
        public int SubServiceId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string SubServiceName { get; set; }

    }
}