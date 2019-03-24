using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinControls
{
    
    public class ImageAndTextCenter:UserControl
    {
        Image icon;

        Size iconSize;

        int imageTextInterval = 5;

        public ImageAndTextCenter()
            :base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            this.Size = new Size(20,20);
            //this.Text = "ImageAndTextCenter";
        }

        public Image Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                Invalidate();
            }
        }

        public Size IconSize
        {
            get { return iconSize; }
            set
            {
                iconSize = value;
                Invalidate();
            }
        }

        public int ImageTextInterval
        {
            get { return imageTextInterval; }
            set
            {
                imageTextInterval = value;
                Invalidate();
            }
        }
        [Browsable(true)]
        public new string Text
        {
            get => base.Text;
            set { base.Text = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

#if true
            Size textSize = TextRenderer.MeasureText(Text,this.Font);

            int centerWidth = textSize.Width + imageTextInterval + (icon == null ? 0 : iconSize.Width);
            int centerHight = Math.Max(textSize.Height, iconSize.Height);

            //居中
            Rectangle centerRect = new Rectangle((Width - centerWidth) / 2,(Height - centerHight)/2,centerWidth,centerHight) ;

            DrawImageAndText(e.Graphics,centerRect);
#endif
        }

        void DrawImageAndText(Graphics g,Rectangle showRect)
        {
            int startX = 0 ,startY = 0;
            Size textSize = TextRenderer.MeasureText(Text, this.Font);
            startX += showRect.X;
            
            if (icon != null)
            {
                startY = showRect.Y + (showRect.Height - iconSize.Height) / 2;
                g.DrawImage(icon,startX,startY,iconSize.Width,icon.Height);
                startX += IconSize.Width + imageTextInterval;
            }

            startY = showRect.Y + (showRect.Height - textSize.Height) / 2;
            g.DrawString(Text,this.Font,new SolidBrush(ForeColor),startX,startY);
        }
    }
}
