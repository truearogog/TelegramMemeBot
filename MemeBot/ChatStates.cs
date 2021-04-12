using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MemeBot.Replies;

namespace MemeBot
{
    class State
    {
        public Meme meme;
        public Reply currentReply;

        public State(Meme meme, Reply currentReply)
        {
            this.meme = meme;
            this.currentReply = currentReply;
        }
    }

    public static class ChatStates
    {
        static Dictionary<long, State> chatStates = new Dictionary<long, State>();

        public static void AddChat(long chatId)
        {
            if (ContainsChat(chatId))
                return;
            chatStates.Add(chatId, new State(new Meme(), null));
            chatStates[chatId].meme.captions.Add(new Caption());
        }

        public static bool ContainsChat(long chatId)
        {
            return chatStates.ContainsKey(chatId);
        }

        public static Reply GetCurrentReply(long chatId)
        {
            return chatStates[chatId].currentReply;
        }

        public static void SetCurrentReply(long chatId, Reply reply)
        {
            if (!ContainsChat(chatId))
                return;
            chatStates[chatId].currentReply = reply;
        }

        public static void SetImage(long chatId, Bitmap bitmap)
        {
            chatStates[chatId].meme.bitmap = bitmap;
        }

        public static void SetCaption(long chatId, string caption)
        {
            chatStates[chatId].meme.captions[0].caption = caption;
        }

        public static void SetFontSize(long chatId, float size)
        {
            chatStates[chatId].meme.captions[0].size = size;
        }

        public static void SetVerticalAligment(long chatId, string verticalAligment)
        {
            chatStates[chatId].meme.captions[0].verticalAligment = verticalAligment;
        }

        public static MemoryStream GetMeme(long chatId)
        {
            if (!chatStates[chatId].meme.Ready())
                return null;
            Meme meme = chatStates[chatId].meme;
            chatStates.Remove(chatId);
            return meme.GetMeme();
        }
    }
}
