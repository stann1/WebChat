using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.Classes
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PictureUrl { get; set; }
        public string SessionKey { get; set; }
        public string UserDetails { get; set; }
    }
}
