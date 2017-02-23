using HeroAcademy.Models;
using System.Web.Mvc;

namespace HeroAcademy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }


        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Instructors()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Courses()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}

