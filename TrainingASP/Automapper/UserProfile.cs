using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TrainingASP.Data;
using TrainingASP.Models;

namespace TrainingASP.Automapper
{
    public class UserTblMappingUserViewModel : Profile
    {
        public UserTblMappingUserViewModel()
        {
            CreateMap<UsersTbl, UserViewModel>().ReverseMap(); ;
            CreateMap<List<UsersTbl>, List<UserViewModel>>().ReverseMap(); ;
        }
    }
}