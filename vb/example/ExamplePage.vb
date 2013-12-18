Imports System.IO

Imports NPOI.HSSF.UserModel

Imports jp.co.systembase.json
Imports jp.co.systembase.report
Imports jp.co.systembase.report.component
Imports jp.co.systembase.report.data
Imports jp.co.systembase.report.renderer.gdi
Imports jp.co.systembase.report.renderer.pdf
Imports jp.co.systembase.report.renderer.xls
Imports jp.co.systembase.report.customizer

' 機能サンプル ページ挿入
Module ExamplePage

    Public Sub Run()

        Dim pages As ReportPages
        With Nothing
            ' 第2引数にCustomizerオブジェクトを渡します
            Dim report As New Report(Json.Read("report\example_page1.rrpt"), New Customizer)
            report.Fill(New ReportDataSource(getDataTable))
            pages = report.GetPages()
        End With

        With Nothing
            Dim report As New Report(Json.Read("report\example_page3.rrpt"))
            report.Fill(DummyDataSource.GetInstance)
            ' 最後のページを追加します
            pages.AddRange(report.GetPages)
        End With

        ' PDF出力
        Using fs As New FileStream("output\example_page.pdf", IO.FileMode.Create)
            pages.Render(New PdfRenderer(fs))
        End Using

        ' XLS出力
        Using fs As New FileStream("output\example_page.xls", IO.FileMode.Create)
            Dim workbook As New HSSFWorkbook
            Dim renderer As New XlsRenderer(workbook)
            renderer.NewSheet("example_page")
            pages.Render(renderer)
            workbook.Write(fs)
        End Using

        'プレビュー
        With Nothing
            Dim printer As New Printer(pages)
            ' 用紙サイズの異なるページが含まれているので、
            ' 動的に用紙設定が行われるようにします
            printer.DynamicPageSetting = True
            Dim preview As New FmPrintPreview(printer)
            preview.StartUpZoomFit = True
            preview.ShowDialog()
        End With

    End Sub

    ' グループが終了するたびに集計ページを挿入するカスタマイザ
    Private Class Customizer
        Inherits DefaultCustomizer

        Private reportDesign As ReportDesign

        Public Sub New()
            ' あらかじめ集計ページの帳票定義ファイルを読み込んでおきます
            Me.reportDesign = New ReportDesign(Json.Read("report\example_page2.rrpt"))
        End Sub

        Public Overrides Sub PageAdded( _
          ByVal report As Report, _
          ByVal pages As ReportPages, _
          ByVal page As ReportPage)
            ' このメソッドはページが追加されるたびに呼ばれます
            ' 直前のページで"group_example"という識別子を持ったグループが終了しているかを調べます()
            Dim g As Group = page.FindFinishedGroup("group_example")
            If g IsNot Nothing Then
                ' 直前に終了したグループのデータを用いて集計ページを作成し、挿入します
                Dim _report As New Report(Me.reportDesign)
                _report.Fill(g.Data)
                pages.AddRange(_report.GetPages)
            End If
        End Sub

    End Class

    Private Function getDataTable() As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("GROUP_CD", GetType(String))
        ret.Columns.Add("DATA", GetType(String))
        ret.Rows.Add("A", "A-1")
        ret.Rows.Add("A", "A-2")
        ret.Rows.Add("A", "A-3")
        ret.Rows.Add("A", "A-4")
        ret.Rows.Add("A", "A-5")
        ret.Rows.Add("A", "A-6")
        ret.Rows.Add("A", "A-7")
        ret.Rows.Add("A", "A-8")
        ret.Rows.Add("A", "A-9")
        ret.Rows.Add("A", "A-10")
        ret.Rows.Add("A", "A-11")
        ret.Rows.Add("A", "A-12")
        ret.Rows.Add("A", "A-13")
        ret.Rows.Add("A", "A-14")
        ret.Rows.Add("A", "A-15")
        ret.Rows.Add("A", "A-16")
        ret.Rows.Add("A", "A-17")
        ret.Rows.Add("A", "A-18")
        ret.Rows.Add("A", "A-19")
        ret.Rows.Add("A", "A-20")
        ret.Rows.Add("B", "B-1")
        ret.Rows.Add("B", "B-2")
        ret.Rows.Add("B", "B-3")
        ret.Rows.Add("B", "B-4")
        ret.Rows.Add("B", "B-5")
        ret.Rows.Add("B", "B-6")
        ret.Rows.Add("B", "B-7")
        ret.Rows.Add("B", "B-8")
        ret.Rows.Add("B", "B-9")
        ret.Rows.Add("B", "B-10")
        Return ret
    End Function

End Module