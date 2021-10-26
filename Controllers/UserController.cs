using System.Linq;
using System.Threading.Tasks;
using API_ToDo.Data;
using API_ToDo.Domain;
using API_ToDo.Domain.Dtos;
using API_ToDo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_ToDo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ToDoContext _context;
        private readonly UserManager<User> _userManager;

        public UserController(ToDoContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.users);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserEntryDto user)
        {
            var userDb = await RegisterService.Register(user, _userManager);

            if (userDb != null)
            {
                return CreatedAtAction(nameof(GetById), new { id = userDb.Id }, new UserReturnDto
                {
                    Email = userDb.Email,
                    Id = userDb.Id,
                    Username = userDb.UserName
                }); 
            }

            return BadRequest();
        }

        [HttpGet("/{id}")]
        public IActionResult GetById( int id)
        {
            return Ok(_context.users.FirstOrDefault(u => u.Id == id));
        }

    }
}
