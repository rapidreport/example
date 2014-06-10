package example;

import java.io.FileOutputStream;
import java.text.SimpleDateFormat;
import java.util.Date;

import jp.co.systembase.core.DataTable;
import jp.co.systembase.report.Report;
import jp.co.systembase.report.ReportPages;
import jp.co.systembase.report.data.ReportDataSource;
import jp.co.systembase.report.renderer.pdf.PdfRenderer;

// 基本サンプル2 売上明細表（PDF1000ページ）
public class Example2Huge {

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

		// PDF出力の実行時間を計測します
		double t = System.currentTimeMillis();

		// ページ分割を行います
		ReportPages pages = report.getPages();

		// PDF出力
		{
			FileOutputStream fos = new FileOutputStream("output/example2_huge.pdf");
			try {
				PdfRenderer renderer = new PdfRenderer(fos);
				pages.render(renderer);
			} finally {
				fos.close();
			}
		}

		// 計測結果を表示します
		System.out.println("実行時間は " + (System.currentTimeMillis() - t) + " ミリ秒です");
	}

	private static DataTable getDataTable() throws Exception {
		DataTable ret = new DataTable();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd");
		ret.setFieldNames("bumonCd", "bumon", "uriageDate", "denpyoNo",
				"shohinCd", "shohin", "tanka", "suryo");

		// 1000ページ分のデータを作成します
		for (int i = 1; i <= 100; i++) {
			for (int j = 1; j <= 50; j++) {
				ret.addRecord().puts(i, "部門" + i, sdf.parse("2013/02/01"), j,
						"PC00001", "ノートパソコン", 70000, 10);
				ret.addRecord().puts(i, "部門" + i, sdf.parse("2013/02/01"), j,
						"DP00002", "モニター", 25000, 10);
				ret.addRecord().puts(i,  "部門" + i, sdf.parse("2013/02/01"), j,
						"PR00003", "プリンタ", 20000, 2);
				ret.addRecord().puts(i, "部門" + i, sdf.parse("2013/02/10"), j,
						"PR00003", "プリンタ", 20000, 3);
			}
		}
		return ret;
	}

}
