package example;

import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;

import jp.co.systembase.report.Report;
import jp.co.systembase.report.ReportPages;
import jp.co.systembase.report.renderer.pdf.PdfRenderer;
import jp.co.systembase.report.renderer.xls.XlsRenderer;
import jp.co.systembase.report.renderer.xlsx.XlsxRenderer;

import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;

// 基本サンプル1 見積書 (CSVデータソース)
public class Example1Csv {

	public static void main(String[] args) throws Exception {

		// 帳票定義を読込みます
		Report report = new Report(ReadUtil.readJson("report/example1.rrpt"));

		// CSVファイルから帳票にデータを渡します
		InputStream is = new FileInputStream("report/data.csv");
		try{
			Reader r = new InputStreamReader(is, "shift-jis");
			try{
				report.fill(new CsvDataSource(r));
			}finally{
				r.close();
			}
		}finally{
			is.close();
		}

		// ページ分割を行います
		ReportPages pages = report.getPages();

		// PDF出力
		{
			FileOutputStream fos = new FileOutputStream("output/example1csv.pdf");
			try {
				PdfRenderer renderer = new PdfRenderer(fos);
				// バックスラッシュ文字を円マーク文字に変換します
				renderer.setting.replaceBackslashToYen = true;
				pages.render(renderer);
			} finally {
				fos.close();
			}
		}

		// XLS出力
		{
			FileOutputStream fos = new FileOutputStream("output/example1csv.xls");
			try {
				HSSFWorkbook workBook = new HSSFWorkbook();
				XlsRenderer renderer = new XlsRenderer(workBook);
				// Renderメソッドを呼ぶ前に必ずNewSheetメソッドを呼んでワークシートを作成します
				renderer.newSheet("見積書");
				pages.render(renderer);
				workBook.write(fos);
			} finally {
				fos.close();
			}
		}

		// XLSX出力
		{
			FileOutputStream fos = new FileOutputStream("output/example1csv.xlsx");
			try {
				XSSFWorkbook workBook = new XSSFWorkbook();
				XlsxRenderer renderer = new XlsxRenderer(workBook);
				// Renderメソッドを呼ぶ前に必ずNewSheetメソッドを呼んでワークシートを作成します
				renderer.newSheet("見積書");
				pages.render(renderer);
				workBook.write(fos);
			} finally {
				fos.close();
			}
		}
	}


}

