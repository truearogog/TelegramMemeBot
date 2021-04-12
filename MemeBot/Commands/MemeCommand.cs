using System;
using System.Linq;
using System.Drawing;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Types;
using MemeBot.Replies;
using System.Threading.Tasks;

namespace MemeBot.Commands
{
    class MemeCommand : Command
    {
        public override string Name => "/meme";

        public override async Task<Reply> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            //request meme image
            await client.SendTextMessageAsync(chatId, "Send me meme image!");

            return new MemeGetImageReply();
        }
    }
}