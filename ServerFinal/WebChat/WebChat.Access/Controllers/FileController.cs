using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebChat.Classes;
using Repositoty;
using WebChat.Model;
using System.Data.Entity;
using WebChat.Access.Models;

namespace WebChat.Access.Controllers
{
    public class FileController : ApiController
    {
        private IRepository<SendFile> repository;
        public FileController()
        {
            this.repository = new FileRepository(new ChatEntities());
        }

        [HttpGet]
        [ActionName("get")]
        public List<File> ResiveFiles(string id)
        {
            int userId = GetUserBySessionKey.Get(id);

            if (userId == 0)
            {
                throw new ArgumentException();
            }

            var files = repository.All();

            var fileList =
                (from file in files
                 where file.ReceiverId == userId && file.MessageStatus == Varibles.UploadingFile ||
                 file.MessageStatus == Varibles.FileIsReadyToDownload
                 select file);

            List<File> list = new List<File>();

            foreach (var item in fileList)
            {
                var newFile = new File()
                {
                    status = item.MessageStatus,
                    Url = item.Content,
                    ResiverId = item.SenderId
                };

                list.Add(newFile);
            }

            return list;
        }

        [HttpPost]
        [ActionName("send")]
        public int SendFiles(string id, [FromBody]int resiverId)
        {
            int userId = GetUserBySessionKey.Get(id);

            if (userId == 0)
            {
                throw new ArgumentException();
            }

            SendFile newFile = new SendFile()
            {
                Content = "",
                MessageStatus = 0,
                ReceiverId = resiverId,
                SenderId = userId,
            };

            this.repository.Add(newFile);

            return newFile.Id;
        }

        [HttpPost]
        [ActionName("upload")]
        public void FileIsUpload(string id, [FromBody]UploadFile file)
        {
            int userId = GetUserBySessionKey.Get(id);

            if (userId == 0)
            {
                throw new ArgumentException();
            }

            this.repository.Update(file.Id, new SendFile() { Content = file.Content, MessageStatus = 1});
        }

        [HttpPost]
        [ActionName("complete")]
        public void FileIsUpload(string id, [FromBody]int messageid)
        {
            int userId = GetUserBySessionKey.Get(id);

            if (userId == 0)
            {
                throw new ArgumentException();
            }

            this.repository.Update(messageid, new SendFile() { Content = "", MessageStatus = 3 });
        }

        [HttpPost]
        [ActionName("isFinished")]
        public bool IsCompleted(string id, [FromBody]int messageid)
        {
            int userId = GetUserBySessionKey.Get(id);

            if (userId == 0)
            {
                throw new ArgumentException();
            }

            var allFiles = this.repository.All();

            var searchFile =
                (from file in allFiles
                 where file.Id == messageid && file.MessageStatus == 3
                 select file).FirstOrDefault();

            if (searchFile == null)
            {
                return false;
            }

            return true;
        }
    }
}
