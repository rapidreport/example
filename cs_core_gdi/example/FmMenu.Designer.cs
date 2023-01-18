namespace example
{
    partial class FmMenu
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
            this.BtnExample1 = new System.Windows.Forms.Button();
            this.BtnExampleImage = new System.Windows.Forms.Button();
            this.BtnExampleExtention = new System.Windows.Forms.Button();
            this.BtnExampleCustomPreview = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnExample1
            // 
            this.BtnExample1.Location = new System.Drawing.Point(12, 12);
            this.BtnExample1.Name = "BtnExample1";
            this.BtnExample1.Size = new System.Drawing.Size(180, 23);
            this.BtnExample1.TabIndex = 0;
            this.BtnExample1.Text = "チュートリアル1 見積書";
            this.BtnExample1.UseVisualStyleBackColor = true;
            this.BtnExample1.Click += new System.EventHandler(this.BtnExample1_Click);
            // 
            // BtnExampleImage
            // 
            this.BtnExampleImage.Location = new System.Drawing.Point(12, 41);
            this.BtnExampleImage.Name = "BtnExampleImage";
            this.BtnExampleImage.Size = new System.Drawing.Size(180, 23);
            this.BtnExampleImage.TabIndex = 1;
            this.BtnExampleImage.Text = "動的画像の表示";
            this.BtnExampleImage.UseVisualStyleBackColor = true;
            this.BtnExampleImage.Click += new System.EventHandler(this.BtnExampleImage_Click);
            // 
            // BtnExampleExtention
            // 
            this.BtnExampleExtention.Location = new System.Drawing.Point(13, 71);
            this.BtnExampleExtention.Name = "BtnExampleExtention";
            this.BtnExampleExtention.Size = new System.Drawing.Size(180, 23);
            this.BtnExampleExtention.TabIndex = 2;
            this.BtnExampleExtention.Text = "カスタム書式/要素";
            this.BtnExampleExtention.UseVisualStyleBackColor = true;
            this.BtnExampleExtention.Click += new System.EventHandler(this.BtnExampleExtention_Click);
            // 
            // BtnExampleCustomPreview
            // 
            this.BtnExampleCustomPreview.Location = new System.Drawing.Point(12, 99);
            this.BtnExampleCustomPreview.Name = "BtnExampleCustomPreview";
            this.BtnExampleCustomPreview.Size = new System.Drawing.Size(180, 23);
            this.BtnExampleCustomPreview.TabIndex = 3;
            this.BtnExampleCustomPreview.Text = "プレビュー画面のカスタマイズ";
            this.BtnExampleCustomPreview.UseVisualStyleBackColor = true;
            this.BtnExampleCustomPreview.Click += new System.EventHandler(this.BtnExampleCustomPreview_Click);
            // 
            // FmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 141);
            this.Controls.Add(this.BtnExampleCustomPreview);
            this.Controls.Add(this.BtnExampleExtention);
            this.Controls.Add(this.BtnExampleImage);
            this.Controls.Add(this.BtnExample1);
            this.Name = "FmMenu";
            this.Text = "メニュー";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnExample1;
        private System.Windows.Forms.Button BtnExampleImage;
        private System.Windows.Forms.Button BtnExampleExtention;
        private System.Windows.Forms.Button BtnExampleCustomPreview;
    }
}