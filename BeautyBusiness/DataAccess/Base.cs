using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace BeautyBusiness.DataAccess
{
    public class Base
    {

        protected string conn;
        public  Base()
        {
            conn = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        }

        

    }
}