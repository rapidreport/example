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
using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xls;
using jp.co.systembase.report.renderer.xlsx;

// 機能サンプル 差込コンテント
namespace example
{
    class ExampleMergeContent
    {

        public static void Run()
        {
            // 差込みを行うコンテントを、あらかじめSharedContentに追加しておきます
            ReportDesign sharedReport = new ReportDesign(Json.Read("report/example_shared.rrpt"));
            Report.AddSharedContent("company_info", sharedReport);

            Report report = new Report(Json.Read("report\\example_mergecontent.rrpt"));
            report.GlobalScope.Add("company_name", "株式会社ラピッドレポート");
            report.GlobalScope.Add("tel", "0000-11-2222");
            report.Fill(new ReportDataSource(getDataTable()));
            ReportPages pages = report.GetPages();

            // PDF出力
            using (FileStream fs = new FileStream("output\\example_mergecontent.pdf", FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                renderer.Setting.ReplaceBackslashToYen = true;
                pages.Render(renderer);
            }

            // XLS出力
            using (FileStream fs = new FileStream("output\\example_mergecontent.xls", FileMode.Create))
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                XlsRenderer renderer = new XlsRenderer(workbook);
                renderer.NewSheet("example_mergecontent");
                pages.Render(renderer);
                workbook.Write(fs);
            }

            // XLSX出力
            using (FileStream fs = new FileStream("output\\example_mergecontent.xlsx", FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                renderer.NewSheet("example_mergecontent");
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
            ret.Columns.Add("mitsumoriNo", typeof(Decimal));
            ret.Columns.Add("mitsumoriDate", typeof(DateTime));
            ret.Columns.Add("tanto", typeof(String));
            ret.Columns.Add("tokuisaki1", typeof(String));
            ret.Columns.Add("tokuisaki2", typeof(String));
            ret.Columns.Add("hinmei", typeof(String));
            ret.Columns.Add("irisu", typeof(Decimal));
            ret.Columns.Add("hakosu", typeof(Decimal));
            ret.Columns.Add("tani", typeof(String));
            ret.Columns.Add("tanka", typeof(Decimal));
            ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", null),
                "営業一部 佐藤太郎", "株式会社 岩手商事", "北上支店",
                "ノートパソコン", 1, 10, "台", 70000);
            ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", null),
                "営業一部 佐藤太郎", "株式会社 岩手商事", "北上支店",
                "モニター", 1, 10, "台", 20000);
            ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", null),
                "営業一部 佐藤太郎", "株式会社 岩手商事", "北上支店",
                "プリンタ", 1, 2, "台", 25000);
            ret.Rows.Add(101, DateTime.ParseExact("2013/03/01", "yyyy/MM/dd", null),
                "営業一部 佐藤太郎", "株式会社 岩手商事", "北上支店",
                "トナーカートリッジ", 2, 2, "本", 5000);
            return ret;
        }

    }
}
