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

        public static void SetCaptionText(long chatId, string captionText)
        {
            chatStates[chatId].meme.captions[chatStates[chatId].meme.captions.Count - 1].Text = captionText;
        }

        public static void SetFontSize(long chatId, float size)
        {
            chatStates[chatId].meme.captions[chatStates[chatId].meme.captions.Count - 1].FontSize = size;
        }

        public static void SetFontFamily(long chatId, string font)
        {
            chatStates[chatId].meme.captions[chatStates[chatId].meme.captions.Count - 1].FontFamily = font;
        }

        public static void SetVerticalAligment(long chatId, string verticalAligment)
        {
            chatStates[chatId].meme.captions[chatStates[chatId].meme.captions.Count - 1].VerticalAligment = verticalAligment;
        }

        public static void AddCaption(long chatId)
        {
            chatStates[chatId].meme.captions.Add(new Caption());
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
