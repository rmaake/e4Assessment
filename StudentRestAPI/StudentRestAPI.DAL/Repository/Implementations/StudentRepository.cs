using Microsoft.EntityFrameworkCore;
using StudentRestAPI.DAL.Models;
using StudentRestAPI.DAL.Models.DbContexts;
using StudentRestAPI.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRestAPI.DAL.Repository.Implementations
{
    public class StudentRepository: BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(StudentDbContext dbContext) : base(dbContext)
        {
        }
    }
}
