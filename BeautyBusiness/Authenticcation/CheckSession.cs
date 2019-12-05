using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyBusiness.Authenticcation
{
    public class CheckSession
    {


        public Boolean IsAdminLogin()
        {

            if(HttpContext.Current.Session["AdminEmail"] !=null)
            {

                return true;
            }
            else
            {
                return false;
            }

        }

    }
}