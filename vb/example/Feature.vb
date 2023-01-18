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

' 「特徴と機能一覧」に掲載したサンプル
Module Feature

    Public Sub Run()
        Dim report As New Report(Json.Read("report/feature.rrpt"))

        ' "feature1-4"にgetDataTable1-4のデータをそれぞれ割り当てます
        Dim dataProvider As New GroupDataProvider
        dataProvider.GroupDataMap.Add("feature1", New ReportDataSource(getDataTable1))
        dataProvider.GroupDataMap.Add("feature2", New ReportDataSource(getDataTable2))
        dataProvider.GroupDataMap.Add("feature3", New ReportDataSource(getDataTable3))
        dataProvider.GroupDataMap.Add("feature4", New ReportDataSource(getDataTable4))

        ' 第2引数にdataProviderを渡します
        report.Fill(DummyDataSource.GetInstance, dataProvider)

        Dim pages As ReportPages = report.GetPages()

        ' PDF出力
        Using fs As New FileStream("output/feature.pdf", IO.FileMode.Create)
            pages.Render(New PdfRenderer(fs))
        End Using

        ' XLS出力
        Using fs As New FileStream("output/feature.xls", IO.FileMode.Create)
            Dim workbook As New HSSFWorkbook
            Dim renderer As New XlsRenderer(workbook)
            renderer.NewSheet("feature")
            pages.Render(renderer)
            workbook.Write(fs)
        End Using

        ' XLSX出力
        Using fs As New FileStream("output/feature.xlsx", IO.FileMode.Create)
            Dim workbook As New XSSFWorkbook
            Dim renderer As New XlsxRenderer(workbook)
            renderer.NewSheet("feature")
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

    Private Function getDataTable1() As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("KUBUN1", GetType(String))
        ret.Columns.Add("KUBUN2", GetType(String))
        ret.Columns.Add("ZAISHITSU", GetType(String))
        ret.Columns.Add("TANI", GetType(String))
        ret.Columns.Add("TANKA", GetType(Decimal))
        ret.Columns.Add("SURYO", GetType(Decimal))
        ret.Rows.Add("内装", "床", "フローリング", "㎡", 7250, 20)
        ret.Rows.Add("内装", "床", "畳", "枚", 4500, 12)
        ret.Rows.Add("内装", "壁", "壁紙", "㎡", 1000, 30)
        ret.Rows.Add("外装", "屋根", "瓦", "枚", 13000, 40)
        ret.Rows.Add("外装", "屋根", "トタン", "㎡", 3920, 60)
        ret.Rows.Add("外装", "屋根", "化粧スレート", "㎡", 5000, 45)
        Return ret
    End Function

    Private Function getDataTable2() As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("REGION", GetType(String))
        ret.Columns.Add("PREF", GetType(String))
        ret.Rows.Add("北海道", "北海道")
        ret.Rows.Add("東北", "青森")
        ret.Rows.Add("東北", "岩手")
        ret.Rows.Add("東北", "秋田")
        ret.Rows.Add("東北", "宮城")
        ret.Rows.Add("東北", "山形")
        ret.Rows.Add("東北", "福島")
        ret.Rows.Add("関東", "茨城")
        ret.Rows.Add("関東", "栃木")
        ret.Rows.Add("関東", "群馬")
        ret.Rows.Add("関東", "埼玉")
        ret.Rows.Add("関東", "ちば")
        ret.Rows.Add("関東", "東京")
        ret.Rows.Add("関東", "神奈川")
        Return ret
    End Function

    Private Function getDataTable3() As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("WRAP", GetType(String))
        ret.Columns.Add("SHRINK", GetType(String))
        ret.Columns.Add("FIXDEC", GetType(Decimal))
        ret.Rows.Add("RapidRport", "ラピッドレポート", 12345)
        ret.Rows.Add("RapidReport 帳票ツール", "ラピッドレポート　帳票ツール", 1234.1)
        ret.Rows.Add("開発者のための帳票ツール", "開発者のための帳票ツール", 123.12)
        ret.Rows.Add("(株)システムベース", "(株)システムベース", 12.123)
        Return ret
    End Function

    Private Function getDataTable4() As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("SHOHIN_CD", GetType(String))
        ret.Columns.Add("SHOHIN_NM", GetType(String))
        ret.Columns.Add("SURYO", GetType(Decimal))
        ret.Columns.Add("TANI", GetType(String))
        ret.Columns.Add("TANKA", GetType(Decimal))
        ret.Rows.Add("SH-A0011", "冷凍コロッケ", 25, "袋", 35)
        ret.Rows.Add("SH-A0012", "冷凍ピザ", 8, "袋", 410)
        ret.Rows.Add("SH-B1005", "カップラーメン醤油味", 40, "個", 90)
        Return ret
    End Function

End Module
