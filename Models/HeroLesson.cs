using System;
using System.ComponentModel.DataAnnotations;

namespace HeroAcademy.Models
{
    public class HeroLesson
    {
        public int Id { get; set; }

        [Required]
        public string FullName  { get; set; }

        public ApplicationUser Student { get; set; }

        [Required]
        public string StudentId { get; set; }

        public Instructor Instructor { get; set; }

        [Required]
        public byte InstructorId { get; set; }


        public Qualification Qualification { get; set; }

        [Required]
        public byte QualificationId { get; set; }

        public Course Course { get; set; }

        [Required]
        public byte CourseId { get; set; }

        public DateTime DateOfBirth { get; set; }


        public DateTime DateTime { get; set; }

        public TimeSpan Duration { get; set; }

        [Required]
        [StringLength(255)]
        public string Classroom { get; set; }
        public bool IsDeleted { get; private set; }

        public void Modify(DateTime dateTime, string classroom, string fullName, TimeSpan duration, byte instructor, byte course, byte qualification, DateTime dateOfBirth)
        {           
            FullName = fullName;
            DateTime = dateTime;
            Classroom = classroom;
            CourseId = course;
            InstructorId = instructor;
            QualificationId = qualification;
            Duration = duration;
            DateOfBirth = dateOfBirth;
        }
    }
}
