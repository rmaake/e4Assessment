using AutoMapper;
using StudentRestAPI.Business.Library.Interfaces;
using StudentRestAPI.Business.Models;
using StudentRestAPI.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRestAPI.Business.Library.Implementations
{
    public class StudentLibrary : IStudentLibrary
    {
        private readonly IStudentRepository _repo;
        private readonly IMapper _mapper;
        public StudentLibrary(IStudentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public Student CreateStudent(Student student)
        {
            DAL.Models.Student newStudent = _mapper.Map<DAL.Models.Student>(student);
            newStudent.CreatedDate = DateTime.Now;
            newStudent.UpdatedDate = DateTime.Now;
            newStudent = _repo.Save(newStudent);
            return _mapper.Map<Student>(newStudent);
        }

        public void DeleteStudent(int id)
        {
            _repo.Delete(id);
        }

        public Student GetStudent(int id)
        {
            DAL.Models.Student student = _repo.GetById(id);
            return _mapper.Map<Student>(student);
        }

        public IEnumerable<Student> GetStudents(int page = 1, int pageSize = 10)
        {
            List<DAL.Models.Student> students = _repo.Get(page, pageSize).ToList();
            return students.ConvertAll(student => _mapper.Map<Student>(student));
        }

        public void UpdateStudent(int studentId, Student student)
        {
            DAL.Models.Student existingStudent = _repo.GetById(studentId);
            if (existingStudent == null)
            {
                throw new Exception($"StudentId={studentId} does not exist on platform");
            }
            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.DateOfBirth = student.DateOfBirth;
            existingStudent.UpdatedDate = DateTime.Now;
            _repo.Save(existingStudent);
        }
    }
}
