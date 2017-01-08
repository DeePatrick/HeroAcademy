using HeroAcademy.Controllers;
using HeroAcademy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace HeroAcademy.ViewModels
{
    public class HeroAcademyVM
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [Birthday]
        public string DateOfBirth { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public string Classroom { get; set; }

        [Required]
        public byte Qualification { get; set; }

        public IEnumerable<Qualification> Qualifications { get; set; }

        [Required]
        public byte Course  { get; set; }

        public IEnumerable<Course> Courses { get; set; }

        [Required]
        public byte Instructor { get; set; }

        public IEnumerable<Instructor> Instructors { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }


        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }

        public DateTime GetDateOfBirth()
        {
            return DateTime.Parse(string.Format("{0} {1}", DateOfBirth, ""));
        }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<HeroLessonsController, ActionResult>> update =
                    (c => c.Update(this));

                Expression<Func<HeroLessonsController, ActionResult>> create =
                    (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }
    }
}