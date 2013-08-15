using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebChat.Classes;
using Repository;
using WebChat.Models;
using System.Data.Entity;
using WebChat.Access.Models;

namespace WebChat.Access.Controllers
{
    public class ChatsController : ApiController
    {
        private IRepository<Chat> repository;
        public ChatsController()
        {
            this.repository = new ChatsRepository(new ChatEntities());
        }

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public void Put()
        {
        }
    }
}
