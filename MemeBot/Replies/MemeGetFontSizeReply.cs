using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MemeBot.Replies
{
    class MemeGetFontSizeReply : Reply
    {

        public override async Task<Reply> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            Console.WriteLine($"Received \"{message.Text}\" font size in {chatId} chat from @{message.Chat.Username}");

            //check if font size parameter is valid
            float f;
            if (!float.TryParse(message.Text, out f))
            {
                WrongReplyParameters(message, client);
                return null;
            }

            //set font size
            ChatStates.SetFontSize(chatId, f);

            //request vertical aligment
            await client.SendTextMessageAsync(chatId, $"Send me font!\nChoose from these:\nArial\nImpact\nCalibri\nCambria\nTimes New Roman");

            return new MemeGetFontReply();
        }
    }
}