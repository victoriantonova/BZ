using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeautyBusiness.DataAccess;
using BeautyBusiness.Models;
using System.IO;

namespace BeautyBusiness.Controllers.Admin
{
    public class AdminAlbumsController : Controller
    {

        AlbumService objAlbumModelService = new AlbumService();
        BeautyServices bs = new BeautyServices();
        // GET: AdminAlbums
        public ActionResult Index()
        {

            ViewBag.total = objAlbumModelService.GetAlbums().ToList().Count;
            return View(objAlbumModelService.GetAlbums().ToList());

           // return View();
        }
        [HttpGet]
        public ActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddImage(FormCollection collection)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            AlbumMaster objAlbumMaster = new AlbumMaster();
            objAlbumMaster.ImageName = collection["ImageName"].ToString();
            objAlbumMaster.SubServiceID = Convert.ToInt32(collection["subservicelist"].ToString());
            objAlbumMaster.Image = ConvertToBytes(file);
            objAlbumModelService.UploadAlbums(objAlbumMaster);
            return RedirectToAction("Index");
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        public string getServiceList()
        {
            List<Service> list = bs.GetServices();
            string output = "<option value=''>Select Service</option>";

            foreach(Service s in list)
            {
                output+="<option value='"+s.serviceId+"'>"+s.serviceName+"</option>";
            }



            return output;
        }

        public string getSubServiceList(int id)
        {

            List<subService> list = bs.GetSubServices(id);
            string output = "<option value=''>Select Sub Service</option>";

            foreach (subService s in list)
            {
                output += "<option value='" + s.subServiceId + "'>" + s.SubServiceName + "</option>";
            }

            return output;
        }


        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = objAlbumModelService.GetImageFromDataBase(id);
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