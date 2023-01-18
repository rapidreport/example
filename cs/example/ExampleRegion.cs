using System.IO;
using System.Data;

using jp.co.systembase.NPOI.HSSF.UserModel;
using jp.co.systembase.NPOI.XSSF.UserModel;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.component;
using jp.co.systembase.report.customizer;
using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xls;
using jp.co.systembase.report.renderer.xlsx;

// 機能サンプル コンテントのサイズ変更
namespace example
{
    class ExampleRegion
    {

        public static void Run()
        {

            // 第2引数にCustomizerオブジェクトを渡します
            Report report = new Report(Json.Read("report/example_region.rrpt"), new Customizer());
            report.Fill(new ReportDataSource(getDataTable()));
            ReportPages pages = report.GetPages();

            // PDF出力
            using (FileStream fs = new FileStream("output/example_region.pdf", FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                renderer.Setting.ReplaceBackslashToYen = true;
                pages.Render(renderer);
            }

            // XLS出力
            using (FileStream fs = new FileStream("output/example_region.xls", FileMode.Create))
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                XlsRenderer renderer = new XlsRenderer(workbook);
                renderer.NewSheet("example_region");
                pages.Render(renderer);
                workbook.Write(fs);
            }

            // XLSX出力
            using (FileStream fs = new FileStream("output/example_region.xlsx", FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                renderer.NewSheet("example_region");
                pages.Render(renderer);
                workbook.Write(fs);
            }

            // プレビュー
            {
                FmPrintPreview preview = new FmPrintPreview(new Printer(pages));
                preview.StartUpZoomFit = true;
                preview.ShowDialog();
            }

        }

        // コンテントのサイズを動的に変更するカスタマイザ
        private class Customizer : DefaultCustomizer
        {
            public override Region ContentRegion(Content content, Evaluator evaluator, Region region)
            {
                // "content_example"という識別子を持ったコンテントに対して処理を行います
                if ("content_example".Equals(content.Design.Id))
                {
                    // regionはコンテントの表示領域を表します
                    // ".HEIGHT"という式を評価することでHEIGHT列の値を取得し、
                    // コンテントの高さを設定します
                    Region ret = new Region(region); // regionのクローンを作成します
                    ret.SetHeight((float)evaluator.EvalTry(".HEIGHT"));
                    return ret;
                }
                else
                {
                    return region;
                }
            }
        }

        private static DataTable getDataTable()
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("HEIGHT", typeof(float));
            ret.Rows.Add(20);
            ret.Rows.Add(30);
            ret.Rows.Add(40);
            ret.Rows.Add(50);
            ret.Rows.Add(60);
            ret.Rows.Add(70);
            ret.Rows.Add(80);
            ret.Rows.Add(90);
            ret.Rows.Add(100);
            return ret;
        }

    }
}
