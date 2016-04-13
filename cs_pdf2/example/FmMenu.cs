using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace example
{
    public partial class FmMenu : Form
    {
        public FmMenu()
        {
            InitializeComponent();
        }

        private void BtnExample1_Click(object sender, EventArgs e)
        {
            Example1.Run();
        }

        private void BtnOpenOutput_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("output\\");
        }

    }
}
