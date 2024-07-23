using Microsoft.AspNetCore.Mvc;
using StudentRestAPI.Business.Library.Interfaces;
using StudentRestAPI.Business.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentLibrary _studentLibrary;
        public StudentsController(IStudentLibrary studentLibrary)
        {
            _studentLibrary = studentLibrary;
        }

        // GET: api/<StudentsController>/1/10
        [HttpGet("{page}/{pageSize}")]
        public IEnumerable<Student> Get(int page, int pageSize)
        {
            return _studentLibrary.GetStudents(page, pageSize);
        }

        // GET: api/<StudentsController>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _studentLibrary.GetStudents();
        }

        // GET api/<StudentsController>/5
        [HttpGet("{studentId}")]
        public Student Get(int studentId)
        {
            return  _studentLibrary.GetStudent(studentId);
        }

        // POST api/<StudentsController>
        [HttpPost]
        public IActionResult CreateStudent([FromBody] Student student)
        {
            try
            {
                student = _studentLibrary.CreateStudent(student);
                return StatusCode(201, student);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{studentId}")]
        public IActionResult UpdateStudent(int studentId, [FromBody] Student student)
        {
            try
            {
                _studentLibrary.UpdateStudent(studentId, student);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{studentId}")]
        public IActionResult Delete(int studentId)
        {
            try
            {
                _studentLibrary.DeleteStudent(studentId);
                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
