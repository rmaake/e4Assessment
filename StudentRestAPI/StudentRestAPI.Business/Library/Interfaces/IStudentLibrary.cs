using StudentRestAPI.Business.Models;

namespace StudentRestAPI.Business.Library.Interfaces
{
    public interface IStudentLibrary
    {
        IEnumerable<Student> GetStudents(int page = 1, int pageSize = 10);
        Student GetStudent(int id);
        Student CreateStudent(Student student);
        void UpdateStudent(int studentId, Student student);
        void DeleteStudent(int id);
    }
}
