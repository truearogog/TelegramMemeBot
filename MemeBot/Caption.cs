using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MemeBot
{
    class Caption
    {
        public string Text { get { return m_text; } set { m_text = value; } }
        private string m_text = "text";
        public string VerticalAligment { get { return m_verticalAligment; } set { m_verticalAligment = value.ToLower(); } }
        private string m_verticalAligment = "Down";
        public static string[] verticalAligments = { "Up", "Down", "Middle" };
        public float FontSize { get { return m_fontSize; } set { m_fontSize = value; } }
        private float m_fontSize = 1.0f;
        public string FontFamily { get { return m_fontFamily; } set { m_fontFamily = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower()); } }
        private string m_fontFamily = "Impact";
        public static string[] fontFamilies = { "Arial", "Impact", "Calibri", "Cambria", "Times New Roman" };

        public Caption() { }

        public static bool ContainsVerticalAligment(string verticalAligment)
        {
            verticalAligment = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(verticalAligment.ToLower());
            return Array.FindIndex(verticalAligments, aligment => aligment.Equals(verticalAligment)) >= 0;
        }

        public static bool ContainsFontFamily(string fontFamily)
        {
            fontFamily = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fontFamily.ToLower());
            return Array.FindIndex(fontFamilies, family => family.Equals(fontFamily)) >= 0;
        }

        public bool Ready()
        {
            return Text != null && VerticalAligment != null && FontFamily != null && FontSize >= 0;
        }

        public void Draw(Bitmap image, GraphicsPath path)
        {
            float fontSize = image.Width / Text.Length * 2 * FontSize;

            path.AddString(Text, new FontFamily(FontFamily), 0, fontSize, new PointF(0, 0), StringFormat.GenericDefault);

            float offsetX = image.Width / 2 - path.GetBounds().Width / 2;
            float offsetY;
            switch (VerticalAligment)
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
            return $"{VerticalAligment} {FontSize} {FontFamily} {Text}";
        }
    }
}