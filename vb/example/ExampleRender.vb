Imports System.IO

Imports NPOI.HSSF.UserModel
Imports NPOI.XSSF.UserModel

Imports jp.co.systembase.json
Imports jp.co.systembase.report
Imports jp.co.systembase.report.component
Imports jp.co.systembase.report.data
Imports jp.co.systembase.report.renderer.gdi
Imports jp.co.systembase.report.renderer.pdf
Imports jp.co.systembase.report.renderer.xls
Imports jp.co.systembase.report.renderer.xlsx
Imports jp.co.systembase.report.customizer

' 機能サンプル 動的Element
Module ExampleRender

    Public Sub Run()

        ' 第2引数にCustomizerオブジェクトを渡します
        Dim report As New Report(Json.Read("report\example_render.rrpt"), New Customizer)
        report.Fill(New ReportDataSource(getDataTable))
        Dim pages As ReportPages = report.GetPages()

        ' PDF出力
        Using fs As New FileStream("output\example_render.pdf", IO.FileMode.Create)
            pages.Render(New PdfRenderer(fs))
        End Using

        ' XLS出力
        Using fs As New FileStream("output\example_render.xls", IO.FileMode.Create)
            Dim workbook As New HSSFWorkbook
            Dim renderer As New XlsRenderer(workbook)
            renderer.NewSheet("example_render")
            pages.Render(renderer)
            workbook.Write(fs)
        End Using

        ' XLSX出力
        Using fs As New FileStream("output\example_render.xlsx", IO.FileMode.Create)
            Dim workbook As New XSSFWorkbook
            Dim renderer As New XlsxRenderer(workbook)
            renderer.NewSheet("example_render")
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

    ' 要素に動的な修正を加えるカスタマイザ
    Private Class Customizer
        Inherits DefaultCustomizer
        Public Overrides Sub RenderContent(
          content As Content, evaluator As Evaluator, region As Region, elementDesigns As ElementDesigns)
            ' このメソッドはコンテントの描画が行われる直前に呼ばれます
            ' "content_example"という識別子を持ったコンテントに対して処理を行います
            If "content_example".Equals(content.Design.Id) Then
                ' "graph"という識別子を持った要素を取得し、レイアウトと色を修正します
                Dim e As ElementDesign = elementDesigns.Find("graph")
                ' ".NUM"という式を評価することで、NUM列の値を得ます
                Dim num As Decimal = evaluator.EvalTry(".NUM")
                If num >= 0 Then
                    e.Child("layout").Put("x1", 100)
                    e.Child("layout").Put("x2", 100 + num)
                    e.Put("fill_color", "lightblue")
                Else
                    e.Child("layout").Put("x1", 100 + num)
                    e.Child("layout").Put("x2", 100)
                    e.Put("fill_color", "pink")
                End If
            End If
        End Sub
    End Class

    Private Function getDataTable() As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("NUM", GetType(Decimal))
        ret.Rows.Add(50)
        ret.Rows.Add(40)
        ret.Rows.Add(30)
        ret.Rows.Add(20)
        ret.Rows.Add(10)
        ret.Rows.Add(0)
        ret.Rows.Add(-10)
        ret.Rows.Add(-20)
        ret.Rows.Add(-30)
        ret.Rows.Add(-40)
        ret.Rows.Add(-50)
        Return ret
    End Function

End Module
