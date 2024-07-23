using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRestAPI.DAL.Models.DbContexts
{
    public class StudentDbContextFactory : IDesignTimeDbContextFactory<StudentDbContext>
    {
        public StudentDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentDbContext>();
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Database=StudentDb;Trusted_Connection=True;"); // leave this as your local config. Purely to enable you to generate migrations.
            return new StudentDbContext(optionsBuilder.Options);
        }
    }
}
