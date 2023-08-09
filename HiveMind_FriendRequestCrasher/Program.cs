using HiveMind_FriendRequestCrasher.Modules;

namespace HiveMind_FriendRequestCrasher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MassFriendRequest.Start();
            MassFriendRequest.InitializeAccounts();

            while (true)
            {
                Console.Clear();
                Console.Write($"UserID: ");

                MassFriendRequest.SendMassFriendRequest(Console.ReadLine());
                Thread.Sleep(5000);
            }

            Thread.Sleep(-1);
        }
    }
}