using Microsoft.EntityFrameworkCore;

namespace StudentRestAPI.DAL.Models.DbContexts
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
            Database.Migrate();
            SeedStudentData();
        }

        public virtual DbSet<Student> Students { get; set; }

        private void SeedStudentData()
        {
            if (Students.Any())
            {
                return;
            }
            var students = new List<Student>
            {
                new Student
                {
                    FirstName = "Raps",
                    LastName = "Tester",
                    DateOfBirth = DateTime.Now.AddYears(-20)
                },
                new Student
                {
                    FirstName = "Rapsidy",
                    LastName = "Tester",
                    DateOfBirth = DateTime.Now.AddYears(-25)
                },
                new Student
                {
                    FirstName = "Inek",
                    LastName = "Tester",
                    DateOfBirth = DateTime.Now.AddYears(-30)
                },
            };
            Students.AddRange(students);
            SaveChanges();
        }
    }
}
