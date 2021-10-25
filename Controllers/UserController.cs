using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_ToDo.Data;
using API_ToDo.Domain;
using API_ToDo.Domain.Dtos;
using API_ToDo.Services;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserController(ToDoContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.User);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserEntryDto user)
        {
            var userDb = await RegisterService.Register(user, _userManager);

            if (userDb != null)
               return CreatedAtAction("Created", _mapper.Map<UserReturnDto>(userDb));


            return BadRequest();
        }

    }
}
