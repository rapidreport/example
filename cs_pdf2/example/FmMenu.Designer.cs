namespace example
{
    partial class FmMenu
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
            this.BtnOpenOutput = new System.Windows.Forms.Button();
            this.BtnExample1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnOpenOutput
            // 
            this.BtnOpenOutput.Location = new System.Drawing.Point(12, 81);
            this.BtnOpenOutput.Name = "BtnOpenOutput";
            this.BtnOpenOutput.Size = new System.Drawing.Size(170, 23);
            this.BtnOpenOutput.TabIndex = 16;
            this.BtnOpenOutput.Text = "PDF,XLS出力フォルダを開く";
            this.BtnOpenOutput.UseVisualStyleBackColor = true;
            this.BtnOpenOutput.Click += new System.EventHandler(this.BtnOpenOutput_Click);
            // 
            // BtnExample1
            // 
            this.BtnExample1.Location = new System.Drawing.Point(12, 12);
            this.BtnExample1.Name = "BtnExample1";
            this.BtnExample1.Size = new System.Drawing.Size(170, 23);
            this.BtnExample1.TabIndex = 0;
            this.BtnExample1.Text = "見積書";
            this.BtnExample1.UseVisualStyleBackColor = true;
            this.BtnExample1.Click += new System.EventHandler(this.BtnExample1_Click);
            // 
            // FmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 116);
            this.Controls.Add(this.BtnOpenOutput);
            this.Controls.Add(this.BtnExample1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FmMenu";
            this.Text = "メニュー";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button BtnOpenOutput;
        internal System.Windows.Forms.Button BtnExample1;
    }
}

