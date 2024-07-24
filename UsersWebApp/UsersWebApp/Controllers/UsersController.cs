using Microsoft.AspNetCore.Mvc;
using UsersWebApp.Models;
using UsersWebApp.Repository;

namespace UsersWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IUserRepository _repo;
        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return StatusCode(200, _repo.GetUsers());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public IActionResult Get(int userId)
        {
            try
            {
                return StatusCode(200, _repo.GetUserById(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult SaveUser([FromBody]User user)
        {
            try
            {
                return StatusCode(200, _repo.Save(user));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            try
            {
                _repo.Delete(userId);
                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
