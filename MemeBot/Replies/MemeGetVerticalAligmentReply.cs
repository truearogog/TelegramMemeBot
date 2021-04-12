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
            if (Array.FindIndex(Caption.verticalAligmentTypes, type => type.Equals(message.Text)) < 0)
            {
                WrongReplyParameters(message, client);
                return null;
            }

            ChatStates.SetVerticalAligment(chatId, message.Text);

            //send new image
            var imageStream = ChatStates.GetMeme(chatId);
            await client.SendPhotoAsync(chatId, imageStream, "Here is your meme!");

            return null;
        }
    }
}