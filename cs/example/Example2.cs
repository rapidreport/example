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

// チュートリアル2 売上明細表
namespace example
{
    class Example2
    {
        public static void Run()
        {
            // 帳票定義ファイルを読み込みます
            Report report = new Report(Json.Read("report/example2.rrpt"));

            // GlobalScopeに値を登録します
            report.GlobalScope.Add("startDate", DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", null));
            report.GlobalScope.Add("endDate", DateTime.ParseExact("2013/02/28", "yyyy/MM/dd", null));
            report.GlobalScope.Add("printDate", DateTime.Today);
            report.GlobalScope.Add("kaisha", "株式会社　システムベース");

            // 帳票にデータを渡します
            report.Fill(new ReportDataSource(getDataTable()));

            // ページ分割を行います
            ReportPages pages = report.GetPages();

            // PDF出力
            using (FileStream fs = new FileStream("output/example2.pdf", FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                // バックスラッシュ文字を円マーク文字に変換します
                renderer.Setting.ReplaceBackslashToYen = true;
                pages.Render(renderer);
            }

            // XLS出力
            using (FileStream fs = new FileStream("output/example2.xls", FileMode.Create))
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                XlsRenderer renderer = new XlsRenderer(workbook);
                // Renderメソッドを呼ぶ前に必ずNewSheetメソッドを呼んでワークシートを作成します
                renderer.NewSheet("売上明細表");
                pages.Render(renderer);
                workbook.Write(fs);
            }

            // XLSX出力
            using (FileStream fs = new FileStream("output/example2.xlsx", FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                // Renderメソッドを呼ぶ前に必ずNewSheetメソッドを呼んでワークシートを作成します
                renderer.NewSheet("売上明細表");
                pages.Render(renderer);
                workbook.Write(fs);
            }

            // 直接印刷、プレビュー画面表示
            {
                Printer printer = new Printer(pages);
                //// 直接印刷
                //// ダイアログを出して印刷します
                //if (printer.PrintDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    printer.PrintDocument.Print();
                //}

                //// ダイアログを出さずに印刷します
                //printer.PrintDocument.Print();

                // プレビュー画面表示
                FmPrintPreview preview = new FmPrintPreview(printer);
                // プレビュー画面が開かれた時点で表示倍率を現在のウィンドウサイズに合わせます
                preview.StartUpZoomFit = true;
                preview.ShowDialog();
            }
        }

        private static DataTable getDataTable()
        {
            DataTable ret = new DataTable();
            ret.Columns.Add("bumonCd", typeof(Decimal));
            ret.Columns.Add("bumon", typeof(String));
            ret.Columns.Add("uriageDate", typeof(DateTime));
            ret.Columns.Add("denpyoNo", typeof(Decimal));
            ret.Columns.Add("shohinCd", typeof(String));
            ret.Columns.Add("shohin", typeof(String));
            ret.Columns.Add("tanka", typeof(Decimal));
            ret.Columns.Add("suryo", typeof(Decimal));
            ret.Rows.Add(1, "第一営業部",
                DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", null),
                1246, "PC00001", "ノートパソコン", 70000, 10);
            ret.Rows.Add(1, "第一営業部",
                DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", null), 
                1246, "DP00002", "モニター", 25000, 10);
            ret.Rows.Add(1, "第一営業部",
                DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", null),
                1246, "PR00003", "プリンタ", 20000, 2);
            ret.Rows.Add(1, "第一営業部",
                DateTime.ParseExact("2013/02/10", "yyyy/MM/dd", null),
                1248, "PR00003", "プリンタ", 20000, 3);
            ret.Rows.Add(2, "第二営業部",
                DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", null),
                1247, "PC00001", "ノートパソコン", 70000, 5);
            ret.Rows.Add(2, "第二営業部",
                DateTime.ParseExact("2013/02/01", "yyyy/MM/dd", null),
                1247, "DP00002", "モニター", 25000, 10);
            return ret;
        }

    }
}
