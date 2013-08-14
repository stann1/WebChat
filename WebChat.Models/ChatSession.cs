﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.Models
{
    public class ChatSession
    {
        public int SessionId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string ConnectionString { get; set; }
        public string Pass { get; set; }
    }
}