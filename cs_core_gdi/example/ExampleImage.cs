using System;
using System.IO;
using System.Data;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer;
using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.gdi.imageloader;

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

            // プレビュー画面表示
            {
                Printer printer = new Printer(pages);

                // イメージローダを登録します
                printer.ImageLoaderMap.Add("image", new GdiImageLoader(imageMap));

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
            ret.Add(1, File.ReadAllBytes("report/image1.jpg"));
            ret.Add(2, File.ReadAllBytes("report/image2.jpg"));
            ret.Add(3, File.ReadAllBytes("report/image3.jpg"));
            ret.Add(4, File.ReadAllBytes("report/image4.jpg"));
            return ret;
        }
    }
}

