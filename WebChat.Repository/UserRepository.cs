using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Models;

namespace WebChat.Repository
{
    public class UserRepository: BaseRepository<User>
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
