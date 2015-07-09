using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.pdf;

namespace example
{
    public partial class MyFmPrintPreview : Form
    {
        // この変数によって、実際に印刷が行われたかを確認できます
        public Boolean PrintExecuted = false;

        // コンストラクタ
        public MyFmPrintPreview()
        {
            this.InitializeComponent();
        }
        public MyFmPrintPreview(Printer printer)
        {
            this.InitializeComponent();
            this.PrintPreview.Printer = printer;
        }

        // フォームロード
        private void MyFmPrintPreview_Load(object sender, EventArgs e)
        {
            using (this.PrintPreview.RenderBlock())
            {
                this.PrintPreviewPage.Init(this.PrintPreview);
                this.PrintPreviewZoom.Init(this.PrintPreview);
                this.PrintPreviewSearch.Init(this.PrintPreview, this.PrintPreviewSearchPanel);
                // 「画面サイズに合わせて拡大/縮小」状態にします
                this.PrintPreview.ZoomFit();
            }
            this.MouseWheel += new MouseEventHandler(this.MyFmPrintPreview_MouseWheel);
        }

        // マウスホイール操作
        private void MyFmPrintPreview_MouseWheel(object sender, MouseEventArgs e)
        {
            Boolean handled = false;
            if (System.Object.ReferenceEquals(this.ActiveControl, this.PrintPreviewPage)){
                handled = this.PrintPreviewPage.HandleMouseWheelEvent(e);
            }else if (System.Object.ReferenceEquals(this.ActiveControl , this.PrintPreviewZoom)){
                handled = this.PrintPreviewZoom.HandleMouseWheelEvent(e);
            }
            if (!handled)
            {
                this.PrintPreview.HandleMouseWheelEvent(e);
            }
        }

        // キー押下
        private void MyFmPrintPreview_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.P:
                    if (e.Modifiers == Keys.Control)
                    {
                        this.Print();
                    }
                    break;
                case Keys.Escape:
                    if (this.PrintPreviewSearchPanel.Visible)
                    {
                        this.PrintPreviewSearch.PanelHide();
                    }
                    else
                    {
                        this.Close();
                    }
                    break;
                default:
                    this.PrintPreview.HandleKeyDownEvent(e);
                    break;
            }
        }

        // 印刷ボタン押下
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            this.Print();
        }
        
        // PDF出力ボタン押下
        private void BtnPDF_Click(object sender, EventArgs e)
        {
            this.ExportPDF();
        }

        // 閉じるボタン押下
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 印刷実行
        public void Print()
        {
            if (this.PrintPreview.Printer.PrintDialog.ShowDialog() == DialogResult.OK)
            {
                this.PrintPreview.Printer.PrintDocument.Print();
                this.PrintExecuted = true;
            }
        }

        // PDF出力実行
        public void ExportPDF()
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.AddExtension = true;
            fd.Filter = "PDFファイル(*.pdf)|*.pdf";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using(FileStream fs = new FileStream(fd.FileName, FileMode.Create))
                    {
                        PdfRenderer renderer = new PdfRenderer(fs);
                        renderer.Setting.ReplaceBackslashToYen = true;
                        this.PrintPreview.Printer.Pages.Render(renderer);
                    }
                    MessageBox.Show(fd.FileName + "を保存しました", "確認");
                }
                catch (Exception)
                {
                    MessageBox.Show(fd.FileName + "の保存に失敗しました", "確認");
                }
            }
        }

    }
}
