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

' 基本サンプル2 売上明細表
Module Example2

    Public Sub Run()

        ' 帳票定義ファイルを読込みます
        Dim report As New Report(Json.Read("report\example2.rrpt"))

        ' GlobalScopeに値を登録します
        report.GlobalScope.Add("startDate", DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", Nothing))
        report.GlobalScope.Add("endDate", DateTime.ParseExact("2013/02/28", "yyyy/MM/dd", Nothing))
        report.GlobalScope.Add("printDate", Today)
        report.GlobalScope.Add("kaisha", "株式会社　システムベース")

        ' 帳票にデータを渡します
        report.Fill(New ReportDataSource(getDataTable))

        ' ページ分割を行います
        Dim pages As ReportPages = report.GetPages()

        ' PDF出力
        Using fs As New FileStream("output\example2.pdf", IO.FileMode.Create)
            Dim renderer As New PdfRenderer(fs)
            ' バックスラッシュ文字を円マーク文字に変換します
            renderer.Setting.ReplaceBackslashToYen = True
            pages.Render(renderer)
        End Using

        ' XLS出力
        Using fs As New FileStream("output\example2.xls", IO.FileMode.Create)
            Dim workbook As New HSSFWorkbook
            Dim renderer As New XlsRenderer(workbook)
            ' Renderメソッドを呼ぶ前に必ずNewSheetメソッドを呼んでワークシートを作成します
            renderer.NewSheet("売上明細表")
            pages.Render(renderer)
            workbook.Write(fs)
        End Using

        ' XLSX出力
        Using fs As New FileStream("output\example2.xlsx", IO.FileMode.Create)
            Dim workbook As New XSSFWorkbook
            Dim renderer As New XlsxRenderer(workbook)
            ' Renderメソッドを呼ぶ前に必ずNewSheetメソッドを呼んでワークシートを作成します
            renderer.NewSheet("売上明細表")
            pages.Render(renderer)
            workbook.Write(fs)
        End Using

        ' 直接印刷、プレビュー画面表示
        With Nothing
            Dim printer As New Printer(pages)

            '' 直接印刷
            '' ダイアログを出して印刷します
            'If printer.PrintDialog.ShowDialog = DialogResult.OK Then
            '  printer.PrintDocument.Print()
            'End If
            '' ダイアログを出さずに印刷します
            'printer.PrintDocument.Print()

            ' プレビュー画面表示
            Dim preview As New FmPrintPreview(printer)
            ' プレビュー画面が開かれた時点で表示倍率を現在のウィンドウサイズに合わせます
            preview.StartUpZoomFit = True
            preview.ShowDialog()
        End With
    End Sub

    Private Function getDataTable() As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("bumonCd", GetType(Decimal))
        ret.Columns.Add("bumon", GetType(String))
        ret.Columns.Add("uriageDate", GetType(Date))
        ret.Columns.Add("denpyoNo", GetType(Decimal))
        ret.Columns.Add("shohinCd", GetType(String))
        ret.Columns.Add("shohin", GetType(String))
        ret.Columns.Add("tanka", GetType(Decimal))
        ret.Columns.Add("suryo", GetType(Decimal))
        ret.Rows.Add(1, "第一営業部", _
            DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", Nothing), _
            1246, "PC00001", "ノートパソコン", 70000, 10)
        ret.Rows.Add(1, "第一営業部", _
            DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", Nothing), _
            1246, "DP00002", "モニター", 25000, 10)
        ret.Rows.Add(1, "第一営業部", _
            DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", Nothing), _
            1246, "PR00003", "プリンタ", 20000, 2)
        ret.Rows.Add(1, "第一営業部", _
            DateTime.ParseExact("2013/02/10", "yyyy/MM/dd", Nothing), _
            1248, "PR00003", "プリンタ", 20000, 3)
        ret.Rows.Add(2, "第二営業部", _
            DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", Nothing), _
            1247, "PC00001", "ノートパソコン", 70000, 5)
        ret.Rows.Add(2, "第二営業部", _
            DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", Nothing), _
            1247, "DP00002", "モニター", 25000, 10)
        Return ret
    End Function

End Module
