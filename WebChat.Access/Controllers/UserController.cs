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
    public class UserController : ApiController
    {
        private IRepository<User> repository;
        public UserController()
        {
            this.repository = new UserRepository(new ChatEntities());
        }

        [HttpPost]
        [ActionName("register")]
        public UserInfo Register([FromBody]UserRegistry userInfo)
        {
            string defaultPic = "default.jpg";
            if (userInfo.PictureUrl != null)
            {
                defaultPic = userInfo.PictureUrl;
            }
            var allUsers = this.repository.All();
            var userExist =
                (from user in allUsers
                 where user.Username == userInfo.Username
                 select user).FirstOrDefault();

            if (userExist != null && userInfo.Password.Length != Varibles.PasswordLenght)
            {
                throw new ArgumentException();
            }

            var newUser = new User()
            {
                Username = userInfo.Username,
                Pass = userInfo.Password,
                PictureUrl = defaultPic,
                UserDetails = userInfo.Details,
                LastActivity = DateTime.Now,
                SessionKey = StringGenerator.Generate(Varibles.SessionKeyLenght)
            };

            var result = this.repository.Add(newUser);

            var respons = new UserInfo()
            {
                Id = newUser.Id,
                PictureUrl = defaultPic,
                SessionKey = newUser.SessionKey,
                UserDetails = newUser.UserDetails,
                UserName = newUser.Username
            };

            return respons;
        }

        [HttpPost]
        [ActionName("login")]
        public UserInfo Login([FromBody]UserLogin userLogin)
        {
            var allUsers = this.repository.All();
            var thisUser =
                (from user in allUsers
                 where user.Username == userLogin.Username
                 && user.Pass == userLogin.Password
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

        [HttpGet]
        [ActionName("online")]
        public void MakeUserOnline(string id)
        {
            int userId = GetUserBySessionKey.Get(id);

            if (userId == 0)
            {
                this.Request.CreateErrorResponse(HttpStatusCode.NotFound,"User Not Found");
            }

            this.repository.Update(userId, new User() { LastActivity = DateTime.Now });
        }

        [HttpGet]
        [ActionName("onlineUsers")]
        public List<UserInfo> GetOnlineUsers(string id)
        {
            int userId = GetUserBySessionKey.Get(id);

            if (userId == 0)
            {
                throw new ArgumentException();
            }

            DateTime onlineTime = DateTime.Now.AddMinutes(-5);

            var allUsers = this.repository.All();
            var usersOnline =
                from user in allUsers
                where user.LastActivity > onlineTime 
                && user.Id != userId
                select user;

            List<UserInfo> list = new List<UserInfo>();

            foreach (var item in usersOnline)
            {
                var newUserOnline = new UserInfo()
                {
                    Id = item.Id,
                    PictureUrl = item.PictureUrl,
                    UserDetails = item.UserDetails,
                    UserName = item.Username
                };

                list.Add(newUserOnline);
            }

            return list;
        }
    }
}
