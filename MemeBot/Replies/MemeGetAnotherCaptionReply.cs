using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MemeBot.Replies
{
    class MemeGetAnotherCaptionReply : Reply
    {
        public override async Task<Reply> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var text = message.Text.Trim();

            Console.WriteLine($"Received \"{text}\" answer in {chatId} chat from @{message.Chat.Username}");

            var toLower = text.ToLower();
            if (toLower == "yes")
            {
                //add another caption
                ChatStates.AddCaption(chatId);

                //request caption
                await client.SendTextMessageAsync(chatId, $"Send me a caption!");

                return new MemeGetCaptionTextReply();
            }

            //send meme image
            var imageStream = ChatStates.GetMeme(chatId);
            await client.SendPhotoAsync(chatId, imageStream, "Here is your meme!");

            return null;
        }
    }
}