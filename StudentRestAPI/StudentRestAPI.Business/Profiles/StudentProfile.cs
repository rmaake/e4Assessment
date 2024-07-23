using AutoMapper;
using StudentRestAPI.Business.Models;

namespace StudentRestAPI.Business.Profiles
{
    public class StudentProfile: Profile
    {
        public StudentProfile()
        {
            CreateMap<DAL.Models.Student, Student>();
            CreateMap<Student, DAL.Models.Student>();
        }
    }
}
