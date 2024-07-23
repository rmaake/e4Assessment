using StudentRestAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRestAPI.DAL.Repository.Interfaces
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
    }
}
