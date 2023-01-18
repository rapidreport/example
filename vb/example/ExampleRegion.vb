Imports System.IO

Imports jp.co.systembase.NPOI.HSSF.UserModel
Imports jp.co.systembase.NPOI.XSSF.UserModel

Imports jp.co.systembase.json
Imports jp.co.systembase.report
Imports jp.co.systembase.report.component
Imports jp.co.systembase.report.data
Imports jp.co.systembase.report.renderer.pdf
Imports jp.co.systembase.report.renderer.gdi
Imports jp.co.systembase.report.renderer.xls
Imports jp.co.systembase.report.renderer.xlsx
Imports jp.co.systembase.report.customizer

' 機能サンプル コンテントのサイズ変更
Module ExampleRegion

    Public Sub Run()

        ' 第2引数にCustomizerオブジェクトを渡します
        Dim report As New Report(Json.Read("report/example_region.rrpt"), New Customizer)
        report.Fill(New ReportDataSource(getDataTable))
        Dim pages As ReportPages = report.GetPages()

        ' PDF出力
        Using fs As New FileStream("output/example_region.pdf", IO.FileMode.Create)
            pages.Render(New PdfRenderer(fs))
        End Using

        ' XLS出力
        Using fs As New FileStream("output/example_region.xls", IO.FileMode.Create)
            Dim workbook As New HSSFWorkbook
            Dim renderer As New XlsRenderer(workbook)
            renderer.NewSheet("example_region")
            pages.Render(renderer)
            workbook.Write(fs)
        End Using

        ' XLSX出力
        Using fs As New FileStream("output/example_region.xlsx", IO.FileMode.Create)
            Dim workbook As New XSSFWorkbook
            Dim renderer As New XlsxRenderer(workbook)
            renderer.NewSheet("example_region")
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

    ' コンテントのサイズを動的に変更するカスタマイザ
    Private Class Customizer
        Inherits DefaultCustomizer
        Public Overrides Function ContentRegion( _
          content As Content, _
          evaluator As Evaluator, _
          region As Region) As Region
            ' "content_example"という識別子を持ったコンテントに対して処理を行います
            If "content_example".Equals(content.Design.Id) Then
                ' regionはコンテントの表示領域を表します
                ' ".HEIGHT"という式を評価することでHEIGHT列の値を取得し、
                ' コンテントの高さを設定します
                Dim ret As New Region(region) ' regionのクローンを作成します
                ret.SetHeight(evaluator.EvalTry(".HEIGHT"))
                Return ret
            Else
                Return region
            End If
        End Function
    End Class

    Private Function getDataTable() As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("HEIGHT", GetType(Single))
        ret.Rows.Add(20)
        ret.Rows.Add(30)
        ret.Rows.Add(40)
        ret.Rows.Add(50)
        ret.Rows.Add(60)
        ret.Rows.Add(70)
        ret.Rows.Add(80)
        ret.Rows.Add(90)
        ret.Rows.Add(100)
        Return ret
    End Function

End Module