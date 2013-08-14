using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebChatAPI.Models
{
    public class UserStartChat
    {
        public string UserKey { get; set; }
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
    }
}