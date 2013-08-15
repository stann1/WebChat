using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
