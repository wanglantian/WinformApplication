using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridViewApplication
{
    public partial class Form1 : Form
    {
        DataGridViewShow showContent;

        public Form1()
        {
            InitializeComponent();
            this.Text = "DataGridViewApp";
            showContent = new DataGridViewShow(this);
        }

        

    }
}
