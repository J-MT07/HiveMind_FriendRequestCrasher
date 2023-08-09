using HiveMind_FriendRequestCrasher.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRChat.API.Api;
using VRChat.API.Client;
using VRChat.API.Model;
using static System.Net.WebRequestMethods;

namespace HiveMind_FriendRequestCrasher.Modules
{
    public class MassFriendRequest
    {
        private static List<string> _accountDatabase { get; set; } = new List<string>();
        private static List<AccountDataObject> _knownAccounts = new List<AccountDataObject>();

        public static void Start()
        {
            Menu.Log("VRChat/MassFriendRequest", "Initializing MassFriendRequest Module...", ConsoleColor.Cyan);
            _accountDatabase = System.IO.File.ReadAllLines($"Accounts.txt").ToList();
            Menu.Log("VRChat/MassFriendRequest", $"Loaded {LoadAccounts()} accounts from database...", ConsoleColor.Gray);

        }

        public static int LoadAccounts()
        {
            int accountNumber = 0;
            foreach (var account in _accountDatabase)
            {
                try
                {
                    _knownAccounts.Add(new AccountDataObject(account.Split(':')[0], account.Split(':')[1], account.Split(':')[2]));
                }
                catch (Exception ex)
                {
                    Menu.Log("VRChat/MassFriendRequest/Exception_Handler", $"Error occured when creating an account object for line {accountNumber} in Accounts.txt\n{ex}", ConsoleColor.Red);
                }
                accountNumber++;
            }
            return _knownAccounts.Count;
        }

        public static void InitializeAccounts()
        {
            foreach (var account in _knownAccounts)
            {
                try
                {
                    account.vRChatConfiguration = new Configuration() { BasePath = "https://api.vrchat.cloud/api/1", Username = account.username, Password = account.password, UserAgent = $"xenosiaspace_friendrequester/v0.0.1" };
                    account.vRChatConfiguration.AddApiKey("apiKey", "JlE5Jldo5Jibnk5O5hTx6XVqsJu4WJ26");
                    if (account.cookie != string.Empty)
                        account.vRChatConfiguration.DefaultHeaders.Add("Cookie", $"apiKey={"JlE5Jldo5Jibnk5O5hTx6XVqsJu4WJ26"}; auth={account.cookie}");
                    
                    account.authenticationApi = new AuthenticationApi(account.vRChatConfiguration);
                    if (account.authenticationApi.GetCurrentUser() == null)
                    {
                        try
                        {
                            Console.Write($"{account.username} -> 2FA Required: ");
                            Verify2FAResult verify2FAResult = account.authenticationApi.Verify2FA(new TwoFactorAuthCode(Console.ReadLine()));
                            VerifyAuthTokenResult resultToken = account.authenticationApi.VerifyAuthToken();
                            Console.WriteLine($"Cookie: {resultToken.Token}");
                        }
                        catch (Exception ex)
                        {
                            string errorLogNumber = new Random().Next(10, 99999).ToString();
                            Menu.Log("VRChat/MassFriendRequest/Exception_Handler", $"Error Occured While Logging in to {account.username} | \"Crash-{errorLogNumber}.txt\"", ConsoleColor.Red);
                            System.IO.File.WriteAllText($"Error-{errorLogNumber}.txt", ex.ToString());
                            continue;
                        }
                    }


                    account.authenticationApi = new AuthenticationApi(account.vRChatConfiguration);
                    account.currentUser = account.authenticationApi.GetCurrentUser();
                    account.friendsApi = new FriendsApi(account.vRChatConfiguration);
                    account.isLoggedIn = true;
                }
                catch (Exception ex)
                {
                    string errorLogNumber = new Random().Next(10, 99999).ToString();
                    Menu.Log("VRChat/MassFriendRequest/Exception_Handler", $"Error Occured | \"Crash-{errorLogNumber}.txt\"", ConsoleColor.Red);
                    System.IO.File.WriteAllText($"Error-{errorLogNumber}.txt", ex.ToString());
                }
                Menu.Log("VRChat/MassFriendRequest", $"Logged in to {account.username} successfully", ConsoleColor.Green);
                Thread.Sleep(3000);
            }
          
        }

        public static void SendMassFriendRequest(string userId)
        {
            foreach (var account in _knownAccounts)
            {
                try
                {
                    if (!account.isLoggedIn)
                        continue;
                    else
                        account.friendsApi.Friend(userId);
                }
                catch { }

                Thread.Sleep(3500 + new Random().Next(1244, 4259));
            }
        }

    }
}
