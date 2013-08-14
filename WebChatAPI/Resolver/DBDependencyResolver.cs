using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using System.Web.Providers.Entities;
using WebChat.DataLayer;
using WebChat.Models;
using WebChat.Repository;
using WebChatAPI.Controllers;

namespace WebChatAPI.Resolver
{
    public class DBDependencyResolver : IDependencyResolver
    {
        private static DbContext webChatContext = new WebChatContext();

        private static IRepository<ChatSession> sessionRepository = new SessionRepository(webChatContext);

        private static IRepository<ChatUser> userRepository = new UserRepository(webChatContext);


        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(SessionsController))
            {
                return new SessionsController(sessionRepository);
            }
            else if (serviceType == typeof(UsersController))
            {
                return new UsersController(userRepository);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public void Dispose()
        {
            
        }
    }
}