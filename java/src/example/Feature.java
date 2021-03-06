package example;

import java.io.FileOutputStream;

import jp.co.systembase.core.DataTable;
import jp.co.systembase.report.Report;
import jp.co.systembase.report.ReportPages;
import jp.co.systembase.report.data.DummyDataSource;
import jp.co.systembase.report.data.GroupDataProvider;
import jp.co.systembase.report.data.ReportDataSource;
import jp.co.systembase.report.renderer.pdf.PdfRenderer;
import jp.co.systembase.report.renderer.xls.XlsRenderer;
import jp.co.systembase.report.renderer.xlsx.XlsxRenderer;

import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;

// 「特徴と機能一覧」ページに掲載したサンプル
public class Feature {

	public static void main(String[] args) throws Throwable {

		Report report = new Report(ReadUtil.readJson("report/feature.rrpt"));

		// "feature1-4"に、getDataTable1-4をそれぞれ割り当てます
		GroupDataProvider dataProvider = new GroupDataProvider();
		dataProvider.groupDataMap.put("feature1", new ReportDataSource(getDataTable1()));
		dataProvider.groupDataMap.put("feature2", new ReportDataSource(getDataTable2()));
		dataProvider.groupDataMap.put("feature3", new ReportDataSource(getDataTable3()));
		dataProvider.groupDataMap.put("feature4", new ReportDataSource(getDataTable4()));

		// 第2引数にdataProviderを渡します
		report.fill(DummyDataSource.getInstance(), dataProvider);
		ReportPages pages = report.getPages();

		// PDF出力
		{
			FileOutputStream fos = new FileOutputStream("output/feature.pdf");
			try{
				PdfRenderer renderer = new PdfRenderer(fos);
				renderer.setting.replaceBackslashToYen = true;
				pages.render(renderer);
			}finally{
				fos.close();
			}
		}

		// XLS出力
		{
			FileOutputStream fos = new FileOutputStream("output/feature.xls");
			try{
				HSSFWorkbook workBook = new HSSFWorkbook();
				XlsRenderer renderer = new XlsRenderer(workBook);
				renderer.newSheet("feature");
				pages.render(renderer);
				workBook.write(fos);
			}finally{
				fos.close();
			}
		}

		// XLSX出力
		{
			FileOutputStream fos = new FileOutputStream("output/feature.xlsx");
			try{
				XSSFWorkbook workBook = new XSSFWorkbook();
				XlsxRenderer renderer = new XlsxRenderer(workBook);
				renderer.newSheet("feature");
				pages.render(renderer);
				workBook.write(fos);
			}finally{
				fos.close();
			}
		}
	}

	private static DataTable getDataTable1()
    {
        DataTable ret = new DataTable();
        ret.setFieldNames("KUBUN1", "KUBUN2", "ZAISHITSU", "TANI", "TANKA", "SURYO");
        ret.addRecord().puts("内装", "床", "フローリング", "㎡", 7250, 20);
        ret.addRecord().puts("内装", "床", "畳", "枚", 4500, 12);
        ret.addRecord().puts("内装", "壁", "壁紙", "㎡", 1000, 30);
        ret.addRecord().puts("外装", "屋根", "瓦", "枚", 13000, 40);
        ret.addRecord().puts("外装", "屋根", "トタン", "㎡", 3920, 60);
        ret.addRecord().puts("外装", "屋根", "化粧スレート", "㎡", 5000, 45);
        return ret;
    }

    private static DataTable getDataTable2()
    {
        DataTable ret = new DataTable();
        ret.setFieldNames("REGION", "PREF");
        ret.addRecord().puts("北海道", "北海道");
        ret.addRecord().puts("東北", "青森");
        ret.addRecord().puts("東北", "岩手");
        ret.addRecord().puts("東北", "秋田");
        ret.addRecord().puts("東北", "宮城");
        ret.addRecord().puts("東北", "山形");
        ret.addRecord().puts("東北", "福島");
        ret.addRecord().puts("関東", "茨城");
        ret.addRecord().puts("関東", "栃木");
        ret.addRecord().puts("関東", "群馬");
        ret.addRecord().puts("関東", "埼玉");
        ret.addRecord().puts("関東", "ちば");
        ret.addRecord().puts("関東", "東京");
        ret.addRecord().puts("関東", "神奈川");
        return ret;
    }

    private static DataTable getDataTable3()
    {
        DataTable ret = new DataTable();
        ret.setFieldNames("WRAP", "SHRINK", "FIXDEC");
        ret.addRecord().puts("RapidRport", "ラピッドレポート", 12345);
        ret.addRecord().puts("RapidReport 帳票ツール", "ラピッドレポート　帳票ツール", 1234.1);
        ret.addRecord().puts("開発者のための帳票ツール", "開発者のための帳票ツール", 123.12);
        ret.addRecord().puts("(株)システムベース", "(株)システムベース", 12.123);
        return ret;
    }

    private static DataTable getDataTable4()
    {
        DataTable ret = new DataTable();
        ret.setFieldNames("SHOHIN_CD", "SHOHIN_NM", "SURYO", "TANI", "TANKA");
        ret.addRecord().puts("SH-A0011", "冷凍コロッケ", 25, "袋", 35);
        ret.addRecord().puts("SH-A0012", "冷凍ピザ", 8, "袋", 410);
        ret.addRecord().puts("SH-B1005", "カップラーメン醤油味", 40, "個", 90);
        return ret;
    }
    
}
