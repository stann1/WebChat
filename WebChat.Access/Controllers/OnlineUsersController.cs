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
    public class OnlineUsersController : ApiController
    {
        private IRepository<User> repository;
        public OnlineUsersController()
        {
            this.repository = new UserRepository(new ChatEntities());
        }

        public IEnumerable<UserInfo> Get(string SessionKey)
        {
            int userId = GetUserBySessionKey.Get(SessionKey);

            if (userId == 0)
            {
                throw new ArgumentException();
            }

            var allUsers = this.repository.All();
            var usersOnline =
                from user in allUsers
                where user.Id != userId && user.LastActivity < DateTime.Now //Here
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
        [HttpPost]
        [ActionName("UserIsOnline")]

        public void Post(string sessionKey)
        {
            int userId = GetUserBySessionKey.Get(sessionKey);

            if (userId == 0)
            {
                throw new ArgumentException();
            }

            this.repository.Update(userId, new User() { LastActivity = DateTime.Now });
        }
    }
}
