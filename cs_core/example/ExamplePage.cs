using System;
using System.IO;
using System.Data;

using jp.co.systembase.NPOI.XSSF.UserModel;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.component;
using jp.co.systembase.report.customizer;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xlsx;

// 機能サンプル ページ挿入
namespace example
{
    class ExamplePage
    {

        public static void Run()
        {

            ReportPages pages;
            {
                // 第2引数にCustomizerオブジェクトを渡します
                Report report = new Report(Json.Read("report/example_page1.rrpt"), new Customizer());
                report.Fill(new ReportDataSource(getDataTable()));
                pages = report.GetPages();
            }

            {
                Report report = new Report(Json.Read("report/example_page3.rrpt"));
                report.Fill(DummyDataSource.GetInstance());
                // 最後のページを追加します
                pages.AddRange(report.GetPages());
            }

            // PDF出力
            using (FileStream fs = new FileStream("output/example_page.pdf", FileMode.Create))
            {
                pages.Render(new PdfRenderer(fs));
            }

            // Excel(XLSX)出力
            using (FileStream fs = new FileStream("output/example_page.xlsx", FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                renderer.NewSheet("example_page");
                pages.Render(renderer);
                workbook.Write(fs);
            }
        }

        // グループが終了するたびに集計ページを挿入するカスタマイザ
        private class Customizer : DefaultCustomizer
        {
            private ReportDesign reportDesign;

            public Customizer()
            {
                this.reportDesign = new ReportDesign(Json.Read("report/example_page2.rrpt"));
            }

            public override void PageAdded(Report report, ReportPages pages, ReportPage page)
            {
                // このメソッドはページが追加されるたびに呼ばれます
                // 直前のページで"group_example"という識別子を持ったグループが終了しているかを調べます
                Group g = page.FindFinishedGroup("group_example");
                if (g != null)
                {
                    // 直前に終了したグループのデータを用いて集計ページを作成し、挿入します
                    Report _report = new Report(this.reportDesign);
                    _report.Fill(g.Data);
                    pages.AddRange(_report.GetPages());
                }
            }

        }

        private static DataTable getDataTable()
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("GROUP_CD", typeof(String));
            ret.Columns.Add("DATA", typeof(String));
            ret.Rows.Add("A", "A-1");
            ret.Rows.Add("A", "A-2");
            ret.Rows.Add("A", "A-3");
            ret.Rows.Add("A", "A-4");
            ret.Rows.Add("A", "A-5");
            ret.Rows.Add("A", "A-6");
            ret.Rows.Add("A", "A-7");
            ret.Rows.Add("A", "A-8");
            ret.Rows.Add("A", "A-9");
            ret.Rows.Add("A", "A-10");
            ret.Rows.Add("A", "A-11");
            ret.Rows.Add("A", "A-12");
            ret.Rows.Add("A", "A-13");
            ret.Rows.Add("A", "A-14");
            ret.Rows.Add("A", "A-15");
            ret.Rows.Add("A", "A-16");
            ret.Rows.Add("A", "A-17");
            ret.Rows.Add("A", "A-18");
            ret.Rows.Add("A", "A-19");
            ret.Rows.Add("A", "A-20");
            ret.Rows.Add("B", "B-1");
            ret.Rows.Add("B", "B-2");
            ret.Rows.Add("B", "B-3");
            ret.Rows.Add("B", "B-4");
            ret.Rows.Add("B", "B-5");
            ret.Rows.Add("B", "B-6");
            ret.Rows.Add("B", "B-7");
            ret.Rows.Add("B", "B-8");
            ret.Rows.Add("B", "B-9");
            ret.Rows.Add("B", "B-10");
            return ret;
        }

    }
}
