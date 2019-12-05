using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeautyBusiness.DataAccess;
using BeautyBusiness.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BeautyBusiness.Controllers
{
    public class HomeController : Controller
    {
        AlbumService alb = new AlbumService();
      
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View("HomeView");
        }

        public ActionResult GallaryView()
        {
            return View();
        }

        public string getAllGallary()
        {
            List<GallaryView> lst = alb.GetGallaryViewList();

            string str = string.Empty;
            string output = string.Empty;
            int p = 0;
            foreach (GallaryView v in lst)
            {

                if(v.ServiceName== "Hair")
                {
                    str = "hair";
                }
                else if(v.ServiceName== "Eyebrows")
                {
                    str = "face";  //face - Eyebrows
                }
                else if(v.ServiceName== "Nails")
                {
                    str = "manicure";  //manicure-  Nails
                }
               
                output += "<div class='gallery-item filterable-item "+str+ " isotope-item'  style = 'position: absolute; left: 0px; top: 0px; transform: translate3d("+p.ToString()+"px, 0px, 0px);'>"
                   + " <a href = ''> "
                   + " <figure class='featured-image'>"
                           + "<img src='/Home/RetrieveImage/" + v.ImageId + "' alt='No Images'  width='120' height='120' class='imageclass'>"
                            + "<figcaption>"
                                +"<h2 class='gallery-title'>"+v.SubServiceName+"</h2>"
                                +"<p>Description will g here</p>"
                            +"</figcaption>"
                        +"</figure>"
                    +"</a>"
                +"</div>";
                p = p + 291;

            }

            string output1 = @"<div class='gallery-item filterable-item manicure' style = 'position: absolute; left: 0px; top: 0px; transform: translate3d(0px, 0px, 0px);'>
                        <a href='dummy/large-gallery/gallery-1.jpg'>     
                             <figure class='featured-image'>
                            <img src = 'dummy/gallery-1.jpg' alt=''>
                            <figcaption>
                                <h2 class='gallery-title'>Lorem ipsum dolor sit amet</h2>
                                <p>Maecenas dictum suscipit</p>
                            </figcaption>
                        </figure>
                    </a>
                </div>";




            return output1;
        }

      //  isotope-item" style="position: absolute; left: 0px; top: 0px; transform: translate3d(0px, 0px, 0px);


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


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Sent()
        {
            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("charlierolfe6767@gmail.com"));  // replace with valid value 
                message.From = new MailAddress("robbiedarza@hotmail.co.uk");  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "robbiedarza@hotmail.co.uk",  // replace with valid value
                        Password = "Cheesenip671"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }
    }
}