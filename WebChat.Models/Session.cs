using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public int FisrtUserId { get; set; }
        public int SecondUserId { get; set; }
        public string ConnectionString { get; set; }
        public string Pass { get; set; }
    }
}
