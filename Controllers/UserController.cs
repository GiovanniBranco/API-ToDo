using System;
using System.Linq;
using System.Threading.Tasks;
using API_ToDo.Data;
using API_ToDo.Domain;
using API_ToDo.Domain.Dtos;
using API_ToDo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            return Ok(_context.UsersDbSet);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserEntryDto user)
        {
            try
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
            catch (Exception ex)
            {
                return ReturnCase500(ex.Message);
            }

        }

        [HttpGet("/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var user = _context.UsersDbSet.FirstOrDefault(u => u.Id == id);

                if (user != null)
                {
                    return Ok(user);
                }

                return new NotFoundObjectResult("Wed did't find any user with this id:" + id + ", please try again or register yourself.");
            }
            catch (Exception ex)
            {
                return ReturnCase500(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UserUpdateDto dto)
        {
            try
            {
                var user = _context.UsersDbSet.FirstOrDefault(u => u.Id == id);

                if (user != null)
                {
                    if (string.IsNullOrEmpty(dto.Email))
                    {
                        user.Email = dto.Email;
                    }
                    if (string.IsNullOrEmpty(dto.Username))
                    {
                        user.UserName = dto.Username;
                    }
                    if (string.IsNullOrEmpty(dto.NewPassword))
                    {
                        if (string.IsNullOrEmpty(dto.CurrentPassword))
                        {
                            return new BadRequestObjectResult("To change password is necessary to inform the current password on field CurrentPassword");
                        }
                        await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
                    }

                    await _context.SaveChangesAsync();

                    return NoContent();
                }

                return new NotFoundObjectResult("Wed did't find any user with this id:" + id + ", please try again or register yourself.");
            }
            catch (Exception ex)
            {
                return ReturnCase500(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            try
            {
                var user = _context.UsersDbSet.FirstOrDefault(u => u.Id == id);

                if (user != null)
                {
                    _context.UsersDbSet.Remove(user);
                    await _context.SaveChangesAsync();
                }

                return new NotFoundObjectResult("Wed did't find any user with this id:" + id + ", please try again or register yourself.");
            }
            catch (Exception ex)
            {
                return ReturnCase500(ex.Message);
            }
        }

        private ObjectResult ReturnCase500(string message)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou {message}");
        }

    }
}
