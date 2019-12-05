using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeautyBusiness.Models
{
    public class customer
    {

        
        public int userId { get; set; }

        public int RoleId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }


        [Required]
        [Display(Name = "User Password")]
        [DataType(DataType.Password)]
        public string UserPwd { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string UserFirstName { get; set; }


        [Required]
        [Display(Name = "Last Name")]
        public string UserLastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }


        [Required]
        [Display(Name = "contact Number")]
        [DataType(DataType.PhoneNumber)]
        public string UserPhone { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        public char UserStatus { get; set; }






    }
}