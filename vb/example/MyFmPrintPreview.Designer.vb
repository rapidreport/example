<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MyFmPrintPreview
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MyFmPrintPreview))
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.PrintPreviewZoom = New jp.co.systembase.report.renderer.gdi.PrintPreviewZoom()
        Me.PrintPreviewPage = New jp.co.systembase.report.renderer.gdi.PrintPreviewPage()
        Me.PrintPreview = New jp.co.systembase.report.renderer.gdi.PrintPreview()
        Me.PrintPreviewSearch = New jp.co.systembase.report.renderer.gdi.PrintPreviewSearch()
        Me.PrintPreviewSearchPanel = New jp.co.systembase.report.renderer.gdi.PrintPreviewSearchPanel()
        Me.BtnPDF = New System.Windows.Forms.Button()
        Me.PrintPreviewMultiPage = New jp.co.systembase.report.renderer.gdi.PrintPreviewMultiPage()
        Me.SuspendLayout()
        '
        'BtnPrint
        '
        Me.BtnPrint.Location = New System.Drawing.Point(10, 5)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(60, 32)
        Me.BtnPrint.TabIndex = 0
        Me.BtnPrint.Text = "印刷..."
        Me.BtnPrint.UseVisualStyleBackColor = True
        '
        'BtnClose
        '
        Me.BtnClose.Location = New System.Drawing.Point(737, 5)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(60, 32)
        Me.BtnClose.TabIndex = 7
        Me.BtnClose.Text = "閉じる"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'PrintPreviewZoom
        '
        Me.PrintPreviewZoom.Location = New System.Drawing.Point(461, 5)
        Me.PrintPreviewZoom.Name = "PrintPreviewZoom"
        Me.PrintPreviewZoom.PrintPreview = Nothing
        Me.PrintPreviewZoom.Size = New System.Drawing.Size(230, 32)
        Me.PrintPreviewZoom.TabIndex = 4
        '
        'PrintPreviewPage
        '
        Me.PrintPreviewPage.Location = New System.Drawing.Point(152, 5)
        Me.PrintPreviewPage.Name = "PrintPreviewPage"
        Me.PrintPreviewPage.PrintPreview = Nothing
        Me.PrintPreviewPage.Size = New System.Drawing.Size(263, 32)
        Me.PrintPreviewPage.TabIndex = 2
        '
        'PrintPreview
        '
        Me.PrintPreview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PrintPreview.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.PrintPreview.Location = New System.Drawing.Point(10, 43)
        Me.PrintPreview.Name = "PrintPreview"
        Me.PrintPreview.Size = New System.Drawing.Size(805, 510)
        Me.PrintPreview.TabIndex = 8
        Me.PrintPreview.TabStop = False
        '
        'PrintPreviewSearch
        '
        Me.PrintPreviewSearch.Location = New System.Drawing.Point(697, 5)
        Me.PrintPreviewSearch.Name = "PrintPreviewSearch"
        Me.PrintPreviewSearch.Size = New System.Drawing.Size(34, 32)
        Me.PrintPreviewSearch.TabIndex = 5
        '
        'PrintPreviewSearchPanel
        '
        Me.PrintPreviewSearchPanel.Location = New System.Drawing.Point(617, 43)
        Me.PrintPreviewSearchPanel.Name = "PrintPreviewSearchPanel"
        Me.PrintPreviewSearchPanel.Size = New System.Drawing.Size(180, 20)
        Me.PrintPreviewSearchPanel.TabIndex = 6
        '
        'BtnPDF
        '
        Me.BtnPDF.Location = New System.Drawing.Point(81, 5)
        Me.BtnPDF.Name = "BtnPDF"
        Me.BtnPDF.Size = New System.Drawing.Size(70, 32)
        Me.BtnPDF.TabIndex = 1
        Me.BtnPDF.Text = "PDF出力"
        Me.BtnPDF.UseVisualStyleBackColor = True
        '
        'PrintPreviewMultiPage
        '
        Me.PrintPreviewMultiPage.Location = New System.Drawing.Point(421, 5)
        Me.PrintPreviewMultiPage.Name = "PrintPreviewMultiPage"
        Me.PrintPreviewMultiPage.PrintPreview = Nothing
        Me.PrintPreviewMultiPage.Size = New System.Drawing.Size(34, 32)
        Me.PrintPreviewMultiPage.TabIndex = 3
        '
        'MyFmPrintPreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(824, 562)
        Me.Controls.Add(Me.PrintPreviewMultiPage)
        Me.Controls.Add(Me.BtnPDF)
        Me.Controls.Add(Me.PrintPreviewSearchPanel)
        Me.Controls.Add(Me.PrintPreviewSearch)
        Me.Controls.Add(Me.PrintPreviewZoom)
        Me.Controls.Add(Me.PrintPreviewPage)
        Me.Controls.Add(Me.PrintPreview)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.BtnPrint)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(840, 200)
        Me.Name = "MyFmPrintPreview"
        Me.Text = "プレビュー"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents BtnPrint As System.Windows.Forms.Button
    Public WithEvents BtnClose As System.Windows.Forms.Button
    Public WithEvents PrintPreview As jp.co.systembase.report.renderer.gdi.PrintPreview
    Public WithEvents PrintPreviewPage As jp.co.systembase.report.renderer.gdi.PrintPreviewPage
    Public WithEvents PrintPreviewZoom As jp.co.systembase.report.renderer.gdi.PrintPreviewZoom
    Friend WithEvents PrintPreviewSearch As jp.co.systembase.report.renderer.gdi.PrintPreviewSearch
    Friend WithEvents PrintPreviewSearchPanel As jp.co.systembase.report.renderer.gdi.PrintPreviewSearchPanel
    Friend WithEvents BtnPDF As System.Windows.Forms.Button
    Friend WithEvents PrintPreviewMultiPage As jp.co.systembase.report.renderer.gdi.PrintPreviewMultiPage
End Class
