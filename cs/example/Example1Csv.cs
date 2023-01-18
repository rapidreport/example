using System.IO;
using System.Text;

using jp.co.systembase.NPOI.HSSF.UserModel;
using jp.co.systembase.NPOI.XSSF.UserModel;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.renderer.gdi;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xls;
using jp.co.systembase.report.renderer.xlsx;

// 機能サンプル CSVデータソース
namespace example
{
    class Example1Csv
    {

        public static void Run()
        {
            // 帳票定義ファイルを読み込みます
            Report report = new Report(Json.Read("report/example1.rrpt"));

            // CSVファイルから帳票にデータを渡します
            using (StreamReader r = new StreamReader("report/data.csv", Encoding.GetEncoding("shift-jis")))
            {
                report.Fill(new CsvDataSource(r));
            }


            // ページ分割を行います
            ReportPages pages = report.GetPages();

            // PDF出力
            using (FileStream fs = new FileStream("output/example1csv.pdf", FileMode.Create))
            {
                PdfRenderer renderer = new PdfRenderer(fs);
                // バックスラッシュ文字を円マーク文字に変換します
                renderer.Setting.ReplaceBackslashToYen = true;
                pages.Render(renderer);
            }

            // XLS出力
            using (FileStream fs = new FileStream("output/example1csv.xls", FileMode.Create))
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                XlsRenderer renderer = new XlsRenderer(workbook);
                // Renderメソッドを呼ぶ前に必ずNewSheetメソッドを呼んでワークシートを作成します
                renderer.NewSheet("見積書");
                pages.Render(renderer);
                workbook.Write(fs);
            }

            // XLSX出力
            using (FileStream fs = new FileStream("output/example1csv.xlsx", FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                // Renderメソッドを呼ぶ前に必ずNewSheetメソッドを呼んでワークシートを作成します
                renderer.NewSheet("見積書");
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

    }
}
