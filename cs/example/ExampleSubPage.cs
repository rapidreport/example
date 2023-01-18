using System;
using System.IO;
using System.Data;

using jp.co.systembase.NPOI.HSSF.UserModel;
using jp.co.systembase.NPOI.XSSF.UserModel;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xls;
using jp.co.systembase.report.renderer.xlsx;

// 機能サンプル 段組帳票(サブページ)
namespace example
{
    class ExampleSubPage
    {

        public static void Run()
        {

            // サブページを先に生成します
            Report subReport = new Report(Json.Read("report/example_subpage2.rrpt"));
            subReport.Fill(new ReportDataSource(getDataTable()));
            ReportPages subPages = subReport.GetPages();

            Report report = new Report(Json.Read("report/example_subpage1.rrpt"));
            // 外枠帳票にサブページを登録します
            report.AddSubPages("subpage", subPages);
            // 外枠帳票の中でサブページが正しく割り当てられるようにSubPageDataSourceを渡します
            report.Fill(new SubPageDataSource(subPages, "group1", "page1", "page2"));
            ReportPages pages = report.GetPages();

            // PDF出力
            using (FileStream fs = new FileStream("output/example_subpage.pdf", FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                renderer.Setting.ReplaceBackslashToYen = true;
                pages.Render(renderer);
            }

            // XLS出力
            using (FileStream fs = new FileStream("output/example_subpage.xls", FileMode.Create))
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                XlsRenderer renderer = new XlsRenderer(workbook);
                renderer.NewSheet("example_subpage");
                pages.Render(renderer);
                workbook.Write(fs);
            }

            // XLSX出力
            using (FileStream fs = new FileStream("output/example_subpage.xlsx", FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                renderer.NewSheet("example_subpage");
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

        private static DataTable getDataTable()
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("g1", typeof(String));
            ret.Columns.Add("g2", typeof(String));
            ret.Columns.Add("num", typeof(Decimal));
            ret.Rows.Add("A", "A1", 123);
            ret.Rows.Add("A", "A1", 456);
            ret.Rows.Add("A", "A1", 200);
            ret.Rows.Add("A", "A1", 100);
            ret.Rows.Add("A", "A1", 99);
            ret.Rows.Add("A", "A1", 88);
            ret.Rows.Add("A", "A1", 77);
            ret.Rows.Add("A", "A1", 230);
            ret.Rows.Add("A", "A2", 109);
            ret.Rows.Add("A", "A2", 10);
            ret.Rows.Add("A", "A3", 120);
            ret.Rows.Add("A", "A3", 63);
            ret.Rows.Add("A", "A4", 30);
            ret.Rows.Add("A", "A4", 97);
            ret.Rows.Add("B", "B1", 10);
            ret.Rows.Add("B", "B2", 22);
            ret.Rows.Add("B", "B2", 44);
            return ret;
        }

    }

}
