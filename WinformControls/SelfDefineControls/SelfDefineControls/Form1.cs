using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XinControls;
using System.Diagnostics;
namespace SelfDefineControls
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            
            this.Controls.Add(CreateImageTextControl());

            TestDisplayRect();
        }

        private Control CreateImageTextControl()
        {
            ImageAndTextCenter textControl = new ImageAndTextCenter();
            textControl.Location = new Point(100, 100);
            textControl.Size = new Size(100, 100);
            textControl.Text = "Center";
            textControl.BackColor = Color.White;
            textControl.ForeColor = Color.Black;
            textControl.Icon = Properties.Resources.EditIcon;
            textControl.IconSize = new Size(30, 30);

            return textControl;
        }

        private void TestDisplayRect()
        {
            Debug.WriteLine("ClientRectangle" + button1.ClientRectangle);
            Debug.WriteLine("DisplayRectangle" + button1.DisplayRectangle);
        }
    }
}
