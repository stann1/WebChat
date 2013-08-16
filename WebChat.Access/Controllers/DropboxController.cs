using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using System.Diagnostics;

using Spring.Social.Dropbox.Api;
using Spring.Social.Dropbox.Connect;
using Spring.IO;

namespace WebChat.Access.Controllers
{
    public class DropboxController : ApiController
    {
        private const string DropboxAppKey = "uzgimkv89ottpft";
        private const string DropboxAppSecret = "vtzv339z54aen0x";       
        
        private string DropboxShareFile(string path, string filename)
        {
            //DropboxServiceProvider dropboxServiceProvider =
            //    new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.AppFolder);                     

            DropboxServiceProvider dropboxServiceProvider =
            new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.AppFolder);

            // Login in Dropbox

            //jd7634fajsiouvx4
            //2u27na84s3dn1ai

            IDropbox dropbox = dropboxServiceProvider.GetApi("zffjilrt712zwzae", "j4lxo1vtaepn4cu");

            Entry uploadFileEntry = dropbox.UploadFileAsync(
                new FileResource(path), filename).Result;

            var sharedUrl = dropbox.GetMediaLinkAsync(uploadFileEntry.Path).Result;
            return (sharedUrl.Url + "?dl=1"); // we can download the file directly
        }
       

        public HttpResponseMessage Post()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                string link = string.Empty;
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);                  
                    var fileName = Path.GetFileName(filePath);
                    docfiles.Add(filePath);
                    
                    link = this.DropboxShareFile(filePath, fileName);
                    File.Delete(filePath);                    
                }
                result = Request.CreateResponse(HttpStatusCode.Created, link);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }

    }
}