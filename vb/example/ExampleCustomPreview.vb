Imports System.IO

Imports jp.co.systembase.json
Imports jp.co.systembase.report
Imports jp.co.systembase.report.data
Imports jp.co.systembase.report.renderer.gdi

' 機能サンプル プレビュー画面のカスタマイズ
Public Module ExampleCustomPreview

    Public Sub Run()
        Dim report As New Report(Json.Read("report/example1.rrpt"))
        report.Fill(New ReportDataSource(getDataTable))
        Dim pages As ReportPages = report.GetPages()

        Dim printer As New Printer(pages)
        ' カスタマイズされたプレビュー画面を表示します
        Dim preview As New MyFmPrintPreview(printer)
        preview.ShowDialog()
    End Sub

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
        ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", Nothing), _
            "株式会社 岩手商事", "北上支社", _
            "ノートパソコン", 1, 10, "台", 70000)
        ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", Nothing), _
            "株式会社 岩手商事", "北上支社", _
            "モニター", 1, 10, "台", 20000)
        ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", Nothing), _
            "株式会社 岩手商事", "北上支社", _
            "プリンタ", 1, 2, "台", 25000)
        ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", Nothing), _
            "株式会社 岩手商事", "北上支社", _
            "トナーカートリッジ", 2, 2, "本", 5000)
        Return ret
    End Function

End Module
