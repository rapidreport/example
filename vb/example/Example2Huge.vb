Imports System.IO

Imports NPOI.HSSF.UserModel

Imports jp.co.systembase.json
Imports jp.co.systembase.report
Imports jp.co.systembase.report.data
Imports jp.co.systembase.report.renderer.gdi
Imports jp.co.systembase.report.renderer.pdf
Imports jp.co.systembase.report.renderer.xls

' 基本サンプル2 売上明細表（PDF1000ページ）
Module Example2Huge

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

        ' PDF出力の実行時間を計測します
        Dim sw As New Stopwatch
        sw.Start()

        ' ページ分割を行います
        Dim pages As ReportPages = report.GetPages()

        ' PDF出力
        Using fs As New FileStream("output\example2_huge.pdf", IO.FileMode.Create)
            Dim renderer As New PdfRenderer(fs)
            'バックスラッシュ文字を円マーク文字に変換します
            renderer.Setting.ReplaceBackslashToYen = True
            pages.Render(renderer)
        End Using

        ' 計測結果を表示します
        MessageBox.Show("実行時間は " & sw.ElapsedMilliseconds & " ミリ秒です")
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

        ' 1000ページ分のデータを作成します
        For i As Integer = 1 To 100
            For j As Integer = 1 To 50
                ret.Rows.Add(i, "部門" & i, _
                    DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", Nothing), _
                    j, "PC00001", "ノートパソコン", 70000, 10)
                ret.Rows.Add(i, "部門" & i, _
                    DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", Nothing), _
                    j, "DP00002", "モニター", 25000, 10)
                ret.Rows.Add(i, "部門" & i, _
                    DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", Nothing), _
                    j, "PR00003", "プリンタ", 20000, 2)
                ret.Rows.Add(i, "部門" & i, _
                    DateTime.ParseExact("2013/02/10", "yyyy/MM/dd", Nothing), _
                    j, "PR00003", "プリンタ", 20000, 3)
            Next
        Next
        Return ret
    End Function

End Module
