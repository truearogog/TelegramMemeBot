using System;
using Telegram.Bot.Args;

namespace MemeBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = Bot.Get();

            var me = client.GetMeAsync().Result;
            Console.WriteLine($"Bot id: {me.Id} Bot name: {me.FirstName} Bot username: {me.Username}");

            client.OnMessage += Bot_OnMessage;
            client.StartReceiving();

            Console.ReadKey();
        }

        private static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var message = e?.Message;

            if (message == null)
                return;

            Bot.TryExecute(message);
        }
    }
}
