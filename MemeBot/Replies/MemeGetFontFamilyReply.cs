using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MemeBot.Replies
{
    class MemeGetFontFamilyReply : Reply
    {

        public override async Task<Reply> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var text = message.Text.Trim();

            Console.WriteLine($"Received \"{text}\" font family in {chatId} chat from @{message.Chat.Username}");

            //check if font family parameter is valid
            if (!Caption.ContainsFontFamily(text))
            {
                WrongReplyParameters(message, client);
                return null;
            }

            //set font family
            ChatStates.SetFontFamily(chatId, text);

            //request vertical aligment
            await client.SendTextMessageAsync(chatId, $"Send me vertical aligment style!\nChoose from these:\n{ string.Join("\n", Caption.verticalAligments) }");

            return new MemeGetVerticalAligmentReply();
        }
    }
}