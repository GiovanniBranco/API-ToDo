using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_ToDo.Domain;
using API_ToDo.Domain.Dtos;
using AutoMapper;

namespace API_ToDo.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntryDto, User>().ReverseMap();
            CreateMap<User, UserReturnDto>().ReverseMap();
        }
    }
}
