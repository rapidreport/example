using System;
using System.IO;
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
            InitializeComponent();
        }
        public MyFmPrintPreview(Printer printer)
        {
            InitializeComponent();
            PrintPreview.Printer = printer;
        }

        // フォームロード
        private void MyFmPrintPreview_Load(object sender, EventArgs e)
        {
            using (PrintPreview.RenderBlock())
            {
                PrintPreviewPage.Init(PrintPreview);
                PrintPreviewMultiPage.Init(PrintPreview);
                PrintPreviewZoom.Init(PrintPreview);
                PrintPreviewSearch.Init(PrintPreview, PrintPreviewSearchPanel);
                // 「画面サイズに合わせて拡大/縮小」状態にします
                PrintPreview.AutoZoomFit = true;
            }
            MouseWheel += new MouseEventHandler(MyFmPrintPreview_MouseWheel);
        }

        // マウスホイール操作
        private void MyFmPrintPreview_MouseWheel(object sender, MouseEventArgs e)
        {
            Boolean handled = false;
            if (System.Object.ReferenceEquals(ActiveControl, PrintPreviewPage))
            {
                handled = PrintPreviewPage.HandleMouseWheelEvent(e);
            }
            else if (System.Object.ReferenceEquals(ActiveControl, PrintPreviewZoom))
            {
                handled = PrintPreviewZoom.HandleMouseWheelEvent(e);
            }
            if (!handled)
            {
                PrintPreview.HandleMouseWheelEvent(e);
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
                        Print();
                    }
                    break;
                case Keys.Escape:
                    if (PrintPreviewSearchPanel.Visible)
                    {
                        PrintPreviewSearch.PanelHide();
                    }
                    else
                    {
                        Close();
                    }
                    break;
                default:
                    PrintPreview.HandleKeyDownEvent(e);
                    break;
            }
        }

        // 印刷ボタン押下
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        // PDF出力ボタン押下
        private void BtnPDF_Click(object sender, EventArgs e)
        {
            ExportPDF();
        }

        // 閉じるボタン押下
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // 印刷実行
        public void Print()
        {
            if (PrintPreview.Printer.PrintDialog.ShowDialog() == DialogResult.OK)
            {
                PrintPreview.Printer.PrintDocument.Print();
                PrintExecuted = true;
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
                    using (FileStream fs = new FileStream(fd.FileName, FileMode.Create))
                    {
                        PdfRenderer renderer = new PdfRenderer(fs);
                        renderer.Setting.ReplaceBackslashToYen = true;
                        PrintPreview.Printer.Pages.Render(renderer);
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
