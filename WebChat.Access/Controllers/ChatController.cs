using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebChat.Classes;
using Repositoty;
using WebChat.Model;
using System.Data.Entity;
using WebChat.Access.Models;

namespace WebChat.Access.Controllers
{
    public class ChatController : ApiController
    {
        private IRepository<Chat> repository;
        public ChatController()
        {
            this.repository = new ChatRepository(new ChatEntities());
        }

        [HttpGet]
        [ActionName("get")]
        public List<NotSeenChats> GetNewChat(string id)
        {
            int userId = GetUserBySessionKey.Get(id);

            if (userId == 0)
            {
                throw new ArgumentException();
            }

            var allChats = this.repository.All();

            var newChats =
                (from chat in allChats
                 where chat.Seen == Varibles.NotSeenMessage && chat.SecondUserId == userId
                 select chat);

            List<NotSeenChats> list = new List<NotSeenChats>();

            foreach (var item in newChats.ToList())
            {
                this.repository.Update(item.Id, item);
                NotSeenChats newChat = new NotSeenChats()
                {
                    Context = item.ConnectionString,
                    SenderId = item.FisrtUserId
                };

                list.Add(newChat);
            }

            return list;
        }

        [HttpPost]
        [ActionName("add")]
        public string AddNewChat(string id, [FromBody]int resiverId)
        {
            int userId = GetUserBySessionKey.Get(id);
            if (userId == 0)
            {
                throw new ArgumentException();
            }

            PubnubController pbController = new PubnubController();

            string connectionString = pbController.CreatenewChannel();

            Chat newChat = new Chat()
            {
                ConnectionString = connectionString,
                FisrtUserId = userId,
                SecondUserId = resiverId,
                Seen = Varibles.NotSeenMessage
            };

            this.repository.Add(newChat);

            return connectionString;
        }

        private string MakeChat()
        {
            return "connection string";
        }
    }
}
