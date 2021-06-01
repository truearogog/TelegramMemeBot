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
            var text = message.Text.Trim();

            Debug.WriteReceived(message);

            //check if font size parameter is valid
            float f;
            if (!float.TryParse(text, out f))
            {
                WrongReplyParameters(message, client);
                return null;
            }

            //set font size
            ChatStates.SetFontSize(chatId, f);

            //request vertical aligment
            await client.SendTextMessageAsync(chatId, $"Send me font!\nChoose from these:\n{ string.Join("\n", Caption.fontFamilies) }");

            return new MemeGetFontFamilyReply();
        }
    }
}