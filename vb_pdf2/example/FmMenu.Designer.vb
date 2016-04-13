<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FmMenu
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
        Me.BtnExample1 = New System.Windows.Forms.Button()
        Me.BtnOpenOutput = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnExample1
        '
        Me.BtnExample1.Location = New System.Drawing.Point(12, 12)
        Me.BtnExample1.Name = "BtnExample1"
        Me.BtnExample1.Size = New System.Drawing.Size(170, 23)
        Me.BtnExample1.TabIndex = 0
        Me.BtnExample1.Text = "見積書"
        Me.BtnExample1.UseVisualStyleBackColor = True
        '
        'BtnOpenOutput
        '
        Me.BtnOpenOutput.Location = New System.Drawing.Point(12, 69)
        Me.BtnOpenOutput.Name = "BtnOpenOutput"
        Me.BtnOpenOutput.Size = New System.Drawing.Size(170, 23)
        Me.BtnOpenOutput.TabIndex = 16
        Me.BtnOpenOutput.Text = "PDF,XLS出力フォルダを開く"
        Me.BtnOpenOutput.UseVisualStyleBackColor = True
        '
        'FmMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(194, 104)
        Me.Controls.Add(Me.BtnOpenOutput)
        Me.Controls.Add(Me.BtnExample1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FmMenu"
        Me.Text = "メニュー"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnExample1 As System.Windows.Forms.Button
    Friend WithEvents BtnOpenOutput As System.Windows.Forms.Button

End Class
