using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MemeBot.Replies
{
    class MemeGetFontReply : Reply
    {

        public override async Task<Reply> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            Console.WriteLine($"Received \"{message.Text}\" font in {chatId} chat from @{message.Chat.Username}");

            //check if font parameter is valid
            var toLower = message.Text.ToLower();
            if (Array.FindIndex(Caption.availableFonts, font => font.Equals(toLower)) < 0)
            {
                WrongReplyParameters(message, client);
                return null;
            }

            //set font size
            ChatStates.SetFont(chatId, message.Text);

            //request vertical aligment
            await client.SendTextMessageAsync(chatId, $"Send me vertical aligment style!\nChoose from these:\nup\nmiddle\ndown");

            return new MemeGetVerticalAligmentReply();
        }
    }
}