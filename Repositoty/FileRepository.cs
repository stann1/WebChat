using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebChat.Model;

namespace Repositoty
{
    public class FileRepository: BaseRepository<SendFile>
    {
        public FileRepository(DbContext dbContext)
            : base(dbContext)
        {

        }

        public override SendFile Update(int id, SendFile entity)
        {
            var file = this.entitySet.Find(id);
            file.MessageStatus = entity.MessageStatus;
            file.Content = entity.Content;
            this.dbContext.SaveChanges();
            return file;
        }
    }
}
