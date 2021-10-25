using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_ToDo.Domain;
using API_ToDo.Domain.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API_ToDo.Services
{
    public class RegisterService 
    {
        public static async Task<User> Register(UserEntryDto userDto, UserManager<User> userManager)
        {
            var user = new User { UserName = userDto.Username, Email = userDto.Email };
            var returnDto = await userManager.CreateAsync(user, userDto.Password);

            if (returnDto.Succeeded)
                return await userManager.FindByNameAsync(user.UserName);

            return null;
        }
    }
}
