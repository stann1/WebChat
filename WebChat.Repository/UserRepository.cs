using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Models;
using WebChat.DataLayer;


namespace WebChat.Repository
{
    public class UserRepository: BaseRepository<ChatUser>
    {
        private const int Sha1CodeLength = 40;
        
        private static Random rand = new Random();

        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        private const string SessionKeyChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const int SessionKeyLen = 50;

        private const string ValidUsernameChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM_1234567890";
        private const string ValidNicknameChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM_1234567890 -";
        private const int MinUsernameNicknameChars = 4;
        private const int MaxUsernameNicknameChars = 30;

        private static void ValidateSessionKey(string sessionKey)
        {
            if (sessionKey.Length != SessionKeyLen || sessionKey.Any(ch => !SessionKeyChars.Contains(ch)))
            {
                throw new ServerErrorException("Invalid Password", "ERR_INV_AUTH");
            }
        }

        private static string GenerateSessionKey(int userId)
        {
            StringBuilder keyChars = new StringBuilder(50);
            keyChars.Append(userId.ToString());
            while (keyChars.Length < SessionKeyLen)
            {
                int randomCharNum;
                lock (rand)
                {
                    randomCharNum = rand.Next(SessionKeyChars.Length);
                }
                char randomKeyChar = SessionKeyChars[randomCharNum];
                keyChars.Append(randomKeyChar);
            }
            string sessionKey = keyChars.ToString();
            return sessionKey;
        }

        private static void ValidateUsername(string username)
        {
            if (username == null || username.Length < MinUsernameNicknameChars || username.Length > MaxUsernameNicknameChars)
            {
                throw new ServerErrorException("Username should be between 4 and 30 symbols long", "INV_USRNAME_LEN");
            }
            else if (username.Any(ch => !ValidUsernameChars.Contains(ch)))
            {
                throw new ServerErrorException("Username contains invalid characters", "INV_USRNAME_CHARS");
            }
        }
        
        private static void ValidateAuthCode(string authCode)
        {
            if (authCode.Length != Sha1CodeLength)
            {
                throw new ServerErrorException("Invalid authentication code length", "INV_USR_AUTH_LEN");
            }
        }

        /* public members */

        public static void CreateUser(string username, string authCode)
        {
            ValidateUsername(username);
            ValidateAuthCode(authCode);
            using (WebChatContext context = new WebChatContext())
            {
                var usernameToLower = username.ToLower();

                var dbUser = context.Users.FirstOrDefault(u => u.Username.ToLower() == usernameToLower);

                if (dbUser != null)
                {
                    if (dbUser.Username.ToLower() == usernameToLower)
                    {
                        throw new ServerErrorException("Username already exists", "ERR_DUP_USR");
                    }
                }

                dbUser = new ChatUser()
                {
                    Username = usernameToLower,                    
                    Pass = authCode
                };
                context.Users.Add(dbUser);
                context.SaveChanges();
            }

        }

        public static string LoginUser(string username, string authCode, out bool success)
        {
            ValidateUsername(username);
            ValidateAuthCode(authCode);
            var context = new WebChatContext();
            using (context)
            {
                var usernameToLower = username.ToLower();
                var user = context.Users.FirstOrDefault(u => u.Username.ToLower() == usernameToLower && u.Pass == authCode);
                if (user == null)
                {
                    throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
                }

                var sessionKey = GenerateSessionKey((int)user.ChatUserId);
                user.UserKey = sessionKey;
                success = true;
                context.SaveChanges();
                return sessionKey;
            }
        }

        public static int LoginUser(string sessionKey)
        {
            ValidateSessionKey(sessionKey);
            var context = new WebChatContext();
            using (context)
            {
                var user = context.Users.FirstOrDefault(u => u.UserKey == sessionKey);
                if (user == null)
                {
                    throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
                }
                return (int)user.ChatUserId;
            }
        }

        public static void LogoutUser(string sessionKey)
        {
            ValidateSessionKey(sessionKey);
            var context = new WebChatContext();
            using (context)
            {
                var user = context.Users.FirstOrDefault(u => u.UserKey == sessionKey);
                if (user == null)
                {
                    throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
                }
                user.UserKey = null;
                context.SaveChanges();
            }
        }

        //public static IEnumerable<User> GetAllUsers()
        //{
        //    var context = new BullsAndCowsEntities();
        //    using (context)
        //    {
        //        var users =
        //            (from user in context.Users
        //             select new UserScore()
        //             {
        //                 Nickname = user.Nickname,
        //                 Score = user.Scores.Any() ? user.Scores.Sum(sc => sc.Value) : 0
        //             });
        //        return users.ToList();
        //    }
        //}
    }
}
