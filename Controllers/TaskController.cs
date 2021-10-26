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
                        var tasksReturn = new List<TaskReturnDto>();
                        foreach (var item in tasks)
                        {
                            tasksReturn.Add(new TaskReturnDto
                            {
                                Id = item.Id,
                                Title = item.Title,
                                Observation = item.Observation,
                                User = item.User.UserName,
                                Date = item.Date,
                            });
                        }

                        return Ok(tasksReturn);
                    }

                    return NoContent();
                }

                return new NotFoundObjectResult("Wed did't find any user with this username:" + username + ", please try again or register yourself.");
            }
            catch (Exception ex)
            {
                return ReturnCase500(ex.Message);
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
                return ReturnCase500(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update (int id, [FromBody] TaskEntryUpdateDto dto)
        {
            try
            {
                var task = await GetTaskById(id);

                if (task != null)
                {
                    if (dto.Title != null)
                    {
                        task.Title = dto.Title;
                    }
                    if (dto.Observation != null)
                    {
                        task.Observation = dto.Observation;
                    }
                    if (dto.Date != null)
                    {
                        task.Date = dto.Date.Value;
                    }

                    await _context.SaveChangesAsync();

                    return NoContent();
                }

                return new NotFoundObjectResult("Wed did't find any task with this id:" + id + ", please try with a valid id.");
            }
            catch (Exception ex)
            {
                return ReturnCase500(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove (int id)
        {
            try
            {
                var task = await GetTaskById(id);

                if (task != null)
                {
                    _context.Tasks.Remove(task);
                    await _context.SaveChangesAsync();

                    return NoContent();
                }

                return new NotFoundObjectResult("Wed did't find any task with this id:" + id + ", please try with a valid id.");
            }
            catch (Exception ex)
            {
                return ReturnCase500(ex.Message);
            }
        }

        private async Task<Domain.Task> GetTaskById(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        private async Task<User> GetUserByUsername(string username)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.UserName == username.ToLower());

            return user;
        }

        private ObjectResult ReturnCase500 (string message)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou{message}");
        }

    }
}
