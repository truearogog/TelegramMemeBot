using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MemeBot.Replies
{
    class MemeGetVerticalAligmentReply : Reply
    {
        public override async Task<Reply> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            Console.WriteLine($"Received \"{message.Text}\" vertical aligment in {chatId} chat from @{message.Chat.Username}");

            //check if vertical aligment parameter is valid
            var toLower = message.Text.ToLower();
            if (Array.FindIndex(Caption.verticalAligmentTypes, type => type.Equals(toLower)) < 0)
            {
                WrongReplyParameters(message, client);
                return null;
            }

            ChatStates.SetVerticalAligment(chatId, message.Text);

            //request answer
            await client.SendTextMessageAsync(chatId, $"Do you want to add more captions?\nYes\nNo");

            return new MemeGetAnotherCaptionReply();
        }
    }
}