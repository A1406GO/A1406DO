using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerBackend.Models.Services
{
    public class UserService
    {
        private Dictionary<long, UserInfo> loginedUsers;
        private Dictionary<UserInfo, long> loginedToken;

        public UserService()
        {
            loginedUsers = new Dictionary<long, UserInfo>();
            loginedToken = new Dictionary<UserInfo, long>();
        }

        public void Add(long token, UserInfo user)
        {
            if (loginedToken.TryGetValue(user, out var t))
            {
                loginedUsers.Remove(t);
                loginedToken.Remove(user);
            }

            loginedUsers.Add(token, user);
            loginedToken.Add(user, token);
        }
        public bool ContainsToken(long token)
        {
            return loginedUsers.ContainsKey(token);
        }
        public bool ContainsUser(UserInfo user)
        {
            return loginedToken.ContainsKey(user);
        }
        public UserInfo FindUser(long token)
        {
            return loginedUsers[token];
        }

        public bool ValidToken(long token)
        {
            return loginedUsers.ContainsKey(token);
        }
    }
}
