using Telegram.Bot;
using Telegram.Bot.Types;
using MemeBot.Commands;
using System.Collections.Generic;
using System;

namespace MemeBot
{
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands { get => commandsList.AsReadOnly(); }

        public static TelegramBotClient Get()
        {
            if (client != null)
            {
                return client;
            }
            
            commandsList = new List<Command>();
            commandsList.Add(new StartCommand());
            commandsList.Add(new MemeCommand());

            client = new TelegramBotClient(AppSettings.Key) { Timeout = TimeSpan.FromSeconds(10)};

            return client;
        }

        private static async void TryExecuteCommand(Message message)
        {
            foreach (var command in commandsList)
            {
                if (command.Contains(message.Text) || command.Contains(message.Caption))
                {
                    var chatId = message.Chat.Id;
                    var currentReply = await command.Execute(message, client);
                    ChatStates.SetCurrentReply(chatId, currentReply);
                    return;
                }
            }
        }

        private static async void TryExecuteReply(Message message)
        {
            var chatId = message.Chat.Id;
            var currentReply = await ChatStates.GetCurrentReply(chatId).Execute(message, client);
            ChatStates.SetCurrentReply(chatId, currentReply);
        }

        public static void TryExecute(Message message)
        {
            var chatId = message.Chat.Id;

            ChatStates.AddChat(chatId);

            if (ChatStates.GetCurrentReply(chatId) != null)
            {
                TryExecuteReply(message);
            }
            else
            {
                TryExecuteCommand(message);
            }
        }
    }
}
