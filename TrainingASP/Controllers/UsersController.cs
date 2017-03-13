using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using Newtonsoft.Json;
using TrainingASP.Client;
using TrainingASP.Models;
using TrainingASP.Services;
using TrainingASP.Services.Interfaces;

namespace TrainingASP.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {

            var users = new UsersClient().GetAllUsers();
            
            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel user)
        {
            new UsersClient().Create(user);

            return RedirectToAction("Index", "Users");
        }

        public ActionResult Delete(int id)
        {
            new UsersClient().Delete(id);
            return RedirectToAction("Index", "Users");
        }
    }
}