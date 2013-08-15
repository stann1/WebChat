using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebChat.Model;

namespace Repositoty
{
    public class UserRepository: BaseRepository<User>
    {
        public UserRepository(DbContext dbContext)
            : base(dbContext)
        {

        }

        public override User Update(int id, User entity)
        {
            var user = this.entitySet.Find(id);
            user.LastActivity = entity.LastActivity;
            this.dbContext.SaveChanges();
            return user;
        }
    }
}
