using HeroAcademy.Models;
using HeroAcademy.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;


namespace HeroAcademy.Controllers
{
    public class HeroLessonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HeroLessonsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: HeroAcademys
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new HeroAcademyVM
            {
                Courses = _context.Courses.ToList(),
                Qualifications = _context.Qualifications.ToList(),
                Instructors = _context.Instructors.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(HeroAcademyVM viewModel)
        {
            var studentId = User.Identity.GetUserId();
            var student = _context.Users.Single(u => u.Id == studentId);

            if (!ModelState.IsValid)
            {
                viewModel.Instructors = _context.Instructors.ToList();
                viewModel.Qualifications = _context.Qualifications.ToList();
                viewModel.Courses = _context.Courses.ToList();

                return View(viewModel);
            }

            var lesson = new HeroLesson
            {
                StudentId = User.Identity.GetUserId(),
                DateOfBirth = viewModel.GetDateOfBirth(),
                DateTime = viewModel.GetDateTime(),
                Classroom = viewModel.Classroom,
                InstructorId = viewModel.Instructor,
                CourseId = viewModel.Course,
                QualificationId = viewModel.Qualification,
                Duration = viewModel.Duration,
                FullName = viewModel.FullName
            };

            _context.HeroLessons.Add(lesson);
            _context.SaveChanges();

            return RedirectToAction("Index", "HeroLessons");
        }

        public ActionResult Index(string query = null)
        {
            var heroLessons = _context.HeroLessons
                .Include(g => g.Student)
                .Include(g => g.Course)
                .Include(g => g.Instructor)
                .Include(g => g.Qualification)
                .OrderBy(g => g.FullName)
                .ToList();

            return View(heroLessons);
        }

        public ActionResult Details(int id)
        {
            var viewModel = _context.HeroLessons
                .Include(g => g.Student)
                .Include(g => g.Course)
                .Include(g => g.Instructor)
                .Include(g => g.Qualification)
                .SingleOrDefault(g => g.Id == id);

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {
            var viewModel = _context.HeroLessons
                .Include(g => g.Student)
                .Include(g => g.Course)
                .Include(g => g.Instructor)
                .Include(g => g.Qualification)
                .SingleOrDefault(g => g.Id == id);


            Debug.Assert(viewModel != null, "viewModel != null");
            _context.HeroLessons.Remove(viewModel);
            _context.SaveChanges();

            return View(viewModel);
        }


        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var heroLesson = _context.HeroLessons.SingleOrDefault(e => e.Id == id && e.StudentId == userId);

            if (heroLesson == null)
                return HttpNotFound();


            var viewModel = new HeroAcademyVM
            {

                Id = heroLesson.Id,
                FullName = heroLesson.FullName,
                Date = heroLesson.DateTime.ToString("d MMM yyyy"),
                Time = heroLesson.DateTime.ToString("HH:mm"),
                Duration = heroLesson.Duration,
                Classroom = heroLesson.Classroom,
                DateOfBirth = heroLesson.DateOfBirth.ToShortDateString(),

                Instructor = heroLesson.InstructorId,
                Qualification = heroLesson.QualificationId,
                Course = heroLesson.CourseId,

                Instructors = _context.Instructors.ToList(),
                Qualifications = _context.Qualifications.ToList(),
                Courses = _context.Courses.ToList()
            };


            return View("Edit", viewModel);
        }



        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update(HeroAcademyVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Instructors = _context.Instructors.ToList();
                viewModel.Qualifications = _context.Qualifications.ToList();
                viewModel.Courses = _context.Courses.ToList();

                return View("Edit", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var heroLesson = _context.HeroLessons
                .Single(g => g.Id == viewModel.Id && g.StudentId == userId);

            heroLesson.Modify(
                viewModel.GetDateTime(),
                viewModel.Classroom,
                viewModel.FullName,
                viewModel.Duration,
                viewModel.Instructor,
                viewModel.Course,
                viewModel.Qualification
                //viewModel.GetDateOfBirth()
            );

            _context.SaveChanges();

            return RedirectToAction("Mine", "HeroLessons");

        }


        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            List<HeroLesson> heroLessons = _context.HeroLessons
                .Where(g =>
                    g.StudentId == userId)
                .Include(g => g.Student)
                .Include(g => g.Course)
                .Include(g => g.Instructor)
                .Include(g => g.Qualification)
                .ToList();

            return View(heroLessons);
        }

        public FileContentResult UserPhotos()
        {
            var userId = User.Identity.GetUserId();
            if (User.Identity.IsAuthenticated)
            {
                // to get the user details to load user Image
                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = bdUsers.Users.SingleOrDefault(x => x.Id == userId);

                return new FileContentResult(userImage.ProfilePicture, "image/jpeg");
            }
            string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.jpg");

            byte[] imageData = null;
            FileInfo fileInfo = new FileInfo(fileName);
            long imageFileLength = fileInfo.Length;
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            imageData = br.ReadBytes((int) imageFileLength);
            return File(imageData, "image/png");
        }
    }
}