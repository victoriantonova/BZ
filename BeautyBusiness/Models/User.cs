using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyBusiness.Models
{
    public class User
    {
        public int userId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public char UserGender { get; set; }
        public char UserStatus { get; set; }
   }
}