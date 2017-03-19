using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using TrainingASP.Automapper;
using TrainingASP.Data;
using TrainingASP.Models;
using TrainingASP.Services;
using TrainingASP.Services.Interfaces;

namespace TrainingASP
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //**************
            //simpleInjector
            //**************
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            
            //Registration
            container.Register<IUsersService, UsersService>(Lifestyle.Scoped); 
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            //Automapper registeration
            var config = new AutoMapperConfiguration().Configure();
            container.RegisterSingleton<MapperConfiguration>(config);
            container.Register<IMapper>(() => config.CreateMapper(container.GetInstance), Lifestyle.Scoped);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
            new SimpleInjectorWebApiDependencyResolver(container);

        }
    }
}
