using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiveMind_FriendRequestCrasher
{
    internal class Menu
    {
        public static List<LogObject> LogContent { get; set; } = new List<LogObject>() { };

        public static void Log(string path, string input, ConsoleColor color)
        {
            Menu.LogContent.Add(new LogObject(path, input, color));
        }

        public static void Print(LogObject log)
        {
            Console.Write($"[");
            switch (log.Color)
            {
                case ConsoleColor.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case ConsoleColor.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case ConsoleColor.Blue:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case ConsoleColor.DarkRed:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case ConsoleColor.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case ConsoleColor.Cyan:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
            }
            Console.Write($"{(log.Path == string.Empty ? "" : $"{log.Path}")}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"] - {log.Data}");
        }

        public class LogObject
        {
            public DateTime DateTime { get; set; } = DateTime.Now;
            public string Path { get; set; } = "";
            public string Data { get; set; } = "";
            public ConsoleColor Color { get; set; } = ConsoleColor.Gray;
            public LogObject(string path, string input, ConsoleColor color)
            {
                Path = $"HiveMind{(path == string.Empty ? "" : $"/{path}")}";
                Data = input;
                DateTime = DateTime.Now;
                Color = color;
            }
        }
    }
}
