using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Providers.Entities;
using WebChat.Repository;
using WebChatAPI.Models;
using WebChat.Models;

namespace WebChatAPI.Controllers
{
    public class SessionsController : ApiController
    {
        IRepository<ChatSession> sessionRepository;

        public SessionsController(IRepository<ChatSession> sessionRepository)
        {
            this.sessionRepository = sessionRepository;
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
        public string Post([FromBody]UserStartChat data)
        {
            var newChannel = PubNubNewChannel.CreatenewChannel();
            var sessionModel = new ChatSession()
            {
                ConnectionString = newChannel,
                SenderId = data.SenderId,
                ReceiverId = data.ReceiverId
            };

            var created = this.sessionRepository.Add(sessionModel);

            return newChannel;
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
