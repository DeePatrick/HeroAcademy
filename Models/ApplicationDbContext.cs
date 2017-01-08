using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace HeroAcademy.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<HeroLesson> HeroLessons { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Instructor> Instructors { get; set; }




        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}