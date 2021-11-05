using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_ToDo.Domain;
using API_ToDo.Domain.Dtos;
using Microsoft.AspNetCore.Identity;

namespace API_ToDo.Services
{
    public class RegisterService
    {
        public static IList<string> Errors { get; set; }

        public static async Task<User> Register(UserEntryDto userDto, UserManager<User> userManager)
        {
            var user = new User { UserName = userDto.Username, Email = userDto.Email, FullName = userDto.FullName };
            var result = await userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
                return await userManager.FindByNameAsync(user.UserName);

            return null;
        }
    }
}
