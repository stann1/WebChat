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

        public User Update(int id, DateTime date)
        {
            var user = this.entitySet.Find(id);

            user.LastActivity = date;
            this.dbContext.SaveChanges();

            return user;
        }
    }
}
