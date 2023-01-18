Imports System.IO

Imports jp.co.systembase.NPOI.HSSF.UserModel
Imports jp.co.systembase.NPOI.XSSF.UserModel

Imports jp.co.systembase.json
Imports jp.co.systembase.report
Imports jp.co.systembase.report.component
Imports jp.co.systembase.report.data
Imports jp.co.systembase.report.renderer.gdi
Imports jp.co.systembase.report.renderer.pdf
Imports jp.co.systembase.report.renderer.xls
Imports jp.co.systembase.report.renderer.xlsx
Imports jp.co.systembase.report.customizer

' 機能サンプル 動的要素(デザイナのみ)
Module ExampleCustomize

    Public Sub Run()

        ' カスタマイザを指定せずに、Reportオブジェクトを生成します
        Dim report As New Report(Json.Read("report/example_customize.rrpt"))
        report.Fill(New ReportDataSource(getDataTable))
        Dim pages As ReportPages = report.GetPages()

        ' PDF出力
        Using fs As New FileStream("output/example_customize.pdf", IO.FileMode.Create)
            pages.Render(New PdfRenderer(fs))
        End Using

        ' XLS出力
        Using fs As New FileStream("output/example_customize.xls", IO.FileMode.Create)
            Dim workbook As New HSSFWorkbook
            Dim renderer As New XlsRenderer(workbook)
            renderer.NewSheet("example_customize")
            pages.Render(renderer)
            workbook.Write(fs)
        End Using

        ' XLSX出力
        Using fs As New FileStream("output/example_customize.xlsx", IO.FileMode.Create)
            Dim workbook As New XSSFWorkbook
            Dim renderer As New XlsxRenderer(workbook)
            renderer.NewSheet("example_customize")
            pages.Render(renderer)
            workbook.Write(fs)
        End Using

        ' プレビュー
        With Nothing
            Dim preview As New FmPrintPreview(New Printer(pages))
            preview.StartUpZoomFit = True
            preview.ShowDialog()
        End With

    End Sub

    Private Function getDataTable() As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("NUM", GetType(Decimal))
        ret.Rows.Add(50)
        ret.Rows.Add(40)
        ret.Rows.Add(30)
        ret.Rows.Add(20)
        ret.Rows.Add(10)
        ret.Rows.Add(0)
        ret.Rows.Add(-10)
        ret.Rows.Add(-20)
        ret.Rows.Add(-30)
        ret.Rows.Add(-40)
        ret.Rows.Add(-50)
        Return ret
    End Function

End Module
