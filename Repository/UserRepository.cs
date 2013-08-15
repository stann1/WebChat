using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Models;
using System.Data.Entity;

namespace Repository
{
    public class UserRepository: BaseRepository<User>
    {
        public UserRepository(DbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
