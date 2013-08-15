using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
