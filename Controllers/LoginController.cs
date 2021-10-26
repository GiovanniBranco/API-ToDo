using System;
using System.Linq;
using System.Threading.Tasks;
using API_ToDo.Data;
using API_ToDo.Domain;
using API_ToDo.Domain.Dtos;
using API_ToDo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_ToDo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ToDoContext _context;

        public LoginController(SignInManager<User> signInManager, UserManager<User> userManager, ToDoContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login (UserLoginDto dto)
        {
            try
            {
                var user = _context.users.FirstOrDefault(u => u.UserName == dto.Username);

                if (user != null)
                {
                    var signIn = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

                    if (signIn.Succeeded)
                    {
                        var token = await AuthService.TokenGenerator(user, _userManager);

                        return Ok(new ReturnLoginDto
                        {
                            AccessToken = token,
                            Username = user.UserName,
                            Date = DateTime.Now,
                        });
                    }

                    return Unauthorized("Username or Passorword is invalid. Please, try again");

                }

                return new NotFoundObjectResult("Wed did't find any user with this username, please try again or register yourself.");
            }

            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou{ex.Message}");
            }
        }
    }
}
