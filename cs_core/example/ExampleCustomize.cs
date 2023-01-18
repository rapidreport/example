using System;
using System.IO;
using System.Data;

using jp.co.systembase.NPOI.XSSF.UserModel;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.data;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xlsx;

// 機能サンプル 動的要素(デザイナのみ)
namespace example
{
    class ExampleCustomize
    {

        public static void Run()
        {
            // カスタマイザを指定せずに、Reportオブジェクトを生成します
            Report report = new Report(Json.Read("report/example_customize.rrpt"));
            report.Fill(new ReportDataSource(getDataTable()));
            ReportPages pages = report.GetPages();

            // PDF出力
            using (FileStream fs = new FileStream("output/example_customize.pdf", FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                renderer.Setting.ReplaceBackslashToYen = true;
                pages.Render(renderer);
            }

            // XLSX出力
            using (FileStream fs = new FileStream("output/example_customize.xlsx", FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                renderer.NewSheet("example_customize");
                pages.Render(renderer);
                workbook.Write(fs);
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