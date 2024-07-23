using Microsoft.EntityFrameworkCore;

namespace StudentRestAPI.DAL.Models.DbContexts
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public virtual DbSet<Student> Students { get; set; }
    }
}
