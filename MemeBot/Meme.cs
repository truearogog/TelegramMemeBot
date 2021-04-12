using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace MemeBot
{
    class Meme
    {
        public Bitmap bitmap { get; set; }
        public List<Caption> captions = new List<Caption>();

        public Meme() { }

        public Meme(Bitmap bitmap, List<Caption> captions)
        {
            this.bitmap = bitmap;
            this.captions = captions;
        }

        public bool Ready()
        {
            return bitmap != null && captions.TrueForAll(caption => caption.Ready());
        }

        public MemoryStream GetMeme()
        {
            MemoryStream ms = new MemoryStream();
            using (Bitmap image = new Bitmap(bitmap))
            {
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    captions.ForEach(caption =>
                    {
                        GraphicsPath path = new GraphicsPath();
                        caption.Draw(image, path);

                        graphics.FillPath(new SolidBrush(Color.White), path);
                        graphics.DrawPath(new Pen(Color.Black, 2), path);
                    });
                }
                image.Save(ms, ImageFormat.Png);
            }
            ms.Position = 0;
            return ms;
        }
    }
}
