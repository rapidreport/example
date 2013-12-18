Imports System.IO
Imports System.Text

Imports NPOI.HSSF.UserModel

Imports jp.co.systembase.json
Imports jp.co.systembase.report
Imports jp.co.systembase.report.renderer.gdi
Imports jp.co.systembase.report.renderer.pdf
Imports jp.co.systembase.report.renderer.xls

' 基本サンプル1 見積書 (CSVデータソース)
Module Example1Csv

    Public Sub Run()

        ' 帳票定義ファイルを読込みます
        Dim report As New Report(Json.Read("report\example1.rrpt"))

        ' CSVファイルから帳票にデータを渡します
        Using r = New StreamReader("report\data.csv", Encoding.GetEncoding("shift-jis"))
            report.Fill(New CsvDataSource(r))
        End Using

        ' ページ分割を行います
        Dim pages As ReportPages = report.GetPages()

        ' PDF出力
        Using fs As New FileStream("output\example1csv.pdf", FileMode.Create)
            Dim renderer As New PdfRenderer(fs)
            ' バックスラッシュ文字を円マーク文字に変換します
            renderer.Setting.ReplaceBackslashToYen = True
            pages.Render(renderer)
        End Using

        ' XLS出力
        Using fs As New FileStream("output\example1csv.xls", FileMode.Create)
            Dim workbook As New HSSFWorkbook
            Dim renderer As New XlsRenderer(workbook)
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

End Module
