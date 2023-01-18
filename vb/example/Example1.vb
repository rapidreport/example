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

' チュートリアル1 見積書
Module Example1

    Public Sub Run()

        ' 帳票定義ファイルを読込みます
        Dim report As New Report(Json.Read("report/example1.rrpt"))

        ' 帳票にデータを渡します
        report.Fill(New ReportDataSource(getDataTable))

        ' ページ分割を行います
        Dim pages As ReportPages = report.GetPages()

        ' PDF出力
        Using fs As New FileStream("output/example1.pdf", IO.FileMode.Create)
            Dim renderer As New PdfRenderer(fs)
            ' バックスラッシュ文字を円マーク文字に変換します
            renderer.Setting.ReplaceBackslashToYen = True
            pages.Render(renderer)
        End Using

        ' XLS出力
        Using fs As New FileStream("output/example1.xls", IO.FileMode.Create)
            Dim workbook As New HSSFWorkbook
            Dim renderer As New XlsRenderer(workbook)
            ' Renderメソッドを呼ぶ前に必ずNewSheetメソッドを呼んでワークシートを作成します
            renderer.NewSheet("見積書")
            pages.Render(renderer)
            workbook.Write(fs)
        End Using

        ' XLSX出力
        Using fs As New FileStream("output/example1.xlsx", IO.FileMode.Create)
            Dim workbook As New XSSFWorkbook
            Dim renderer As New XlsxRenderer(workbook)
            ' Renderメソッドを呼ぶ前に必ずNewSheetメソッドを呼んでワークシートを作成します
            renderer.NewSheet("見積書")
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

    ' DataTableを利用したサンプル
    Private Function getDataTable() As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("mitsumoriNo", GetType(Decimal))
        ret.Columns.Add("mitsumoriDate", GetType(Date))
        ret.Columns.Add("tokuisaki1", GetType(String))
        ret.Columns.Add("tokuisaki2", GetType(String))
        ret.Columns.Add("hinmei", GetType(String))
        ret.Columns.Add("irisu", GetType(Decimal))
        ret.Columns.Add("hakosu", GetType(Decimal))
        ret.Columns.Add("tani", GetType(String))
        ret.Columns.Add("tanka", GetType(Decimal))
        ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", Nothing),
            "株式会社 岩手商事", "北上支社",
            "ノートパソコン", 1, 10, "台", 70000)
        ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", Nothing),
            "株式会社 岩手商事", "北上支社",
            "モニター", 1, 10, "台", 20000)
        ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", Nothing),
            "株式会社 岩手商事", "北上支社",
            "プリンタ", 1, 2, "台", 25000)
        ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", Nothing),
            "株式会社 岩手商事", "北上支社",
            "トナーカートリッジ", 2, 2, "本", 5000)
        Return ret
    End Function

    '' DTOを利用したサンプル
    'Private Function getDataTable() As IList
    '    Dim ret As New List(Of ExampleDto)
    '    With Nothing
    '        Dim r As New ExampleDto
    '        r.Tokuisaki1 = "株式会社 岩手商事"
    '        r.Tokuisaki2 = "北上支社"
    '        r.MitsumoriNo = 101
    '        r.MitsumoriDate = DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", Nothing)
    '        r.Hinmei = "ノートパソコン"
    '        r.Irisu = 1
    '        r.Hakosu = 10
    '        r.Tani = "台"
    '        r.Tanka = 70000
    '        ret.Add(r)
    '    End With
    '    With Nothing
    '        Dim r As New ExampleDto
    '        r.Tokuisaki1 = "株式会社 岩手商事"
    '        r.Tokuisaki2 = "北上支社"
    '        r.MitsumoriNo = 101
    '        r.MitsumoriDate = DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", Nothing)
    '        r.Hinmei = "モニター"
    '        r.Irisu = 1
    '        r.Hakosu = 10
    '        r.Tani = "台"
    '        r.Tanka = 20000
    '        ret.Add(r)
    '    End With
    '    With Nothing
    '        Dim r As New ExampleDto
    '        r.Tokuisaki1 = "株式会社 岩手商事"
    '        r.Tokuisaki2 = "北上支社"
    '        r.MitsumoriNo = 101
    '        r.MitsumoriDate = DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", Nothing)
    '        r.Hinmei = "プリンタ"
    '        r.Irisu = 1
    '        r.Hakosu = 2
    '        r.Tani = "台"
    '        r.Tanka = 25000
    '        ret.Add(r)
    '    End With
    '    With Nothing
    '        Dim r As New ExampleDto
    '        r.Tokuisaki1 = "株式会社 岩手商事"
    '        r.Tokuisaki2 = "北上支社"
    '        r.MitsumoriNo = 101
    '        r.MitsumoriDate = DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", Nothing)
    '        r.Hinmei = "トナーカートリッジ"
    '        r.Irisu = 2
    '        r.Hakosu = 2
    '        r.Tani = "本"
    '        r.Tanka = 5000
    '        ret.Add(r)
    '    End With
    '    Return ret
    'End Function

End Module
