using Filling.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filling
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Photo_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Resources.spiderman, new Point(0,0));
        }


    }
}
