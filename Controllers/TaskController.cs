using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using API_ToDo.Data;
using API_ToDo.Domain;
using API_ToDo.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_ToDo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ToDoContext _context;

        public TaskController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetByUser(string username)
        {
            try
            {
                var user = await GetUserByUsername(username);

                if (user != null)
                {
                    var tasks = _context.Tasks
                        .Include(u => u.User)
                        .Where(t => t.User.UserName == user.UserName)
                        .ToList();

                    if (tasks.Count > 0)
                    {
                        return Ok(tasks);
                    }

                    return NoContent();
                }

                return new NotFoundObjectResult("Wed did't find any user with this username:" + username + ", please try again or register yourself.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou{ex.Message}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TaskEntryDto dto)
        {
            try
            {
                var user = await GetUserByUsername(dto.User);

                if (user != null)
                {
                    var task = new Domain.Task
                    {
                        Title = dto.Title,
                        Observation = dto.Observation,
                        Date = dto.Date,
                        User = user,

                    };

                    _context.Tasks.Add(task);
                    await _context.SaveChangesAsync();


                    return CreatedAtAction(
                        nameof(GetByUser),
                        new { username = task.User.UserName },
                        new TaskReturnDto
                        {
                            Id = task.Id,
                            Title = task.Title,
                            Observation = task.Observation,
                            Date = task.Date,
                            User = task.User.UserName
                        });
                }

                return new NotFoundObjectResult("Wed did't find any user with this username:" + dto.User + ", please try again or register yourself.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou{ex.Message}");
            }
        }


        private async Task<User> GetUserByUsername(string username)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.UserName == username.ToLower());

            return user;
        }
    }
}
