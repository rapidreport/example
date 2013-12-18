Imports System.IO

Imports NPOI.HSSF.UserModel

Imports jp.co.systembase.json
Imports jp.co.systembase.report
Imports jp.co.systembase.report.data
Imports jp.co.systembase.report.renderer.gdi
Imports jp.co.systembase.report.renderer.pdf
Imports jp.co.systembase.report.renderer.xls

' 機能サンプル 絶対座標による配置
Module ExampleLocate

    Public Sub Run()

        Dim report As New Report(Json.Read("report\example_locate.rrpt"))
        report.Fill(New ReportDataSource(getDataTable))
        Dim pages As ReportPages = report.GetPages()

        ' PDF出力
        Using fs As New FileStream("output\example_locate.pdf", IO.FileMode.Create)
            Dim renderer As New PdfRenderer(fs)
            pages.Render(renderer)
        End Using

        ' XLS出力
        Using fs As New FileStream("output\example_locate.xls", IO.FileMode.Create)
            Dim workbook As New HSSFWorkbook
            Dim renderer As New XlsRenderer(workbook)
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

    Private Function getDataTable() As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("yubin", GetType(String))
        ret.Columns.Add("jusho", GetType(String))
        ret.Columns.Add("tokuisaki1", GetType(String))
        ret.Columns.Add("tokuisaki2", GetType(String))
        ret.Columns.Add("tanto", GetType(String))
        ret.Rows.Add("556-0005", "大阪府大阪市浪速区日本橋", "大阪販売　株式会社", "大阪支店", "菊池　誠　様")
        ret.Rows.Add("980-0000", "宮城県仙台市青葉区", "宮城事務機販売　株式会社", Nothing, "中沢　雅彦　様")
        ret.Rows.Add("420-0001", "静岡県静岡市葵区井宮町", "静岡電機　株式会社", "静岡支店", "川田　敬　様")
        ret.Rows.Add("001-0000", "北海道札幌市北区大通西", "北海道産業　株式会社", Nothing, "堀内　繁　様")
        ret.Rows.Add("220-0023", "神奈川県横浜市西区平沼", "株式会社　神奈川事務機販売", Nothing, "村田　英二　様")
        ret.Rows.Add("460-0000", "愛知県名古屋市中区", "愛知電機　株式会社", Nothing, "高橋　雅氏　様")
        ret.Rows.Add("310-0001", "茨城県水戸市三の丸", "株式会社　茨城販売", Nothing, "佐藤　健二　様")
        ret.Rows.Add("460-0008", "愛知県名古屋市中区栄", "愛知産業　株式会社", "愛知支店", "小野　修一　様")
        ret.Rows.Add("024-0073", "岩手県北上市下江釣子", "岩手販売　株式会社", Nothing, "田中　琢磨　様")
        ret.Rows.Add("920-0961", "石川県金沢市香林坊", "石川事務機販売　株式会社", "石川支店", "厚木　渡　様")
        ret.Rows.Add("330-0845", "埼玉県大宮市仲町", "埼玉電機　株式会社", Nothing, "柳田　元　様")
        ret.Rows.Add("260-0016", "千葉県千葉市中央区栄町", "株式会社　千葉電機", Nothing, "橘　孝彦　様")
        Return ret
    End Function

End Module
