using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StringExtention.DerivedString
{
    /* create this type to extend String for drawing */
    class DrawString
    {
        string content;

        public DrawString(Font font)
        {
            Font = font;
        }

        public DrawString(string content,Font font)
        {
            Content = content;
            Font = font;
        }

        public string Content {
            get { return content; }
            set {
                content = value;
                ContentSize = TextRenderer.MeasureText(content,Font);
            }
        }

        public Font Font { get; set; }

        public Size ContentSize { get; private set; }

        public override string ToString()
        {
            return content;
        }
    }
}
