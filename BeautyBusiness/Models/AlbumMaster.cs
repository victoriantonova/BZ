using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyBusiness.Models
{
    public class AlbumMaster
    {

        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public byte[] Image { get; set; }
        public int Userid { get; set; }
        public int SubServiceID { get; set; }



    }
}