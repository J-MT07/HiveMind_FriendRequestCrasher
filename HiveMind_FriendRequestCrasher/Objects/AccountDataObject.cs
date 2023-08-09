using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRChat.API.Api;
using VRChat.API.Client;
using VRChat.API.Model;

namespace HiveMind_FriendRequestCrasher.Objects
{
    public class AccountDataObject
    {
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string cookie { get; set; } = string.Empty;

        public bool isLoggedIn { get; set; } = false;

        public Configuration vRChatConfiguration { get; set; }
        public CurrentUser currentUser { get; set; }
        public AuthenticationApi authenticationApi { get; set; }
        public FriendsApi friendsApi { get; set; }

        public AccountDataObject(string _username, string _password, string _cookie)
        {
            username = _username;
            password = _password;
            cookie = _cookie;
        }

    }
}
