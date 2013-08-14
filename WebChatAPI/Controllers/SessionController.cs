using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebChatAPI.Controllers
{
    public class SessionController : ApiController
    {
        public SessionController()
        {
        }
        // GET api/sessions
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/sessions/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/sessions
        public void Post([FromBody]string value)
        {
        }

        // PUT api/sessions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/sessions/5
        public void Delete(int id)
        {
        }
    }
}
