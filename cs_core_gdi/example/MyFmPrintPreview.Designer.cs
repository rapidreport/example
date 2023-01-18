namespace example
{
    partial class MyFmPrintPreview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PrintPreviewMultiPage = new jp.co.systembase.report.renderer.gdi.PrintPreviewMultiPage();
            this.PrintPreviewSearch = new jp.co.systembase.report.renderer.gdi.PrintPreviewSearch();
            this.PrintPreviewZoom = new jp.co.systembase.report.renderer.gdi.PrintPreviewZoom();
            this.PrintPreviewPage = new jp.co.systembase.report.renderer.gdi.PrintPreviewPage();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.PrintPreview = new jp.co.systembase.report.renderer.gdi.PrintPreview();
            this.PrintPreviewSearchPanel = new jp.co.systembase.report.renderer.gdi.PrintPreviewSearchPanel();
            this.BtnPDF = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PrintPreviewMultiPage
            // 
            this.PrintPreviewMultiPage.Location = new System.Drawing.Point(494, 6);
            this.PrintPreviewMultiPage.Margin = new System.Windows.Forms.Padding(5);
            this.PrintPreviewMultiPage.Name = "PrintPreviewMultiPage";
            this.PrintPreviewMultiPage.PrintPreview = null;
            this.PrintPreviewMultiPage.Size = new System.Drawing.Size(40, 40);
            this.PrintPreviewMultiPage.TabIndex = 3;
            // 
            // PrintPreviewSearch
            // 
            this.PrintPreviewSearch.Location = new System.Drawing.Point(815, 6);
            this.PrintPreviewSearch.Margin = new System.Windows.Forms.Padding(5);
            this.PrintPreviewSearch.Name = "PrintPreviewSearch";
            this.PrintPreviewSearch.Size = new System.Drawing.Size(40, 40);
            this.PrintPreviewSearch.TabIndex = 5;
            // 
            // PrintPreviewZoom
            // 
            this.PrintPreviewZoom.Location = new System.Drawing.Point(544, 6);
            this.PrintPreviewZoom.Margin = new System.Windows.Forms.Padding(5);
            this.PrintPreviewZoom.Name = "PrintPreviewZoom";
            this.PrintPreviewZoom.PrintPreview = null;
            this.PrintPreviewZoom.Size = new System.Drawing.Size(270, 40);
            this.PrintPreviewZoom.TabIndex = 4;
            // 
            // PrintPreviewPage
            // 
            this.PrintPreviewPage.Location = new System.Drawing.Point(182, 6);
            this.PrintPreviewPage.Margin = new System.Windows.Forms.Padding(5);
            this.PrintPreviewPage.Name = "PrintPreviewPage";
            this.PrintPreviewPage.PrintPreview = null;
            this.PrintPreviewPage.Size = new System.Drawing.Size(307, 40);
            this.PrintPreviewPage.TabIndex = 2;
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(864, 6);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(4);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(70, 40);
            this.BtnClose.TabIndex = 7;
            this.BtnClose.Text = "閉じる";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Location = new System.Drawing.Point(12, 6);
            this.BtnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(70, 40);
            this.BtnPrint.TabIndex = 0;
            this.BtnPrint.Text = "印刷...";
            this.BtnPrint.UseVisualStyleBackColor = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // PrintPreview
            // 
            this.PrintPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintPreview.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.PrintPreview.Location = new System.Drawing.Point(13, 54);
            this.PrintPreview.Margin = new System.Windows.Forms.Padding(4);
            this.PrintPreview.Name = "PrintPreview";
            this.PrintPreview.Size = new System.Drawing.Size(921, 657);
            this.PrintPreview.TabIndex = 8;
            this.PrintPreview.TabStop = false;
            // 
            // PrintPreviewSearchPanel
            // 
            this.PrintPreviewSearchPanel.Location = new System.Drawing.Point(724, 54);
            this.PrintPreviewSearchPanel.Margin = new System.Windows.Forms.Padding(5);
            this.PrintPreviewSearchPanel.Name = "PrintPreviewSearchPanel";
            this.PrintPreviewSearchPanel.Size = new System.Drawing.Size(210, 25);
            this.PrintPreviewSearchPanel.TabIndex = 6;
            this.PrintPreviewSearchPanel.Visible = false;
            // 
            // BtnPDF
            // 
            this.BtnPDF.Location = new System.Drawing.Point(90, 6);
            this.BtnPDF.Margin = new System.Windows.Forms.Padding(4);
            this.BtnPDF.Name = "BtnPDF";
            this.BtnPDF.Size = new System.Drawing.Size(83, 40);
            this.BtnPDF.TabIndex = 1;
            this.BtnPDF.Text = "PDF出力";
            this.BtnPDF.UseVisualStyleBackColor = true;
            this.BtnPDF.Click += new System.EventHandler(this.BtnPDF_Click);
            // 
            // MyFmPrintPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 721);
            this.Controls.Add(this.BtnPDF);
            this.Controls.Add(this.PrintPreviewSearchPanel);
            this.Controls.Add(this.PrintPreview);
            this.Controls.Add(this.PrintPreviewMultiPage);
            this.Controls.Add(this.PrintPreviewSearch);
            this.Controls.Add(this.PrintPreviewZoom);
            this.Controls.Add(this.PrintPreviewPage);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnPrint);
            this.Name = "MyFmPrintPreview";
            this.Text = "プレビュー";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MyFmPrintPreview_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MyFmPrintPreview_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        internal jp.co.systembase.report.renderer.gdi.PrintPreviewMultiPage PrintPreviewMultiPage;
        internal jp.co.systembase.report.renderer.gdi.PrintPreviewSearch PrintPreviewSearch;
        public jp.co.systembase.report.renderer.gdi.PrintPreviewZoom PrintPreviewZoom;
        public jp.co.systembase.report.renderer.gdi.PrintPreviewPage PrintPreviewPage;
        public System.Windows.Forms.Button BtnClose;
        public System.Windows.Forms.Button BtnPrint;
        public jp.co.systembase.report.renderer.gdi.PrintPreview PrintPreview;
        internal jp.co.systembase.report.renderer.gdi.PrintPreviewSearchPanel PrintPreviewSearchPanel;
        public System.Windows.Forms.Button BtnPDF;
    }
}