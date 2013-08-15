using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebChat.Model;

namespace Repositoty
{
    public class ChatRepository : BaseRepository<Chat>
    {
        public ChatRepository(DbContext dbContext)
            : base(dbContext)
        {

        }

        public override Chat Update(int id, Chat entity)
        {
            var chat = this.entitySet.Find(id);
            chat.Seen = 1;
            this.dbContext.SaveChanges();
            return chat;
        }
    }
}
