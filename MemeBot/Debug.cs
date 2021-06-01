using System;
using Telegram.Bot.Types;

namespace MemeBot
{
    public static class Debug
    {
        public static void WriteReceived(Message message)
        {
            var chatId = message.Chat.Id;
            Console.WriteLine($"{DateTime.Now.ToString("h:mm:ss tt")} Received \"{message.Text.Trim().Replace("\n", "\\n")}\" in {chatId} chat from @{message.Chat.Username}");
        }

        public static void WriteReceived(Message message, string recv)
        {
            var chatId = message.Chat.Id;
            Console.WriteLine($"{DateTime.Now.ToString("h:mm:ss tt")} Received {recv} in {chatId} chat from @{message.Chat.Username}");
        }
    }
}
