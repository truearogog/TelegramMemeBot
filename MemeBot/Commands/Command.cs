using Telegram.Bot;
using Telegram.Bot.Types;
using MemeBot.Replies;
using System.Threading.Tasks;

namespace MemeBot.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract Task<Reply> Execute(Message message, TelegramBotClient client);

        public bool Contains(string command)
        {
            if (command == null)
                return false;
            return this.Name == command.Trim().Split(' ')[0];
        }

        protected async void WrongCommandParameters(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, "Wrong command parameters..");
        }
    }
}
