using HeroAcademy.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace HeroAcademy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context =  new ApplicationDbContext();
        }

           
        // GET: Home
        public ActionResult Index()
        {
            List<HeroLesson> heroLessons = _context.HeroLessons
                .Include(g => g.Student)
                .Include(g => g.Course)
                .Include(g => g.Instructor)
                .Include(g => g.Qualification) 
                .ToList();

            return View(heroLessons);
        }


        //This code is to return image from binary db.Delete if not working.
        //start here.
        public FileContentResult getImg(int id)
        {
            byte[] byteArray = _context.Users.Find(id).ProfilePicture;
            return byteArray != null
                ? new FileContentResult(byteArray, "image/jpeg")
                : null;
        }
        //ends here.




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
    }
}