Imports System.IO

Imports jp.co.systembase.NPOI.HSSF.UserModel
Imports jp.co.systembase.NPOI.XSSF.UserModel

Imports jp.co.systembase.json
Imports jp.co.systembase.report
Imports jp.co.systembase.report.data
Imports jp.co.systembase.report.renderer.gdi
Imports jp.co.systembase.report.renderer.pdf
Imports jp.co.systembase.report.renderer.xls
Imports jp.co.systembase.report.renderer.xlsx

' 機能サンプル サブページ
Module ExampleSubPage

    Public Sub Run()

        ' サブページを先に生成します
        Dim subReport As New Report(Json.Read("report\example_subpage2.rrpt"))
        subReport.Fill(New ReportDataSource(getDataTable))
        Dim subPages As ReportPages = subReport.GetPages()

        Dim report As New Report(Json.Read("report\example_subpage1.rrpt"))
        ' 外枠帳票にサブページを登録します
        report.AddSubPages("subpage", subPages)
        ' 外枠帳票の中でサブページが正しく割り当てられるようにSubPageDataSourceを渡します
        report.Fill(New SubPageDataSource(subPages, "group1", "page1", "page2"))
        Dim pages As ReportPages = report.GetPages()

        ' PDF出力
        Using fs As New FileStream("output\example_subpage.pdf", IO.FileMode.Create)
            Dim renderer As New PdfRenderer(fs)
            pages.Render(renderer)
        End Using

        ' XLS出力
        Using fs As New FileStream("output\example_subpage.xls", IO.FileMode.Create)
            Dim workbook As New HSSFWorkbook
            Dim renderer As New XlsRenderer(workbook)
            renderer.NewSheet("example_subpage")
            pages.Render(renderer)
            workbook.Write(fs)
        End Using

        ' XLSX出力
        Using fs As New FileStream("output\example_subpage.xlsx", IO.FileMode.Create)
            Dim workbook As New XSSFWorkbook
            Dim renderer As New XlsxRenderer(workbook)
            renderer.NewSheet("example_subpage")
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
        ret.Columns.Add("g1", GetType(String))
        ret.Columns.Add("g2", GetType(String))
        ret.Columns.Add("num", GetType(Decimal))
        ret.Rows.Add("A", "A1", 123)
        ret.Rows.Add("A", "A1", 456)
        ret.Rows.Add("A", "A1", 200)
        ret.Rows.Add("A", "A1", 100)
        ret.Rows.Add("A", "A1", 99)
        ret.Rows.Add("A", "A1", 88)
        ret.Rows.Add("A", "A1", 77)
        ret.Rows.Add("A", "A1", 230)
        ret.Rows.Add("A", "A2", 109)
        ret.Rows.Add("A", "A2", 10)
        ret.Rows.Add("A", "A3", 120)
        ret.Rows.Add("A", "A3", 63)
        ret.Rows.Add("A", "A4", 30)
        ret.Rows.Add("A", "A4", 97)
        ret.Rows.Add("B", "B1", 10)
        ret.Rows.Add("B", "B2", 22)
        ret.Rows.Add("B", "B2", 44)
        Return ret
    End Function


End Module
