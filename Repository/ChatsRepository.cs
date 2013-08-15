using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Models;

namespace Repository
{
    public class ChatsRepository : BaseRepository<Chat>
    {
        public ChatsRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
        public void Update(int id, Chat entity)
        {
        }
    }
}
