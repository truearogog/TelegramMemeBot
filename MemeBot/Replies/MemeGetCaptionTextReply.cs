using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MemeBot.Replies
{
    class MemeGetCaptionTextReply : Reply
    {
        public override async Task<Reply> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var text = message.Text.Trim();

            Console.WriteLine($"Received \"{text}\" caption in {chatId} chat from @{message.Chat.Username}");

            //set meme caption
            ChatStates.SetCaptionText(chatId, text);

            //request font size
            await client.SendTextMessageAsync(chatId, $"Send me font size (from 0.0 to 1.0)!");

            return new MemeGetFontSizeReply();
        }
    }
}