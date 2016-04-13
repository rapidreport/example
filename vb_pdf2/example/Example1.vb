Imports System.IO

Imports jp.co.systembase.json
Imports jp.co.systembase.report
Imports jp.co.systembase.report.data
Imports jp.co.systembase.report.renderer.pdf2

' 基本サンプル1 見積書
Module Example1

    Public Sub Run()

        ' 帳票定義ファイルを読込みます
        Dim report As New Report(Json.Read("report\example1.rrpt"))

        ' 帳票にデータを渡します
        report.Fill(New ReportDataSource(getDataTable))

        ' ページ分割を行います
        Dim pages As ReportPages = report.GetPages()

        ' PDF出力
        Using fs As New FileStream("output\example1.pdf", IO.FileMode.Create)
            Dim renderer As New PdfRenderer(fs)
            ' バックスラッシュ文字を円マーク文字に変換します
            renderer.Setting.ReplaceBackslashToYen = True
            pages.Render(renderer)
        End Using
        MessageBox.Show("PDF出力しました")
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
