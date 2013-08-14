using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebChat.Models;
using WebChat.Repository;

namespace WebChatAPI.Controllers
{
    public class UsersController : ApiController
    {
        UserRepository sessionRepository;

        public UsersController(IRepository<ChatUser> sessionRepository)
        {
            this.sessionRepository = (UserRepository)sessionRepository;
        }
        [HttpPost]
        [ActionName("register")]
        public HttpResponseMessage RegisterUser(ChatUser user)
        {
            if (user == null || user.Username == null)
            {
                var errResponse = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The name of the User cannot be empty");
                return errResponse;
            }
            var entity = this.sessionRepository.Add(user);

            var response =
                 Request.CreateResponse(HttpStatusCode.Created, entity);

            response.Headers.Location = new Uri(Url.Link("DefaultApi",
                new { id = entity.UserId }));
            return response;
        }

        // GET api/login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/login
        public void Post([FromBody]string value)
        {
        }

        // PUT api/login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/login/5
        public void Delete(int id)
        {
        }
    }
}
