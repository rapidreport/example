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

// 機能サンプル クロス集計表
public class ExampleCrosstab {

	public static void main(String[] args) throws Exception {

		Report report = new Report(ReadUtil.readJson("report/example_crosstab.rrpt"));
		
		// 横方向の列データを設定します。
		report.addCrosstabCaptionDataSource("crosstab_example", new ReportDataSource(getCaptionDataTable()));
		
		report.fill(new ReportDataSource(getDataTable()));
		ReportPages pages = report.getPages();

		// PDF出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_crosstab.pdf");
			try {
				pages.render(new PdfRenderer(fos));
			} finally {
				fos.close();
			}
		}

		// XLS出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_crosstab.xls");
			try {
				HSSFWorkbook workBook = new HSSFWorkbook();
				XlsRenderer renderer = new XlsRenderer(workBook);
				renderer.newSheet("example_crosstab");
				pages.render(renderer);
				workBook.write(fos);
			} finally {
				fos.close();
			}
		}

		// XLSX出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_crosstab.xlsx");
			try {
				XSSFWorkbook workBook = new XSSFWorkbook();
				XlsxRenderer renderer = new XlsxRenderer(workBook);
				renderer.newSheet("example_crosstab");
				pages.render(renderer);
				workBook.write(fos);
			} finally {
				fos.close();
			}
		}
	}

	private static DataTable getCaptionDataTable(){
		DataTable ret = new DataTable();
		ret.setFieldNames("period_cd", "period_nm");
		ret.addRecord().puts(1, "2010年上期");
		ret.addRecord().puts(2, "2010年下期");
		ret.addRecord().puts(3, "2011年上期");
		ret.addRecord().puts(4, "2011年下期");
		ret.addRecord().puts(5, "2012年上期");
		ret.addRecord().puts(6, "2012年下期");
		ret.addRecord().puts(7, "2013年上期");
		ret.addRecord().puts(8, "2013年下期");
		ret.addRecord().puts(9, "2014年上期");
		ret.addRecord().puts(10, "2014年下期");
		ret.addRecord().puts(11, "2015年上期");
		ret.addRecord().puts(12, "2015年下期");
		ret.addRecord().puts(13, "2016年上期");
		ret.addRecord().puts(14, "2016年下期");
		return ret;
	}

	private static DataTable getDataTable(){
		DataTable ret = new DataTable();
		String branchNms[] = 
				{"北上本社", "東京支社", "盛岡営業所", "秋田営業所",
				 "仙台営業所", "山形営業所", "福島営業所"};				
		ret.setFieldNames("branch_cd", "branch_nm", "period_cd", "amount");
		for(int i = 0;i < 14;i++){
			for(int j = 0;j < 7;j++){
				ret.addRecord().puts(j + 1, branchNms[j], i + 1, 10000 + i * 100 + j * 10);
			}
		}
		return ret;
	}

//	// 横の見出し列値も含めたデータ。
//	// こちらを利用する場合は、CaptionDataを設定する必要はありません。
//	private static DataTable getDataTable(){
//		DataTable ret = new DataTable();
//		String branchNms[] = 
//				{"北上本社", "東京支社", "盛岡営業所", "秋田営業所",
//				 "仙台営業所", "山形営業所", "福島営業所"};
//		String periodNms[] = 
//			{"2010年上期", "2010年下期", "2011年上期", "2011年下期",
//			  "2012年上期", "2012年下期", "2013年上期", "2013年下期",
//			  "2014年上期", "2014年下期", "2015年上期", "2015年下期",
//			  "2016年上期", "2016年下期"};
//		ret.setFieldNames("branch_cd", "branch_nm", "period_cd", "period_nm", "amount");
//		for(int i = 0;i < 14;i++){
//			for(int j = 0;j < 7;j++){
//				ret.addRecord().puts(j + 1, branchNms[j], i + 1, periodNms[i], 10000 + i * 100 + j * 10);
//			}
//		}
//		return ret;
//	}
	
}
