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
            var text = message.Text.Trim();

            Debug.WriteReceived(message);

            //check if vertical aligment parameter is valid
            if (!Caption.ContainsVerticalAligment(text))
            {
                WrongReplyParameters(message, client);
                return null;
            }

            //set vertical aligment
            ChatStates.SetVerticalAligment(chatId, text);

            //request answer
            await client.SendTextMessageAsync(chatId, $"Do you want to add more captions?\nYes\nNo");

            return new MemeGetAnotherCaptionReply();
        }
    }
}