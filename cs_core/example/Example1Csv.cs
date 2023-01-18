using System.IO;
using System.Text;

using jp.co.systembase.NPOI.XSSF.UserModel;

using jp.co.systembase.json;
using jp.co.systembase.report;
using jp.co.systembase.report.renderer.pdf;
using jp.co.systembase.report.renderer.xlsx;

// 基本サンプル1 見積書 (CSVデータソース)
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

            // Excel(XLSX)出力
            using (FileStream fs = new FileStream("output/example1csv.xlsx", FileMode.Create))
            {
                XSSFWorkbook workbook = new XSSFWorkbook();
                XlsxRenderer renderer = new XlsxRenderer(workbook);
                // Renderメソッドを呼ぶ前に必ずNewSheetメソッドを呼んでワークシートを作成します
                renderer.NewSheet("見積書");
                pages.Render(renderer);
                workbook.Write(fs);
            }
        }

    }
}
