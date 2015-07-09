Imports System.IO

Imports jp.co.systembase.report.renderer.gdi
Imports jp.co.systembase.report.renderer.pdf

' カスタマイズされたプレビュー画面
Public Class MyFmPrintPreview

    ' この変数によって、実際に印刷が行われたかを確認できます
    Public PrintExecuted As Boolean = False

    ' コンストラクタ
    Public Sub New()
        Me.InitializeComponent()
    End Sub
    Public Sub New(ByVal printer As Printer)
        Me.InitializeComponent()
        Me.PrintPreview.Printer = printer
    End Sub

    ' フォームロード
    Private Sub FmPrintPreview_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Using Me.PrintPreview.RenderBlock
            Me.PrintPreviewPage.Init(Me.PrintPreview)
            Me.PrintPreviewZoom.Init(Me.PrintPreview)
            Me.PrintPreviewSearch.Init(Me.PrintPreview, Me.PrintPreviewSearchPanel)
            ' 「画面サイズに合わせて拡大/縮小」状態にします
            Me.PrintPreview.ZoomFit()
        End Using
    End Sub

    ' マウスホイール操作
    Private Sub FmPrintPreview_MouseWheel(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseWheel
        Dim handled As Boolean = False
        If Me.ActiveControl Is Me.PrintPreviewPage Then
            handled = Me.PrintPreviewPage.HandleMouseWheelEvent(e)
        ElseIf Me.ActiveControl Is Me.PrintPreviewZoom Then
            handled = Me.PrintPreviewZoom.HandleMouseWheelEvent(e)
        End If
        If Not handled Then
            Me.PrintPreview.HandleMouseWheelEvent(e)
        End If
    End Sub

    ' キー押下
    Private Sub FmPrintPreview_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.P
                If e.Modifiers = Keys.Control Then
                    Me.Print()
                End If
            Case Keys.Escape
                If Me.PrintPreviewSearchPanel.Visible Then
                    Me.PrintPreviewSearch.PanelHide()
                Else
                    Me.Close()
                End If
            Case Else
                Me.PrintPreview.HandleKeyDownEvent(e)
        End Select
    End Sub

    ' 印刷ボタン押下
    Private Sub BtnPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnPrint.Click
        Me.Print()
    End Sub

    ' PDF出力ボタン押下
    Private Sub BtnPDF_Click(sender As System.Object, e As System.EventArgs) Handles BtnPDF.Click
        Me.ExportPDF()
    End Sub

    ' 閉じるボタン押下
    Private Sub BtnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    ' 印刷実行
    Public Sub Print()
        If Me.PrintPreview.Printer.PrintDialog.ShowDialog = DialogResult.OK Then
            Me.PrintPreview.Printer.PrintDocument.Print()
            Me.PrintExecuted = True
        End If
    End Sub

    ' PDF出力実行
    Public Sub ExportPDF()
        With New SaveFileDialog()
            .AddExtension = True
            .Filter = "PDFファイル(*.pdf)|*.pdf"
            If .ShowDialog = DialogResult.OK Then
                Try
                    Using fs As New FileStream(.FileName, IO.FileMode.Create)
                        Dim renderer As New PdfRenderer(fs)
                        renderer.Setting.ReplaceBackslashToYen = True
                        Me.PrintPreview.Printer.Pages.Render(renderer)
                    End Using
                    MessageBox.Show(.FileName & "を保存しました", "確認")
                Catch ex As Exception
                    MessageBox.Show(.FileName & "の保存に失敗しました", "確認")
                End Try
            End If
        End With
    End Sub

End Class
