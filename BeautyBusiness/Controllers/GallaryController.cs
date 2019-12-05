using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeautyBusiness.DataAccess;
using BeautyBusiness.Models;

namespace BeautyBusiness.Controllers.Admin
{
    public class GallaryController : Controller
    {

        AlbumService alb = new AlbumService();
        // GET: Gallary
        public ActionResult Index()
        {
            List<GallaryView> lst = alb.GetGallaryViewList();
            return View("GallaryView",lst);
        }

        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = alb.GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

    }
}