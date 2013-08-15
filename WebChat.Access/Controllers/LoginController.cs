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
    public class LoginController : ApiController
    {
        private IRepository<User> repository;
        public LoginController()
        {
            this.repository = new UserRepository(new ChatEntities());
        }

        public UserInfo Post([FromBody]UserLogin userInfo)
        {
            var allUsers = this.repository.All();
            var thisUser =
                (from user in allUsers
                 where user.Username == userInfo.Username
                 && user.Pass == userInfo.Password
                 select user).FirstOrDefault();

            if (thisUser == null)
            {
                throw new ArgumentException();
            }

            var respons = new UserInfo()
            {
                Id = thisUser.Id,
                PictureUrl = thisUser.PictureUrl,
                SessionKey = thisUser.SessionKey,
                UserDetails = thisUser.UserDetails,
                UserName = thisUser.Username
            };

            return respons;
        }
    }
}
