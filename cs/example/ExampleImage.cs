using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

using jp.co.systembase.NPOI.HSSF.UserModel;
using jp.co.systembase.NPOI.XSSF.UserModel;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer;
using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.gdi.imageloader;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.pdf.imageloader;
using jp.co.systembase.report.renderer.xls;
using jp.co.systembase.report.renderer.xls.imageloader;
using jp.co.systembase.report.renderer.xlsx;
using jp.co.systembase.report.renderer.xlsx.imageloader;

// 機能サンプル 動的画像(グラフ)の表示
namespace example
{
    class ExampleImage
    {

        public static void Run()
        {
            // 帳票定義ファイルを読み込みます
            Report report = new Report(Json.Read("report/example_image.rrpt"));

            // 帳票にデータを渡します
            report.Fill(new ReportDataSource(getDataTable()));

            // ページ分割を行います
            ReportPages pages = report.GetPages();

            // イメージマップを生成します
            ImageMap imageMap = getImageMap();

            // PDF出力
            using (FileStream fs = new FileStream("output/example_image.pdf", FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                
                // イメージローダを登録します
                renderer.ImageLoaderMap.Add("image", new PdfImageLoader(imageMap));
                renderer.ImageLoaderMap.Add("graph", new PdfGraphImageLoader());

                pages.Render(renderer);
            }

            // XLS出力
            using (FileStream fs = new FileStream("output/example_image.xls", FileMode.Create))
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                XlsRenderer renderer = new XlsRenderer(workbook);
                renderer.NewSheet("example_image");

                // イメージローダを登録します
                renderer.ImageLoaderMap.Add("image", new XlsImageLoader(imageMap));
                renderer.ImageLoaderMap.Add("graph", new XlsGraphImageLoader());

                pages.Render(renderer);
                workbook.Write(fs);
            }

            // XLSX出力
            using (FileStream fs = new FileStream("output/example_image.xlsx", FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                renderer.NewSheet("example_image");

                // イメージローダを登録します
                renderer.ImageLoaderMap.Add("image", new XlsxImageLoader(imageMap));
                renderer.ImageLoaderMap.Add("graph", new XlsxGraphImageLoader());

                pages.Render(renderer);
                workbook.Write(fs);
            }

            // プレビュー画面表示
            {
                Printer printer = new Printer(pages);

                // イメージローダを登録します
                printer.ImageLoaderMap.Add("image", new GdiImageLoader(imageMap));
                printer.ImageLoaderMap.Add("graph", new GdiGraphImageLoader());

                FmPrintPreview preview = new FmPrintPreview(printer);
                preview.StartUpZoomFit = true;
                preview.ShowDialog();
            }

        }

        private static DataTable getDataTable()
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("code", typeof(int));
            ret.Columns.Add("name", typeof(String));
            ret.Rows.Add(1, "ハクサンイチゲ");
            ret.Rows.Add(2, "ニッコウキスゲ");
            ret.Rows.Add(3, "チングルマ");
            ret.Rows.Add(4, "コマクサ");
            return ret;
        }

        // イメージマップを生成します
        private static ImageMap getImageMap()
        {
            ImageMap ret = new ImageMap();
            ret.Add(1, new Bitmap("report/image1.jpg"));
            ret.Add(2, new Bitmap("report/image2.jpg"));
            ret.Add(3, new Bitmap("report/image3.jpg"));
            ret.Add(4, new Bitmap("report/image4.jpg"));
            return ret;
        }

        // グラフの画像を直接印刷・プレビューに埋め込むためのイメージローダ
        private class GdiGraphImageLoader : IGdiImageLoader
        {
            // 画像をキャッシュするためのDictionary
            private Dictionary<Object, Image> cachedImage = new Dictionary<object, Image>();
            public Image GetImage(object param)
            {
                if (param == null)
                {
                    return null;
                }
                // 画像がキャッシュになければ生成します
                if (!this.cachedImage.ContainsKey(param))
                {
                    this.cachedImage.Add(param, getGraphImage(param));
                }
                return this.cachedImage[param];
            }
        }

        // グラフの画像をPDFに埋め込むためのイメージローダ
        private class PdfGraphImageLoader : IPdfImageLoader
        {
            // 画像をキャッシュするためのDictionary
            private Dictionary<Object, iTextSharp.text.Image> cachedImage = 
                new Dictionary<object, iTextSharp.text.Image>();
            public iTextSharp.text.Image GetImage(object param)
            {
                if (param == null)
                {
                    return null;
                }
                // 画像がキャッシュになければ生成します
                if (!this.cachedImage.ContainsKey(param))
                {
                    Image image = getGraphImage(param);
                    this.cachedImage.Add(param, 
                        iTextSharp.text.Image.GetInstance(image, image.RawFormat));
                }
                return this.cachedImage[param];
            }
        }

        // グラフの画像をXLSに埋め込むためのイメージローダ
        private class XlsGraphImageLoader : IXlsImageLoader
        {
            // 画像をキャッシュするためのDictionary
            private Dictionary<Object, Image> cachedImage = new Dictionary<object, Image>();
            public Image GetImage(object param)
            {
                if (param == null)
                {
                    return null;
                }
                // 画像がキャッシュになければ生成します
                if (!this.cachedImage.ContainsKey(param))
                {
                    this.cachedImage.Add(param, getGraphImage(param));
                }
                return this.cachedImage[param];
            }
        }

        // グラフの画像をXLSXに埋め込むためのイメージローダ
        private class XlsxGraphImageLoader : IXlsxImageLoader
        {
            // 画像をキャッシュするためのDictionary
            private Dictionary<Object, Image> cachedImage = new Dictionary<object, Image>();
            public Image GetImage(object param)
            {
                if (param == null)
                {
                    return null;
                }
                // 画像がキャッシュになければ生成します
                if (!this.cachedImage.ContainsKey(param))
                {
                    this.cachedImage.Add(param, getGraphImage(param));
                }
                return this.cachedImage[param];
            }
        }

        // グラフの画像を生成します
        private static Image getGraphImage(Object param)
        { 
            using (Chart chart = new Chart())
            {
                chart.Width = 800;
                chart.Height = 500;
                chart.Legends.Add(new Legend());
                chart.Legends[0].Title = "スマホ販売台数シェア";
                chart.ChartAreas.Add(new ChartArea());
                {
                    Series s = chart.Series.Add("Android");
                    s.ChartType = SeriesChartType.Line;
                    s.Points.Add(getDataPoint("2010 1Q", 9.6));
                    s.Points.Add(getDataPoint("2010 2Q", 17.2));
                    s.Points.Add(getDataPoint("2010 3Q", 25.3));
                    s.Points.Add(getDataPoint("2010 4Q", 30.5));
                    s.Points.Add(getDataPoint("2011 1Q", 36.4));
                    s.Points.Add(getDataPoint("2011 2Q", 43.4));
                    s.Points.Add(getDataPoint("2011 3Q", 52.5));
                    s.Points.Add(getDataPoint("2011 4Q", 50.9));
                    s.Points.Add(getDataPoint("2012 1Q", 56.1));
                    s.Points.Add(getDataPoint("2012 2Q", 64.1));
                    s.Points.Add(getDataPoint("2012 3Q", 72.4));
                    s.Points.Add(getDataPoint("2012 4Q", 69.7));
                }
                {
                    Series s = chart.Series.Add("iOS");
                    s.ChartType = SeriesChartType.Line;
                    s.Points.Add(getDataPoint("2010 1Q", 15.3));
                    s.Points.Add(getDataPoint("2010 2Q", 14.1));
                    s.Points.Add(getDataPoint("2010 3Q", 16.6));
                    s.Points.Add(getDataPoint("2010 4Q", 15.8));
                    s.Points.Add(getDataPoint("2011 1Q", 16.9));
                    s.Points.Add(getDataPoint("2011 2Q", 18.2));
                    s.Points.Add(getDataPoint("2011 3Q", 15.0));
                    s.Points.Add(getDataPoint("2011 4Q", 23.8));
                    s.Points.Add(getDataPoint("2012 1Q", 22.9));
                    s.Points.Add(getDataPoint("2012 2Q", 18.8));
                    s.Points.Add(getDataPoint("2012 3Q", 13.9));
                    s.Points.Add(getDataPoint("2012 4Q", 20.9));
                }
                {
                    Series s = chart.Series.Add("Symbian");
                    s.ChartType = SeriesChartType.Line;
                    s.Points.Add(getDataPoint("2010 1Q", 44.2));
                    s.Points.Add(getDataPoint("2010 2Q", 40.9));
                    s.Points.Add(getDataPoint("2010 3Q", 36.3));
                    s.Points.Add(getDataPoint("2010 4Q", 32.3));
                    s.Points.Add(getDataPoint("2011 1Q", 27.7));
                    s.Points.Add(getDataPoint("2011 2Q", 22.1));
                    s.Points.Add(getDataPoint("2011 3Q", 16.9));
                    s.Points.Add(getDataPoint("2011 4Q", 11.7));
                    s.Points.Add(getDataPoint("2012 1Q", 8.6));
                    s.Points.Add(getDataPoint("2012 2Q", 5.9));
                    s.Points.Add(getDataPoint("2012 3Q", 2.6));
                    s.Points.Add(getDataPoint("2012 4Q", 1.2));
                }
                {
                    Series s = chart.Series.Add("Rim");
                    s.ChartType = SeriesChartType.Line;
                    s.Points.Add(getDataPoint("2010 1Q", 19.7));
                    s.Points.Add(getDataPoint("2010 2Q", 18.7));
                    s.Points.Add(getDataPoint("2010 3Q", 15.4));
                    s.Points.Add(getDataPoint("2010 4Q", 14.6));
                    s.Points.Add(getDataPoint("2011 1Q", 13.0));
                    s.Points.Add(getDataPoint("2011 2Q", 11.7));
                    s.Points.Add(getDataPoint("2011 3Q", 11.0));
                    s.Points.Add(getDataPoint("2011 4Q", 8.8));
                    s.Points.Add(getDataPoint("2012 1Q", 6.9));
                    s.Points.Add(getDataPoint("2012 2Q", 5.2));
                    s.Points.Add(getDataPoint("2012 3Q", 5.3));
                    s.Points.Add(getDataPoint("2012 4Q", 3.5));
                }
                MemoryStream ms = new MemoryStream();
                chart.SaveImage(ms, ChartImageFormat.Png);
                return new Bitmap(ms);
            }
        }

        private static DataPoint getDataPoint(Object x, Object y)
        {
            DataPoint ret = new DataPoint();
            ret.SetValueXY(x, y);
            return ret;
        }

    }
}

