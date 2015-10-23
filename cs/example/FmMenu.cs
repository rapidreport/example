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

        private void BtnExample1Csv_Click(object sender, EventArgs e)
        {
            Example1Csv.Run();
        }

        private void BtnExample2_Click(object sender, EventArgs e)
        {
            Example2.Run();
        }

        private void BtnExample2Huge_Click(object sender, EventArgs e)
        {
            Example2Huge.Run();
        }

        private void BtnExampleDataProvider_Click(object sender, EventArgs e)
        {
            ExampleDataProvider.Run();
        }

        private void BtnExampleLocate_Click(object sender, EventArgs e)
        {
            ExampleLocate.Run();
        }

        private void BtnExampleRegion_Click(object sender, EventArgs e)
        {
            ExampleRegion.Run();
        }

        private void BtnExamplePage_Click(object sender, EventArgs e)
        {
            ExamplePage.Run();
        }

        private void BtnExampleRender_Click(object sender, EventArgs e)
        {
            ExampleRender.Run();
        }

        private void BtnExampleCustomize_Click(object sender, EventArgs e)
        {
            ExampleCustomize.Run();
        }

        private void BtnExampleSubPage_Click(object sender, EventArgs e)
        {
            ExampleSubPage.Run();
        }

        private void BtmExampleImage_Click(object sender, EventArgs e)
        {
            ExampleImage.Run();
        }

        private void BtnExtention_Click(object sender, EventArgs e)
        {
            ExampleExtention.Run();
        }

        private void BtnCustomPreview_Click(object sender, EventArgs e)
        {
            ExampleCustomPreview.Run();
        }

        private void BtnMergeContent_Click(object sender, EventArgs e)
        {
            ExampleMergeContent.Run();
        }

        private void BtnFeature_Click(object sender, EventArgs e)
        {
            Feature.Run();
        }

        private void BtnOpenOutput_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("output\\");
        }

    }
}
