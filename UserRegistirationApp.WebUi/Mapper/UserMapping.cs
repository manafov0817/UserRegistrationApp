using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistirationApp.Entity.Entities;
using UserRegistirationApp.WebUi.Models.DTOs;

namespace UserRegistirationApp.WebUi.Mapper
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserDTO, User>().ReverseMap();
        }

    }
}
