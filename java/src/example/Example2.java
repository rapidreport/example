package example;

import java.io.FileOutputStream;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;

import jp.co.systembase.core.DataTable;
import jp.co.systembase.report.Report;
import jp.co.systembase.report.ReportPages;
import jp.co.systembase.report.data.ReportDataSource;
import jp.co.systembase.report.renderer.pdf.PdfRenderer;
import jp.co.systembase.report.renderer.xls.XlsRenderer;
import jp.co.systembase.report.renderer.xlsx.XlsxRenderer;

// チュートリアル2 売上明細表
public class Example2 {

	public static void main(String[] args) throws Exception {

		// 帳票定義を読込みます
		Report report = new Report(ReadUtil.readJson("report/example2.rrpt"));

		// globalScopeに値を登録します
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd");
		report.globalScope.put("startDate", sdf.parse("2013/02/01"));
		report.globalScope.put("endDate", sdf.parse("2013/02/28"));
		report.globalScope.put("printDate", new Date());
		report.globalScope.put("kaisha", "株式会社　システムベース");

		// 帳票にデータを渡します
		report.fill(new ReportDataSource(getDataTable()));

		// ページ分割を行います
		ReportPages pages = report.getPages();

		// PDF出力
		{
			FileOutputStream fos = new FileOutputStream("output/example2.pdf");
			try {
				PdfRenderer renderer = new PdfRenderer(fos);
				pages.render(renderer);
			} finally {
				fos.close();
			}
		}

		// XLS出力
		{
			FileOutputStream fos = new FileOutputStream("output/example2.xls");
			try {
				HSSFWorkbook workBook = new HSSFWorkbook();
				XlsRenderer renderer = new XlsRenderer(workBook);
				// Renderメソッドを呼ぶ前に必ずNewSheetメソッドを呼んでワークシートを作成します
				renderer.newSheet("売上明細表");
				pages.render(renderer);
				workBook.write(fos);
			} finally {
				fos.close();
			}
		}

		// XLSX出力
		{
			FileOutputStream fos = new FileOutputStream("output/example2.xlsx");
			try {
				XSSFWorkbook workBook = new XSSFWorkbook();
				XlsxRenderer renderer = new XlsxRenderer(workBook);
				// Renderメソッドを呼ぶ前に必ずNewSheetメソッドを呼んでワークシートを作成します
				renderer.newSheet("売上明細表");
				pages.render(renderer);
				workBook.write(fos);
			} finally {
				fos.close();
			}
		}
	}

	private static DataTable getDataTable() throws Exception {
		DataTable ret = new DataTable();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd");
		ret.setFieldNames("bumonCd", "bumon", "uriageDate", "denpyoNo",
				"shohinCd", "shohin", "tanka", "suryo");
		ret.addRecord().puts(1, "第一営業部", sdf.parse("2013/02/01"), 1246,
				"PC00001", "ノートパソコン", 70000, 10);
		ret.addRecord().puts(1, "第一営業部", sdf.parse("2013/02/01"), 1246,
				"DP00002", "モニター", 25000, 10);
		ret.addRecord().puts(1, "第一営業部", sdf.parse("2013/02/01"), 1246,
				"PR00003", "プリンタ", 20000, 2);
		ret.addRecord().puts(1, "第一営業部", sdf.parse("2013/02/10"), 1248,
				"PR00003", "プリンタ", 20000, 3);
		ret.addRecord().puts(2, "第二営業部", sdf.parse("2013/02/01"), 1247,
				"PC00001", "ノートパソコン", 70000, 5);
		ret.addRecord().puts(2, "第二営業部", sdf.parse("2013/02/01"), 1247,
				"DP00002", "モニター", 25000, 10);
		return ret;
	}

}
