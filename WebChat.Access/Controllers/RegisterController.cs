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
    public class RegisterController : ApiController
    {
        private IRepository<User> repository;
        public RegisterController()
        {
            this.repository = new UserRepository(new ChatEntities());
        }


        public UserInfo Post([FromBody]UserRegistry userInfo)
        {
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
                PictureUrl = userInfo.PictureUrl,
                UserDetails = userInfo.Details,
                LastActivity = DateTime.Now,
                SessionKey = StringGenerator.Generate(Varibles.SessionKeyLenght)
            };

            var result = this.repository.Add(newUser);

            var respons = new UserInfo()
            {
                Id = newUser.Id,
                PictureUrl = newUser.PictureUrl,
                SessionKey = newUser.SessionKey,
                UserDetails = newUser.UserDetails,
                UserName = newUser.Username
            };

            return respons;
        }
    }
}
