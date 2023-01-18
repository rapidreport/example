Imports System.IO

Imports iTextSharp.text.pdf
Imports jp.co.systembase.NPOI.SS.UserModel
Imports jp.co.systembase.NPOI.HSSF.UserModel
Imports jp.co.systembase.NPOI.XSSF.UserModel

Imports jp.co.systembase.report
Imports jp.co.systembase.report.textformatter
Imports jp.co.systembase.report.component
Imports jp.co.systembase.report.data
Imports jp.co.systembase.json

Imports jp.co.systembase.report.renderer
Imports jp.co.systembase.report.renderer.gdi
Imports jp.co.systembase.report.renderer.pdf
Imports jp.co.systembase.report.renderer.xls
Imports jp.co.systembase.report.renderer.xlsx

' 機能サンプル　カスタム書式/要素
Module ExampleExtention

    Public Sub Run()

        ' 郵便番号フォーマッタが設定されたSettingオブジェクトを用意します
        Dim setting As New ReportSetting
        setting.TextFormatterMap.Add("yubin", New YubinTextFormatter)

        Dim report As New Report(Json.Read("report/example_extention.rrpt"), setting)
        report.Fill(New ReportDataSource(getDataTable))
        Dim pages As ReportPages = report.GetPages()

        ' PDF出力
        Using fs As New FileStream("output/example_extention.pdf", IO.FileMode.Create)
            ' チェックボックスレンダラが設定されたSettingオブジェクトを用意します
            Dim pdfSetting As New PdfRendererSetting
            pdfSetting.ElementRendererMap.Add("checkbox", New PdfCheckBoxRenderer)

            Dim renderer As New PdfRenderer(fs, pdfSetting)
            pages.Render(renderer)
        End Using

        ' XLS出力
        Using fs As New FileStream("output/example_extention.xls", IO.FileMode.Create)
            ' チェックボックスレンダラが設定されたSettingオブジェクトを用意します
            Dim xlsSetting As New XlsRendererSetting
            xlsSetting.ElementRendererMap.Add("checkbox", New XlsCheckBoxRenderer)

            Dim workbook As New HSSFWorkbook
            Dim renderer As New XlsRenderer(workbook, xlsSetting)
            renderer.NewSheet("example_extention")
            pages.Render(renderer)
            workbook.Write(fs)
        End Using

        ' XLSX出力
        Using fs As New FileStream("output/example_extention.xlsx", IO.FileMode.Create)
            ' チェックボックスレンダラが設定されたSettingオブジェクトを用意します
            Dim xlsxSetting As New XlsxRendererSetting
            xlsxSetting.ElementRendererMap.Add("checkbox", New XlsxCheckBoxRenderer)

            Dim workbook As New XSSFWorkbook
            Dim renderer As New XlsxRenderer(workbook, xlsxSetting)
            renderer.NewSheet("example_extention")
            pages.Render(renderer)
            workbook.Write(fs)
        End Using

        ' プレビュー
        With Nothing
            ' チェックボックスレンダラが設定されたSettingオブジェクトを用意します
            Dim gdiSetting As New GdiRendererSetting
            gdiSetting.ElementRendererMap.Add("checkbox", New GdiCheckBoxRenderer)

            Dim preview As New FmPrintPreview(New Printer(pages, gdiSetting))
            preview.StartUpZoomFit = True
            preview.ShowDialog()
        End With

    End Sub

    Private Function getDataTable() As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("check", GetType(Boolean))
        ret.Rows.Add(True)
        ret.Rows.Add(False)
        ret.Rows.Add(True)
        ret.Rows.Add(False)
        Return ret
    End Function

    ' 郵便番号フォーマッタ
    Public Class YubinTextFormatter
        Implements ITextFormatter
        Public Function Format( _
          v As Object, _
          design As ElementDesign) As String Implements ITextFormatter.Format
            If v Is Nothing Then
                Return Nothing
            End If
            Dim _v As String = v.ToString
            If _v.Length > 3 Then
                Return _v.Substring(0, 3) & "-" & _v.Substring(3)
            Else
                Return _v
            End If
        End Function
    End Class

    ' チェックボックスを描く要素レンダラ(プレビュー・直接印刷)
    Public Class GdiCheckBoxRenderer
        Implements gdi.elementrenderer.IElementRenderer
        Public Sub Render( _
          env As gdi.RenderingEnv, _
          reportDesign As ReportDesign, _
          region As Region, _
          design As ElementDesign, _
          data As Object) Implements gdi.elementrenderer.IElementRenderer.Render
            Dim r As Region = region.ToPointScale(reportDesign)
            Dim x As Single = r.Left + r.GetWidth / 2
            Dim y As Single = r.Top + r.GetHeight / 2
            Dim w As Single = 12
            env.Graphics.DrawRectangle(Pens.Black, x - w / 2, y - w / 2, w, w)
            If data Then
                Dim p As Point() = { _
                    New Point(x - w / 2, y - w / 4), _
                    New Point(x - w / 4, y + w / 2), _
                    New Point(x + w / 2, y - w / 2), _
                    New Point(x - w / 4, y)}
                env.Graphics.FillPolygon(Brushes.SteelBlue, p)
            End If
        End Sub
    End Class

    ' チェックボックスを描く要素レンダラ(PDF)
    Public Class PdfCheckBoxRenderer
        Implements pdf.elementrenderer.IElementRenderer
        Public Sub Render( _
          renderer As pdf.PdfRenderer, _
          reportDesign As ReportDesign, _
          region As Region, _
          design As ElementDesign, _
          data As Object) Implements pdf.elementrenderer.IElementRenderer.Render
            Dim r As Region = region.ToPointScale(reportDesign)
            Dim cb As PdfContentByte = renderer.Writer.DirectContent
            Dim x As Single = r.Left + r.GetWidth / 2
            Dim y As Single = r.Top + r.GetHeight / 2
            Dim w As Single = 12
            cb.SaveState()
            cb.Rectangle(renderer.Trans.X(x - w / 2), renderer.Trans.Y(y - w / 2), w, -w)
            cb.Stroke()
            If data Then
                cb.SetColorFill(PdfRenderUtil.GetColor("steelblue"))
                cb.MoveTo(renderer.Trans.X(x - w / 2), renderer.Trans.Y(y - w / 4))
                cb.LineTo(renderer.Trans.X(x - w / 4), renderer.Trans.Y(y + w / 2))
                cb.LineTo(renderer.Trans.X(x + w / 2), renderer.Trans.Y(y - w / 2))
                cb.LineTo(renderer.Trans.X(x - w / 4), renderer.Trans.Y(y))
                cb.Fill()
            End If
            cb.RestoreState()
        End Sub
    End Class

    ' チェックボックスを描く要素レンダラ(XLS)
    Public Class XlsCheckBoxRenderer
        Implements xls.elementrenderer.IElementRenderer
        Public Sub Collect( _
          renderer As xls.XlsRenderer, _
          reportDesign As ReportDesign, _
          region As Region, _
          design As ElementDesign, _
          data As Object) Implements xls.elementrenderer.IElementRenderer.Collect
            Dim r As Region = region.ToPointScale(reportDesign)
            Dim shape As New xls.component.Shape
            shape.Region = r
            shape.Renderer = New CheckBoxShapeRenderer(data)
            renderer.CurrentPage.Shapes.Add(shape)
        End Sub

        Private Shared checkedImage As Image = Nothing
        Private Shared noCheckedImage As Image = Nothing

        Private Shared Sub createImage()
            If checkedImage Is Nothing Then
                checkedImage = New Bitmap(40, 40)
                Dim g As Graphics = Graphics.FromImage(checkedImage)
                g.DrawRectangle(Pens.Black, 10, 10, 20, 20)
                Dim p As Point() = { _
                    New Point(10, 15), _
                    New Point(15, 30), _
                    New Point(30, 10), _
                    New Point(15, 20)}
                g.FillPolygon(Brushes.SteelBlue, p)
            End If
            If noCheckedImage Is Nothing Then
                noCheckedImage = New Bitmap(40, 40)
                Dim g As Graphics = Graphics.FromImage(noCheckedImage)
                g.DrawRectangle(Pens.Black, 10, 10, 20, 20)
            End If
        End Sub

        Public Class CheckBoxShapeRenderer
            Implements xls.elementrenderer.IShapeRenderer
            Public data As Object
            Public Sub New(data As Object)
                Me.data = data
            End Sub
            Public Sub Render(page As xls.component.Page, shape As xls.component.Shape) _
              Implements xls.elementrenderer.IShapeRenderer.Render
                createImage()
                Dim index As Integer
                If Me.data Then
                    index = page.Renderer.GetImageIndex(checkedImage)
                Else
                    index = page.Renderer.GetImageIndex(noCheckedImage)
                End If
                If index > 0 Then
                    Dim p As HSSFPatriarch = page.Renderer.Sheet.DrawingPatriarch
                    p.CreatePicture(shape.GetHSSFClientAnchor(page.TopRow), index)
                End If
            End Sub
        End Class
    End Class

    ' チェックボックスを描く要素レンダラ(XLSX)
    Public Class XlsxCheckBoxRenderer
        Implements xlsx.elementrenderer.IElementRenderer
        Public Sub Collect( _
          renderer As xlsx.XlsxRenderer, _
          reportDesign As ReportDesign, _
          region As Region, _
          design As ElementDesign, _
          data As Object) Implements xlsx.elementrenderer.IElementRenderer.Collect
            Dim r As Region = region.ToPointScale(reportDesign)
            Dim shape As New xlsx.component.Shape
            shape.Region = r
            shape.Renderer = New CheckBoxShapeRenderer(data)
            renderer.CurrentPage.Shapes.Add(shape)
        End Sub

        Private Shared checkedImage As Image = Nothing
        Private Shared noCheckedImage As Image = Nothing

        Private Shared Sub createImage()
            If checkedImage Is Nothing Then
                checkedImage = New Bitmap(40, 40)
                Dim g As Graphics = Graphics.FromImage(checkedImage)
                g.DrawRectangle(Pens.Black, 10, 10, 20, 20)
                Dim p As Point() = { _
                    New Point(10, 15), _
                    New Point(15, 30), _
                    New Point(30, 10), _
                    New Point(15, 20)}
                g.FillPolygon(Brushes.SteelBlue, p)
            End If
            If noCheckedImage Is Nothing Then
                noCheckedImage = New Bitmap(40, 40)
                Dim g As Graphics = Graphics.FromImage(noCheckedImage)
                g.DrawRectangle(Pens.Black, 10, 10, 20, 20)
            End If
        End Sub

        Public Class CheckBoxShapeRenderer
            Implements xlsx.elementrenderer.IShapeRenderer
            Public data As Object
            Public Sub New(data As Object)
                Me.data = data
            End Sub
            Public Sub Render(page As xlsx.component.Page, shape As xlsx.component.Shape) _
              Implements xlsx.elementrenderer.IShapeRenderer.Render
                createImage()
                Dim index As Integer
                If Me.data Then
                    index = page.Renderer.GetImageIndex(checkedImage)
                Else
                    index = page.Renderer.GetImageIndex(noCheckedImage)
                End If
                If index >= 0 Then
                    Dim p As IDrawing = page.Renderer.Sheet.CreateDrawingPatriarch
                    p.CreatePicture(shape.GetXSSFClientAnchor(page.TopRow), index)
                End If
            End Sub
        End Class
    End Class

End Module