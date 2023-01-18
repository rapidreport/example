using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void BtnExampleImage_Click(object sender, EventArgs e)
        {
            ExampleImage.Run();
        }

        private void BtnExampleExtention_Click(object sender, EventArgs e)
        {
            ExampleExtention.Run();
        }

        private void BtnExampleCustomPreview_Click(object sender, EventArgs e)
        {
            ExampleCustomPreview.Run();
        }
    }
}
