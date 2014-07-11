using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.component;
using jp.co.systembase.report.customizer;
using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xls;
using jp.co.systembase.report.renderer.xlsx;

// 機能サンプル 動的要素
namespace example
{
    class ExampleRender
    {

        public static void Run()
        {

            // 第2引数にCustomizerオブジェクトを渡します
            Report report = new Report(Json.Read("report\\example_render.rrpt"), new Customizer());
            report.Fill(new ReportDataSource(getDataTable()));
            ReportPages pages = report.GetPages();

            // PDF出力
            using (FileStream fs = new FileStream("output\\example_render.pdf", FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                renderer.Setting.ReplaceBackslashToYen = true;
                pages.Render(renderer);
            }

            // XLS出力
            using (FileStream fs = new FileStream("output\\example_render.xls", FileMode.Create))
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                XlsRenderer renderer = new XlsRenderer(workbook);
                renderer.NewSheet("example_render");
                pages.Render(renderer);
                workbook.Write(fs);
            }

            // XLSX出力
            using (FileStream fs = new FileStream("output\\example_render.xlsx", FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                renderer.NewSheet("example_render");
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

        // 要素に動的な修正を加えるカスタマイザ
        private class Customizer : DefaultCustomizer
        {
            public override void RenderContent(
                Content content, 
                Evaluator evaluator, 
                Region region, 
                ElementDesigns elementDesigns)
            {
                // このメソッドはコンテントの描画が行われる直前に呼ばれます
                // "content_example"という識別子を持ったコンテントに対して処理を行います
                if ("content_example".Equals(content.Design.Id))
                {
                    // "graph"という識別子を持った要素を取得し、レイアウトと色を修正します
                    ElementDesign e = elementDesigns.Find("graph");
                    // ".NUM"という式を評価することで、NUM列の値を得ます
                    Decimal num = (Decimal)evaluator.EvalTry(".NUM");
                    if (num >= 0)
                    {
                        e.Child("layout").Put("x1", 100);
                        e.Child("layout").Put("x2", 100 + num);
                        e.Put("fill_color", "lightblue");
                    }
                    else
                    {
                        e.Child("layout").Put("x1", 100 + num);
                        e.Child("layout").Put("x2", 100);
                        e.Put("fill_color", "pink");
                    }
                }
            }
        }

        private static DataTable getDataTable()
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("NUM", typeof(Decimal));
            ret.Rows.Add(50);
            ret.Rows.Add(40);
            ret.Rows.Add(30);
            ret.Rows.Add(20);
            ret.Rows.Add(10);
            ret.Rows.Add(0);
            ret.Rows.Add(-10);
            ret.Rows.Add(-20);
            ret.Rows.Add(-30);
            ret.Rows.Add(-40);
            ret.Rows.Add(-50);
            return ret;
        }

    }

}