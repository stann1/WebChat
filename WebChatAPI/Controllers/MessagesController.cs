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
    public class MessagesController : ApiController
    {
        private IRepository<Message> data;

        public MessagesController()
        {
            this.data = new MessagesRepository();
        }

        public void Send(int senderId, int receiverId, [FromBody]string content)
        {
            // Validate content

            Message newMessage = new Message()
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content
            };

            data.Add(newMessage);
        }
        
        public ICollection<Message> GetInbox(int receiverId)
        {
            var allMesages = this.data.All();
            var inbox = allMesages.Where(m => m.ReceiverId == receiverId).ToList();
            return inbox;
        }

        public ICollection<Message> GetSent(int senderId)
        {
            var sentMessages = this.data.All().Where(m => m.SenderId == senderId).ToList();
            return sentMessages;
        }

        // POST api/message
        public void Post([FromBody]string value)
        {
        }

        // PUT api/message/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/message/5
        public void Delete(int id)
        {
        }
    }
}
