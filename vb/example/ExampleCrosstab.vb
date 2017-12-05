Imports System.IO

Imports NPOI.HSSF.UserModel
Imports NPOI.XSSF.UserModel

Imports jp.co.systembase.json
Imports jp.co.systembase.report
Imports jp.co.systembase.report.data
Imports jp.co.systembase.report.renderer.gdi
Imports jp.co.systembase.report.renderer.pdf
Imports jp.co.systembase.report.renderer.xls
Imports jp.co.systembase.report.renderer.xlsx

' 機能サンプル 絶対座標による配置
Module ExampleCrosstab

    Public Sub Run()

        Dim report As New Report(Json.Read("report\example_crosstab.rrpt"))

        ' 横方向の列データを設定します。
        report.AddCrosstabCaptionDataSource("crosstab_example", New ReportDataSource(getCaptionDataTable()))

        report.Fill(New ReportDataSource(getDataTable))
        Dim pages As ReportPages = report.GetPages()

        ' PDF出力
        Using fs As New FileStream("output\example_crosstab.pdf", IO.FileMode.Create)
            Dim renderer As New PdfRenderer(fs)
            pages.Render(renderer)
        End Using

        ' XLS出力
        Using fs As New FileStream("output\example_crosstab.xls", IO.FileMode.Create)
            Dim workbook As New HSSFWorkbook
            Dim renderer As New XlsRenderer(workbook)
            renderer.NewSheet("example_crosstab")
            pages.Render(renderer)
            workbook.Write(fs)
        End Using

        ' XLSX出力
        Using fs As New FileStream("output\example_crosstab.xlsx", IO.FileMode.Create)
            Dim workbook As New XSSFWorkbook
            Dim renderer As New XlsxRenderer(workbook)
            renderer.NewSheet("example_locate")
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

    Private Function getCaptionDataTable() As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("period_cd", GetType(Integer))
        ret.Columns.Add("period_nm", GetType(String))
        ret.Rows.Add(1, "2010年上期")
        ret.Rows.Add(2, "2010年下期")
        ret.Rows.Add(3, "2011年上期")
        ret.Rows.Add(4, "2011年下期")
        ret.Rows.Add(5, "2012年上期")
        ret.Rows.Add(6, "2012年下期")
        ret.Rows.Add(7, "2013年上期")
        ret.Rows.Add(8, "2013年下期")
        ret.Rows.Add(9, "2014年上期")
        ret.Rows.Add(10, "2014年下期")
        ret.Rows.Add(11, "2015年上期")
        ret.Rows.Add(12, "2015年下期")
        ret.Rows.Add(13, "2016年上期")
        ret.Rows.Add(14, "2016年下期")
        Return ret
    End Function

    Private Function getDataTable() As DataTable
        Dim ret As New DataTable
        Dim branchNms() As String =
          {"北上本社", "東京支社", "盛岡営業所", "秋田営業所",
           "仙台営業所", "山形営業所", "福島営業所"}
        ret.Columns.Add("branch_cd", GetType(Integer))
        ret.Columns.Add("branch_nm", GetType(String))
        ret.Columns.Add("period_cd", GetType(Integer))
        ret.Columns.Add("amount", GetType(Decimal))
        For i As Integer = 0 To 13
            For j As Integer = 0 To 6
                ret.Rows.Add(j + 1, branchNms(j), i + 1, 10000 + i * 100 + j * 10)
            Next
        Next
        Return ret
    End Function

    'Private Function getDataTable() As DataTable
    '    Dim ret As New DataTable
    '    Dim branchNms() As String =
    '      {"北上本社", "東京支社", "盛岡営業所", "秋田営業所",
    '       "仙台営業所", "山形営業所", "福島営業所"}
    '    Dim periodNms() As String =
    '      {"2010年上期", "2010年下期", "2011年上期", "2011年下期",
    '       "2012年上期", "2012年下期", "2013年上期", "2013年下期",
    '       "2014年上期", "2014年下期", "2015年上期", "2015年下期",
    '       "2016年上期", "2016年下期"}
    '    ret.Columns.Add("branch_cd", GetType(Integer))
    '    ret.Columns.Add("branch_nm", GetType(String))
    '    ret.Columns.Add("period_cd", GetType(Integer))
    '    ret.Columns.Add("period_nm", GetType(String))
    '    ret.Columns.Add("amount", GetType(Decimal))
    '    For i As Integer = 0 To 13
    '        For j As Integer = 0 To 6
    '            ret.Rows.Add(j + 1, branchNms(j), i + 1, periodNms(i), 10000 + i * 100 + j * 10)
    '        Next
    '    Next
    '    Return ret
    'End Function

End Module

