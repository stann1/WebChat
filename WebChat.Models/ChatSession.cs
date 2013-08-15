using System;
using System.Linq;

namespace WebChat.Models
{
    public class ChatSession
    {
        public int ChatSessionId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string ConnectionString { get; set; }
        public string Pass { get; set; }
    }
}
