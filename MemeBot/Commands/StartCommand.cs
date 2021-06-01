using MemeBot.Replies;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MemeBot.Commands
{
    class StartCommand : Command
    {
        public override string Name => "/start";

        public override async Task<Reply> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            Debug.WriteReceived(message);

            await client.SendTextMessageAsync(chatId, "Just write /meme and create your own meme!");

            return null;
        }
    }
}
