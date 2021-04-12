using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MemeBot.Replies
{
    class MemeGetCaptionReply : Reply
    {
        public override async Task<Reply> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            Console.WriteLine($"Received \"{message.Text}\" caption in {chatId} chat from @{message.Chat.Username}");

            //set meme caption
            ChatStates.SetCaption(chatId, message.Text);

            //request font size
            await client.SendTextMessageAsync(chatId, $"Send me font size (from 0.0 to 1.0)!");

            return new MemeGetFontSizeReply();
        }
    }
}