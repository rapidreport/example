Imports System.IO
Imports System.Windows.Forms.DataVisualization.Charting

Imports NPOI.HSSF.UserModel
Imports NPOI.XSSF.UserModel

Imports jp.co.systembase.json
Imports jp.co.systembase.report
Imports jp.co.systembase.report.data
Imports jp.co.systembase.report.renderer
Imports jp.co.systembase.report.renderer.gdi
Imports jp.co.systembase.report.renderer.gdi.imageloader
Imports jp.co.systembase.report.renderer.pdf
Imports jp.co.systembase.report.renderer.pdf.imageloader
Imports jp.co.systembase.report.renderer.xls
Imports jp.co.systembase.report.renderer.xls.imageloader
Imports jp.co.systembase.report.renderer.xlsx
Imports jp.co.systembase.report.renderer.xlsx.imageloader

' 機能サンプル 動的画像(グラフ)表示
Module ExampleImage

    Public Sub Run()

        ' 帳票定義ファイルを読込みます
        Dim report As New Report(Json.Read("report\example_image.rrpt"))

        ' 帳票にデータを渡します
        report.Fill(New ReportDataSource(getDataTable))

        ' ページ分割を行います
        Dim pages As ReportPages = report.GetPages()

        ' イメージマップを生成します
        Dim imageMap As ImageMap = getImageMap()

        ' PDF出力
        Using fs As New FileStream("output\example_image.pdf", IO.FileMode.Create)
            Dim renderer As New PdfRenderer(fs)

            ' イメージローダを登録します
            renderer.ImageLoaderMap.Add("image", New PdfImageLoader(imageMap))
            renderer.ImageLoaderMap.Add("graph", New PdfGraphImageLoader())

            pages.Render(renderer)
        End Using

        ' XLS出力
        Using fs As New FileStream("output\example_image.xls", IO.FileMode.Create)
            Dim workbook As New HSSFWorkbook
            Dim renderer As New XlsRenderer(workbook)
            renderer.NewSheet("example_image")

            ' イメージローダを登録します
            renderer.ImageLoaderMap.Add("image", New XlsImageLoader(imageMap))
            renderer.ImageLoaderMap.Add("graph", New XlsGraphImageLoader())

            pages.Render(renderer)
            workbook.Write(fs)
        End Using

        ' XLSX出力
        Using fs As New FileStream("output\example_image.xlsx", IO.FileMode.Create)
            Dim workbook As New XSSFWorkbook
            Dim renderer As New XlsxRenderer(workbook)
            renderer.NewSheet("example_image")

            ' イメージローダを登録します
            renderer.ImageLoaderMap.Add("image", New XlsxImageLoader(imageMap))
            renderer.ImageLoaderMap.Add("graph", New XlsxGraphImageLoader())

            pages.Render(renderer)
            workbook.Write(fs)
        End Using

        ' プレビュー画面表示
        With Nothing
            Dim printer As New Printer(pages)

            ' イメージローダを登録します
            printer.ImageLoaderMap.Add("image", New GdiImageLoader(imageMap))
            printer.ImageLoaderMap.Add("graph", New GdiGraphImageLoader())

            Dim preview As New FmPrintPreview(printer)
            preview.StartUpZoomFit = True
            preview.ShowDialog()
        End With
    End Sub

    Private Function getDataTable() As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("code", GetType(Integer))
        ret.Columns.Add("name", GetType(String))
        ret.Rows.Add(1, "ハクサンイチゲ")
        ret.Rows.Add(2, "ニッコウキスゲ")
        ret.Rows.Add(3, "チングルマ")
        ret.Rows.Add(4, "コマクサ")
        Return ret
    End Function

    ' イメージマップを生成します
    Private Function getImageMap() As ImageMap
        Dim ret As New ImageMap
        ret.Add(1, New Bitmap("report\image1.jpg"))
        ret.Add(2, New Bitmap("report\image2.jpg"))
        ret.Add(3, New Bitmap("report\image3.jpg"))
        ret.Add(4, New Bitmap("report\image4.jpg"))
        Return ret
    End Function

    ' グラフの画像を直接印刷・プレビューに埋め込むためのイメージローダ
    Private Class GdiGraphImageLoader
        Implements IGdiImageLoader
        ' 画像をキャッシュするためのDictionary
        Private cachedImage As New Dictionary(Of Object, Image)
        Public Function GetImage(param As Object) As Image Implements IGdiImageLoader.GetImage
            If param Is Nothing Then
                Return Nothing
            End If
            ' 画像がキャッシュになければ生成します
            If Not Me.cachedImage.ContainsKey(param) Then
                Me.cachedImage.Add(param, getGraphImage(param))
            End If
            Return Me.cachedImage(param)
        End Function
    End Class

    ' グラフの画像をPDFに埋め込むためのイメージローダ
    Private Class PdfGraphImageLoader
        Implements IPdfImageLoader
        ' 画像をキャッシュするためのDictionary
        Private cachedImage As New Dictionary(Of Object, iTextSharp.text.Image)
        Public Function GetImage(param As Object) As iTextSharp.text.Image Implements IPdfImageLoader.GetImage
            If param Is Nothing Then
                Return Nothing
            End If
            ' 画像がキャッシュになければ生成します
            If Not Me.cachedImage.ContainsKey(param) Then
                Dim image As Image = getGraphImage(param)
                Me.cachedImage.Add(param, iTextSharp.text.Image.GetInstance(image, image.RawFormat))
            End If
            Return Me.cachedImage(param)
        End Function
    End Class

    ' グラフの画像をExcel(XLS)に埋め込むためのイメージローダ
    Private Class XlsGraphImageLoader
        Implements IXlsImageLoader
        ' 画像をキャッシュするためのDictionary
        Private cachedImage As New Dictionary(Of Object, Image)
        Public Function GetImage(param As Object) As Image Implements IXlsImageLoader.GetImage
            If param Is Nothing Then
                Return Nothing
            End If
            ' 画像がキャッシュになければ生成します
            If Not Me.cachedImage.ContainsKey(param) Then
                Me.cachedImage.Add(param, getGraphImage(param))
            End If
            Return Me.cachedImage(param)
        End Function
    End Class

    ' グラフの画像をExcel(XLSX)に埋め込むためのイメージローダ
    Private Class XlsxGraphImageLoader
        Implements IXlsxImageLoader
        ' 画像をキャッシュするためのDictionary
        Private cachedImage As New Dictionary(Of Object, Image)
        Public Function GetImage(param As Object) As Image Implements IXlsxImageLoader.GetImage
            If param Is Nothing Then
                Return Nothing
            End If
            ' 画像がキャッシュになければ生成します
            If Not Me.cachedImage.ContainsKey(param) Then
                Me.cachedImage.Add(param, getGraphImage(param))
            End If
            Return Me.cachedImage(param)
        End Function
    End Class

    ' グラフの画像を生成します
    Private Function getGraphImage(param As Object) As Image
        Using chart As New Chart
            chart.Width = 800
            chart.Height = 500
            chart.Legends.Add(New Legend())
            chart.Legends(0).Title = "スマホ販売台数シェア"
            chart.ChartAreas.Add(New ChartArea())
            With Nothing
                Dim s As Series = chart.Series.Add("Android")
                s.ChartType = SeriesChartType.Line
                s.Points.Add(getDataPoint("2010 1Q", 9.6))
                s.Points.Add(getDataPoint("2010 2Q", 17.2))
                s.Points.Add(getDataPoint("2010 3Q", 25.3))
                s.Points.Add(getDataPoint("2010 4Q", 30.5))
                s.Points.Add(getDataPoint("2011 1Q", 36.4))
                s.Points.Add(getDataPoint("2011 2Q", 43.4))
                s.Points.Add(getDataPoint("2011 3Q", 52.5))
                s.Points.Add(getDataPoint("2011 4Q", 50.9))
                s.Points.Add(getDataPoint("2012 1Q", 56.1))
                s.Points.Add(getDataPoint("2012 2Q", 64.1))
                s.Points.Add(getDataPoint("2012 3Q", 72.4))
                s.Points.Add(getDataPoint("2012 4Q", 69.7))
            End With
            With Nothing
                Dim s As Series = chart.Series.Add("iOS")
                s.ChartType = SeriesChartType.Line
                s.Points.Add(getDataPoint("2010 1Q", 15.3))
                s.Points.Add(getDataPoint("2010 2Q", 14.1))
                s.Points.Add(getDataPoint("2010 3Q", 16.6))
                s.Points.Add(getDataPoint("2010 4Q", 15.8))
                s.Points.Add(getDataPoint("2011 1Q", 16.9))
                s.Points.Add(getDataPoint("2011 2Q", 18.2))
                s.Points.Add(getDataPoint("2011 3Q", 15.0))
                s.Points.Add(getDataPoint("2011 4Q", 23.8))
                s.Points.Add(getDataPoint("2012 1Q", 22.9))
                s.Points.Add(getDataPoint("2012 2Q", 18.8))
                s.Points.Add(getDataPoint("2012 3Q", 13.9))
                s.Points.Add(getDataPoint("2012 4Q", 20.9))
            End With
            With Nothing
                Dim s As Series = chart.Series.Add("Symbian")
                s.ChartType = SeriesChartType.Line
                s.Points.Add(getDataPoint("2010 1Q", 44.2))
                s.Points.Add(getDataPoint("2010 2Q", 40.9))
                s.Points.Add(getDataPoint("2010 3Q", 36.3))
                s.Points.Add(getDataPoint("2010 4Q", 32.3))
                s.Points.Add(getDataPoint("2011 1Q", 27.7))
                s.Points.Add(getDataPoint("2011 2Q", 22.1))
                s.Points.Add(getDataPoint("2011 3Q", 16.9))
                s.Points.Add(getDataPoint("2011 4Q", 11.7))
                s.Points.Add(getDataPoint("2012 1Q", 8.6))
                s.Points.Add(getDataPoint("2012 2Q", 5.9))
                s.Points.Add(getDataPoint("2012 3Q", 2.6))
                s.Points.Add(getDataPoint("2012 4Q", 1.2))
            End With
            With Nothing
                Dim s As Series = chart.Series.Add("RIM")
                s.ChartType = SeriesChartType.Line
                s.Points.Add(getDataPoint("2010 1Q", 19.7))
                s.Points.Add(getDataPoint("2010 2Q", 18.7))
                s.Points.Add(getDataPoint("2010 3Q", 15.4))
                s.Points.Add(getDataPoint("2010 4Q", 14.6))
                s.Points.Add(getDataPoint("2011 1Q", 13.0))
                s.Points.Add(getDataPoint("2011 2Q", 11.7))
                s.Points.Add(getDataPoint("2011 3Q", 11.0))
                s.Points.Add(getDataPoint("2011 4Q", 8.8))
                s.Points.Add(getDataPoint("2012 1Q", 6.9))
                s.Points.Add(getDataPoint("2012 2Q", 5.2))
                s.Points.Add(getDataPoint("2012 3Q", 5.3))
                s.Points.Add(getDataPoint("2012 4Q", 3.5))
            End With
            Dim ms As New MemoryStream
            chart.SaveImage(ms, ChartImageFormat.Png)
            Return New Bitmap(ms)
        End Using
    End Function

    Private Function getDataPoint(x As Object, y As Object) As DataPoint
        Dim ret As New DataPoint
        ret.SetValueXY(x, y)
        Return ret
    End Function

End Module

