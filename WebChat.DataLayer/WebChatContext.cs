using System;
using System.Data.Entity;
using System.Linq;
using WebChat.Models;

namespace WebChat.DataLayer
{
    public class WebChatContext : DbContext
    {
        public WebChatContext()
            : base("WebChatDb")
        {
            Database.SetInitializer<WebChatContext>(new DropCreateDatabaseIfModelChanges<WebChatContext>());
        }

        //public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ChatSession> Sessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
