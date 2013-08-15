using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebChat.Classes;
using Repository;
using WebChat.Models;
using System.Data.Entity;
using WebChat.Access.Models;

namespace WebChat.Access.Controllers
{
    public class FilesController : ApiController
    {
        private IRepository<SendFile> repository;

        public FilesController()
        {
            this.repository = new SendFilesRepository(new ChatEntities());
        }

        public List<File> Get(string SessionKey)
        {
            int userId = GetUserBySessionKey.Get(SessionKey);

            if (userId == 0)
            {
                throw new ArgumentException();
            }

            var files = repository.All();

            var fileList =
                (from file in files
                 where file.ReceiverId == userId && file.MessageStatus == Varibles.UploadingFile && 
                 file.MessageStatus == Varibles.FileIsReadyToDownload
                 select file);

            List<File> list = new List<File>();

            foreach (var item in fileList)
            {
                var newFile = new File()
                {
                    status = item.MessageStatus,
                    Url = item.Content,
                    FromId = item.SenderId
                };

                list.Add(newFile);
            }

            return list;
        }

        public void Put()
        {
        }
    }
}
