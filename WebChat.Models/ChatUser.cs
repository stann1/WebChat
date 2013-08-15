using System;
using System.Linq;

namespace WebChat.Models
{
    public class ChatUser
    {
        //[Key]
        public int ChatUserId { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public string PictureUrl { get; set; }
        public Nullable<System.DateTime> LastActivity { get; set; }
        public string UserDetails { get; set; }
        public string Ip { get; set; }
        public string UserKey { get; set; }
    }
}
