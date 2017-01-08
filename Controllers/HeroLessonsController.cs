using HeroAcademy.Models;
using HeroAcademy.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

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
        [Authorize]
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

            var Lesson = new HeroLesson
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

            _context.HeroLessons.Add(Lesson);
            _context.SaveChanges();

            return RedirectToAction("Index", "HeroLessons");
        }

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


        [Authorize]
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
        public ActionResult Delete(int Id)
        {
            var viewModel = _context.HeroLessons
                .Include(g => g.Student)
                .Include(g => g.Course)
                .Include(g => g.Instructor)
                .Include(g => g.Qualification)
                .SingleOrDefault(g => g.Id == Id);

            return View(viewModel);
        }

         [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var heroLesson  = _context.HeroLessons.Single(e => e.Id == id && e.StudentId == userId);

            var viewModel = new HeroAcademyVM
            {

                Id = heroLesson.Id,
                FullName = heroLesson.FullName,
                Date = heroLesson.DateOfBirth.ToString("d MMM yyyy"),
                Time = heroLesson.DateOfBirth.ToString("HH:mm"),
                Duration = heroLesson.Duration,
                Classroom = heroLesson.Classroom,
                Instructors = _context.Instructors.ToList(),
                Qualifications = _context.Qualifications.ToList(),
                Courses = _context.Courses.ToList()
            };


            return View("Edit", viewModel);
        }



        [Authorize]
        [HttpPost]
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
            heroLesson.Modify( viewModel.GetDateTime(),
                viewModel.Classroom,
                viewModel.FullName,
                viewModel.Duration,
                viewModel.Instructor,
                viewModel.Course,
                viewModel.Qualification
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
       
    }
}