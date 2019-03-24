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
    public class CanWithBorderLabel:UserControl
    {
        Color borderColor;

        int borderWidth;

        public CanWithBorderLabel()
            :base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            this.Size = new Size(20, 20);
        }

        [Browsable(true)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        public int BorderWidth
        {
            get { return borderWidth; }
            set { borderWidth = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            base.OnPaintBackground(e);

            DrawString(e.Graphics);

            DrawBorder(e.Graphics);
        }

        private void DrawString(Graphics g)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                int startX = Padding.Left;
                int startY = Padding.Top;
                int stringRectWidth = Width - Padding.Left - Padding.Right;
                int stringRectHeight = Height - Padding.Top - Padding.Bottom;

                if (borderWidth > 0 && borderColor != null)
                {
                    startX = Math.Max(borderWidth,startX);
                    startY = Math.Max(borderWidth,startY);
                    stringRectWidth = Width - Math.Max(borderWidth * 2, Padding.Left + Padding.Right);
                    stringRectHeight = Height - Math.Max(borderWidth*2,Padding.Top + Padding.Bottom);
                }

                Rectangle stringRect = new Rectangle(startX, startY, stringRectWidth, stringRectHeight);
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                g.DrawString(Text, Font, new SolidBrush(ForeColor), stringRect, sf);
            }
        }

        private void DrawBorder(Graphics g)
        {
            if (borderWidth > 0 && borderColor != null)
            {
                g.DrawRectangle(new Pen(borderColor,borderWidth),borderWidth - 1,borderWidth - 1,Width - 2*(borderWidth) + 1,Height - 2*(borderWidth)+ 1);
            }
        }
    }
}
