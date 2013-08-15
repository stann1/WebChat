using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebChat.Access.Models
{
    public static class Varibles
    {
        public const int SessionKeyLenght = 40;
        public const int PasswordLenght = 40;
        public const int SeenMessage = 1;
        public const int NotSeenMessage = 0;
        public const int UploadingFile = 0;
        public const int FileIsReadyToDownload = 1;
        public const int FileSend = 2;
    }
}