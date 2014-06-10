package example;

import java.io.FileOutputStream;

import jp.co.systembase.core.DataTable;
import jp.co.systembase.report.Report;
import jp.co.systembase.report.ReportPages;
import jp.co.systembase.report.data.ReportDataSource;
import jp.co.systembase.report.renderer.pdf.PdfRenderer;
import jp.co.systembase.report.renderer.xls.XlsRenderer;
import jp.co.systembase.report.renderer.xlsx.XlsxRenderer;

import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;

// 機能サンプル 絶対座標による配置
public class ExampleLocate {

	public static void main(String[] args) throws Exception {

		Report report = new Report(ReadUtil.readJson("report/example_locate.rrpt"));
		report.fill(new ReportDataSource(getDataTable()));
		ReportPages pages = report.getPages();

		// PDF出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_locate.pdf");
			try {
				pages.render(new PdfRenderer(fos));
			} finally {
				fos.close();
			}
		}

		// XLS出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_locate.xls");
			try {
				HSSFWorkbook workBook = new HSSFWorkbook();
				XlsRenderer renderer = new XlsRenderer(workBook);
				renderer.newSheet("example_locate");
				pages.render(renderer);
				workBook.write(fos);
			} finally {
				fos.close();
			}
		}

		// XLSX出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_locate.xlsx");
			try {
				XSSFWorkbook workBook = new XSSFWorkbook();
				XlsxRenderer renderer = new XlsxRenderer(workBook);
				renderer.newSheet("example_locate");
				pages.render(renderer);
				workBook.write(fos);
			} finally {
				fos.close();
			}
		}
	}

	private static DataTable getDataTable() throws Exception {
		DataTable ret = new DataTable();
		ret.setFieldNames("yubin", "jusho", "tokuisaki1", "tokuisaki2", "tanto");
		ret.addRecord().puts("556-0005", "大阪府大阪市浪速区日本橋", "大阪販売　株式会社", "大阪支店", "菊池　誠　様");
		ret.addRecord().puts("980-0000", "宮城県仙台市青葉区", "宮城事務機販売　株式会社", null, "中沢　雅彦　様");
		ret.addRecord().puts("420-0001", "静岡県静岡市葵区井宮町", "静岡電機　株式会社", "静岡支店", "川田　敬　様");
		ret.addRecord().puts("001-0000", "北海道札幌市北区大通西", "北海道産業　株式会社", null, "堀内　繁　様");
		ret.addRecord().puts("220-0023", "神奈川県横浜市西区平沼", "株式会社　神奈川事務機販売", null, "村田　英二　様");
		ret.addRecord().puts("460-0000", "愛知県名古屋市中区", "愛知電機　株式会社", null, "高橋　雅氏　様");
		ret.addRecord().puts("310-0001", "茨城県水戸市三の丸", "株式会社　茨城販売", null, "佐藤　健二　様");
		ret.addRecord().puts("460-0008", "愛知県名古屋市中区栄", "愛知産業　株式会社", "愛知支店", "小野　修一　様");
		ret.addRecord().puts("024-0073", "岩手県北上市下江釣子", "岩手販売　株式会社", null, "田中　琢磨　様");
		ret.addRecord().puts("920-0961", "石川県金沢市香林坊", "石川事務機販売　株式会社", "石川支店", "厚木　渡　様");
		ret.addRecord().puts("330-0845", "埼玉県大宮市仲町", "埼玉電機　株式会社", null, "柳田　元　様");
		ret.addRecord().puts("260-0016", "千葉県千葉市中央区栄町", "株式会社　千葉電機", null, "橘　孝彦　様");
		return ret;
	}

}
