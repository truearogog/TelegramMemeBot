using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MemeBot.Replies
{
    public abstract class Reply
    {
        public abstract Task<Reply> Execute(Message message, TelegramBotClient client);

        protected async void WrongReplyParameters(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, "Wrong reply parameters..");
        }
    }
}
