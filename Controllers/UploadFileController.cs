using System.IO;
using System.Web;
using System.Web.Mvc;

namespace HeroAcademy.Controllers
{
    public class UploadFileController : Controller
    {
        // GET: UploadFile
        public ActionResult Index()
        {
            return View();
        }
        
        private bool isValidContentType(string contentType)
        {
            return contentType.Equals("image/png") 
                || contentType.Equals("image/gif")
                || contentType.Equals("image/gif")
                || contentType.Equals("image/jpg");
        }

        private bool isValidContentLength(int contentLength)
        {
            return ((contentLength / 1024 )/ 1024 )< 1; //1MB
        }


        [HttpPost]
        public ActionResult Process(HttpPostedFileBase photo)
        {
            if (!isValidContentType(photo.ContentType))
            {
                ViewBag.Error = "Only JPG, PNG & GIF files allowed.";
                return View("Index");
            }

            else if (!isValidContentLength(photo.ContentLength))
            {
                ViewBag.Error = "File too large! Only files less than 1MB allowed.";
                return View("Index");
            }

            else
            {
                if(photo.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(photo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                    photo.SaveAs(path);
                    ViewBag.fileName = photo.FileName;
                }

            }
            return View("Success");
        }
    }
}