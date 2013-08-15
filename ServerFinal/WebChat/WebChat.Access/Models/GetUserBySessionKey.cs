using Repositoty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebChat.Model;

namespace WebChat.Access.Models
{
    public static class GetUserBySessionKey
    {
        public static int Get(string key)
        {
            IRepository<User> repository = new UserRepository(new ChatEntities());

            var allUsers = repository.All();

            var userCorrect =
                (from user in allUsers
                 where user.SessionKey == key
                 select user).FirstOrDefault();

            if (userCorrect == null)
            {
                return 0;
            }

            return userCorrect.Id;
        }
    }
}