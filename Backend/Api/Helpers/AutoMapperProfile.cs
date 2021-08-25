using Api.DataContext.Entity;
using Api.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserRequestModels, User>();            
            CreateMap<User, UserModels>();
            CreateMap<Role, RoleModels>();
        }
    }
}
