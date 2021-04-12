using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MemeBot.Replies
{
    class MemeGetImageReply : Reply
    {
        public override async Task<Reply> Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;

            //check if image parameter is valid
            if (message.Photo.Length == 0)
            {
                WrongReplyParameters(message, client);
                return null;
            }

            //download image from telegram server
            var file = await client.GetFileAsync(message.Photo.LastOrDefault().FileId);
            Bitmap bitmap;
            using (MemoryStream ms = new MemoryStream())
            {
                await client.DownloadFileAsync(file.FilePath, ms);
                bitmap = new Bitmap(ms);
            }

            //resize image
            float minimalSize = 700;
            if (bitmap.Width < minimalSize || bitmap.Height < minimalSize)
            {
                double smallerSide = Math.Min(bitmap.Width, bitmap.Height);
                double resizePercentage = minimalSize / smallerSide;
                Bitmap resizedBitmap = ImageUtilities.ResizeImage(bitmap, (int)(bitmap.Width * resizePercentage), (int)(bitmap.Height * resizePercentage));
                bitmap = new Bitmap(resizedBitmap);
            }

            Console.WriteLine($"Received {file.FileSize / 1024} KB {bitmap.Width}x{bitmap.Height} image in {chatId} chat from @{message.Chat.Username}");

            //add chat and set meme image
            ChatStates.AddChat(chatId);
            ChatStates.SetImage(chatId, bitmap);

            //request caption
            await client.SendTextMessageAsync(chatId, $"Send me a caption!");

            return new MemeGetCaptionReply();
        }
    }
}
