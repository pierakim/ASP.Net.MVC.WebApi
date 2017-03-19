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

        // GET: api/Users/5  
        public IHttpActionResult Get(int id)
        {
            var user = _service.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public IHttpActionResult Create(UserViewModel user)
        {
            if (!ModelState.IsValid || user == null)
            {
                return BadRequest(ModelState);
            }

            var res = _service.Create(user);

            return CreatedAtRoute("DefaultApi", new {id = res.UserId}, res);
        }

        // DELETE: api/users/5 
        [HttpDelete] 
        public IHttpActionResult Delete(int id)
        {
            var user = _service.Delete(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // PUT: api/users/5 
        [HttpPut]
        public IHttpActionResult Edit(int id, UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var res = _service.Edit(id, user);
            if (!res)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}