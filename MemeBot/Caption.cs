using System.Drawing;
using System.Drawing.Drawing2D;

namespace MemeBot
{
    class Caption
    {
        public string text { get; set; }
        public string verticalAligment { get; set; }
        public static string[] verticalAligmentTypes = { "up", "down", "middle" };
        public float size { get; set; }
        public string font { get; set; } = "impact";
        public static string[] availableFonts = { "arial", "impact", "calibri", "cambria", "times new roman" };

        public Caption() { }

        public bool Ready()
        {
            return text != null && verticalAligment != null && font != null && size >= 0;
        }

        public void Draw(Bitmap image, GraphicsPath path)
        {
            float fontSize = image.Width / text.Length * 2 * this.size;

            path.AddString(text, new FontFamily(font), 0, fontSize, new PointF(0, 0), StringFormat.GenericDefault);

            float offsetX = image.Width / 2 - path.GetBounds().Width / 2;
            float offsetY;
            switch (verticalAligment)
            {
                case "up":
                    offsetY = 5;
                    break;
                case "middle":
                    offsetY = image.Height / 2 - path.GetBounds().Height / 2;
                    break;
                case "down":
                    offsetY = image.Height - fontSize - 2;
                    break;
                default:
                    offsetY = 5;
                    break;
            }
            Matrix translateMatrix = new Matrix();
            translateMatrix.Translate(-path.GetBounds().Left + offsetX, -path.GetBounds().Top + offsetY);
            path.Transform(translateMatrix);
        }

        public override string ToString()
        {
            return $"{verticalAligment} {size} {text}";
        }
    }
}