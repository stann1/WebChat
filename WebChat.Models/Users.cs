//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebChat.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Messages = new HashSet<Message>();
            this.Messages1 = new HashSet<Message>();
            this.Sessions = new HashSet<Session>();
            this.Sessions1 = new HashSet<Session>();
        }
    
        public int Id { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public string PictureUrl { get; set; }
        public Nullable<System.DateTime> LastActivity { get; set; }
        public string UserDetails { get; set; }
        public string Ip { get; set; }
    
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Message> Messages1 { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<Session> Sessions1 { get; set; }
    }
}
