﻿Imports System.IO

Imports jp.co.systembase.NPOI.HSSF.UserModel
Imports jp.co.systembase.NPOI.XSSF.UserModel

Imports jp.co.systembase.json
Imports jp.co.systembase.report
Imports jp.co.systembase.report.data
Imports jp.co.systembase.report.renderer.gdi
Imports jp.co.systembase.report.renderer.pdf
Imports jp.co.systembase.report.renderer.xls
Imports jp.co.systembase.report.renderer.xlsx

' 機能サンプル コンテントの差し込み
Module ExampleMergeContent

    Public Sub Run()

        ' 差込みを行うコンテントを、あらかじめ共有コンテントへ登録しておきます
        Dim sharedReport As New ReportDesign(Json.Read("report/example_shared.rrpt"))
        jp.co.systembase.report.Report.AddSharedContent("company_info", sharedReport)

        Dim report As New Report(Json.Read("report/example_mergecontent.rrpt"))
        report.GlobalScope.Add("company_name", "株式会社ラピッドレポート")
        report.GlobalScope.Add("tel", "0000-11-2222")
        report.Fill(New ReportDataSource(getDataTable))
        Dim pages As ReportPages = report.GetPages()

        ' PDF出力
        Using fs As New FileStream("output/example_mergecontent.pdf", IO.FileMode.Create)
            Dim renderer As New PdfRenderer(fs)
            renderer.Setting.ReplaceBackslashToYen = True
            pages.Render(renderer)
        End Using

        ' XLS出力
        Using fs As New FileStream("output/example_mergecontent.xls", IO.FileMode.Create)
            Dim workbook As New HSSFWorkbook
            Dim renderer As New XlsRenderer(workbook)
            renderer.NewSheet("example_mergecontent")
            pages.Render(renderer)
            workbook.Write(fs)
        End Using

        ' XLSX出力
        Using fs As New FileStream("output/example_locate.xlsx", IO.FileMode.Create)
            Dim workbook As New XSSFWorkbook
            Dim renderer As New XlsxRenderer(workbook)
            renderer.NewSheet("example_mergecontent")
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
        ret.Columns.Add("mitsumoriNo", GetType(Decimal))
        ret.Columns.Add("mitsumoriDate", GetType(Date))
        ret.Columns.Add("tanto", GetType(String))
        ret.Columns.Add("tokuisaki1", GetType(String))
        ret.Columns.Add("tokuisaki2", GetType(String))
        ret.Columns.Add("hinmei", GetType(String))
        ret.Columns.Add("irisu", GetType(Decimal))
        ret.Columns.Add("hakosu", GetType(Decimal))
        ret.Columns.Add("tani", GetType(String))
        ret.Columns.Add("tanka", GetType(Decimal))
        ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", Nothing), _
            "営業一部 佐藤太郎", "株式会社 岩手商事", "北上支社", _
            "ノートパソコン", 1, 10, "台", 70000)
        ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", Nothing), _
            "営業一部 佐藤太郎", "株式会社 岩手商事", "北上支社", _
            "モニター", 1, 10, "台", 20000)
        ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", Nothing), _
            "営業一部 佐藤太郎", "株式会社 岩手商事", "北上支社", _
            "プリンタ", 1, 2, "台", 25000)
        ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", Nothing), _
            "営業一部 佐藤太郎", "株式会社 岩手商事", "北上支社", _
            "トナーカートリッジ", 2, 2, "本", 5000)
        Return ret
    End Function

End Module
