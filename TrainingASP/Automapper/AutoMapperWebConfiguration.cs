using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TrainingASP.Data;
using TrainingASP.Models;

namespace TrainingASP.Automapper
{
    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserTblMappingUserViewModel>();
            });
            return config;
        }
    }
}