namespace example
{
    partial class MyFmPrintPreview
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyFmPrintPreview));
            this.BtnPDF = new System.Windows.Forms.Button();
            this.PrintPreviewSearchPanel = new jp.co.systembase.report.renderer.gdi.PrintPreviewSearchPanel();
            this.PrintPreviewSearch = new jp.co.systembase.report.renderer.gdi.PrintPreviewSearch();
            this.PrintPreviewZoom = new jp.co.systembase.report.renderer.gdi.PrintPreviewZoom();
            this.PrintPreviewPage = new jp.co.systembase.report.renderer.gdi.PrintPreviewPage();
            this.PrintPreviewMultiPage = new jp.co.systembase.report.renderer.gdi.PrintPreviewMultiPage();
            this.PrintPreview = new jp.co.systembase.report.renderer.gdi.PrintPreview();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnPDF
            // 
            this.BtnPDF.Location = new System.Drawing.Point(81, 7);
            this.BtnPDF.Name = "BtnPDF";
            this.BtnPDF.Size = new System.Drawing.Size(70, 32);
            this.BtnPDF.TabIndex = 1;
            this.BtnPDF.Text = "PDF出力";
            this.BtnPDF.UseVisualStyleBackColor = true;
            this.BtnPDF.Click += new System.EventHandler(this.BtnPDF_Click);
            // 
            // PrintPreviewSearchPanel
            // 
            this.PrintPreviewSearchPanel.Location = new System.Drawing.Point(617, 45);
            this.PrintPreviewSearchPanel.Name = "PrintPreviewSearchPanel";
            this.PrintPreviewSearchPanel.Size = new System.Drawing.Size(180, 20);
            this.PrintPreviewSearchPanel.TabIndex = 6;
            // 
            // PrintPreviewSearch
            // 
            this.PrintPreviewSearch.Location = new System.Drawing.Point(697, 7);
            this.PrintPreviewSearch.Name = "PrintPreviewSearch";
            this.PrintPreviewSearch.Size = new System.Drawing.Size(34, 32);
            this.PrintPreviewSearch.TabIndex = 5;
            // 
            // PrintPreviewZoom
            // 
            this.PrintPreviewZoom.Location = new System.Drawing.Point(461, 7);
            this.PrintPreviewZoom.Name = "PrintPreviewZoom";
            this.PrintPreviewZoom.PrintPreview = null;
            this.PrintPreviewZoom.Size = new System.Drawing.Size(230, 32);
            this.PrintPreviewZoom.TabIndex = 4;
            // 
            // PrintPreviewPage
            // 
            this.PrintPreviewPage.Location = new System.Drawing.Point(152, 7);
            this.PrintPreviewPage.Name = "PrintPreviewPage";
            this.PrintPreviewPage.PrintPreview = null;
            this.PrintPreviewPage.Size = new System.Drawing.Size(263, 32);
            this.PrintPreviewPage.TabIndex = 2;
            // 
            // PrintPreviewMultiPage
            // 
            this.PrintPreviewMultiPage.Location = new System.Drawing.Point(421, 7);
            this.PrintPreviewMultiPage.Name = "PrintPreviewMultiPage";
            this.PrintPreviewMultiPage.PrintPreview = null;
            this.PrintPreviewMultiPage.Size = new System.Drawing.Size(34, 32);
            this.PrintPreviewMultiPage.TabIndex = 3;
            // 
            // PrintPreview
            // 
            this.PrintPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintPreview.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.PrintPreview.Location = new System.Drawing.Point(10, 45);
            this.PrintPreview.Name = "PrintPreview";
            this.PrintPreview.Size = new System.Drawing.Size(805, 510);
            this.PrintPreview.TabIndex = 8;
            this.PrintPreview.TabStop = false;
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(737, 7);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(60, 32);
            this.BtnClose.TabIndex = 7;
            this.BtnClose.Text = "閉じる";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.Location = new System.Drawing.Point(10, 7);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(60, 32);
            this.BtnPrint.TabIndex = 0;
            this.BtnPrint.Text = "印刷...";
            this.BtnPrint.UseVisualStyleBackColor = true;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // MyFmPrintPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 562);
            this.Controls.Add(this.PrintPreviewMultiPage);
            this.Controls.Add(this.BtnPDF);
            this.Controls.Add(this.PrintPreviewSearchPanel);
            this.Controls.Add(this.PrintPreviewSearch);
            this.Controls.Add(this.PrintPreviewZoom);
            this.Controls.Add(this.PrintPreviewPage);
            this.Controls.Add(this.PrintPreview);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnPrint);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(840, 200);
            this.Name = "MyFmPrintPreview";
            this.Text = "プレビュー";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MyFmPrintPreview_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MyFmPrintPreview_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button BtnPDF;
        internal jp.co.systembase.report.renderer.gdi.PrintPreviewSearchPanel PrintPreviewSearchPanel;
        internal jp.co.systembase.report.renderer.gdi.PrintPreviewSearch PrintPreviewSearch;
        public jp.co.systembase.report.renderer.gdi.PrintPreviewZoom PrintPreviewZoom;
        public jp.co.systembase.report.renderer.gdi.PrintPreviewPage PrintPreviewPage;
        public jp.co.systembase.report.renderer.gdi.PrintPreview PrintPreview;
        public System.Windows.Forms.Button BtnClose;
        public System.Windows.Forms.Button BtnPrint;
        public jp.co.systembase .report .renderer .gdi .PrintPreviewMultiPage PrintPreviewMultiPage;
    }
}