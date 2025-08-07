using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private static List<User> _users = new List<User>();
        private static int _nextId = 1;

        // GET: api/user
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return Ok(_users);
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
        public ActionResult<User> CreateUser(User newUser)
        {
            newUser.Id = _nextId++;
            _users.Add(newUser);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null) return NotFound();

            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Email = updatedUser.Email;
            existingUser.Department = updatedUser.Department;

            return NoContent();
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            _users.Remove(user);
            return NoContent();
        }
    }
}
