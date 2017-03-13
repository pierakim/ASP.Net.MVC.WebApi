using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TrainingASP.Models;
using TrainingASP.Services.Interfaces;

namespace TrainingASP.Controllers.Api
{
    public class UsersApiController : ApiController
    {
        private readonly IUsersService _service;
        public UsersApiController(IUsersService service)
        {
            _service = service;
        }

        // GET: api/Users  
        public IEnumerable<UserViewModel> GetUsers()
        {
            return _service.GetUsersList();
        }

        // POST: api/users
        [HttpPost]
        public IHttpActionResult Create(UserViewModel user)
        {
            _service.Create(user);

            return Ok();
        }

        // DELETE: api/users/5 
        [HttpDelete] 
        public IHttpActionResult Delete(int id)
        {
            _service.Delete(id);

            return Ok();
        }
    }
}