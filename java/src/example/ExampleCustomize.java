package example;

import java.io.FileOutputStream;

import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;

import jp.co.systembase.core.DataTable;
import jp.co.systembase.report.Report;
import jp.co.systembase.report.ReportPages;
import jp.co.systembase.report.data.ReportDataSource;
import jp.co.systembase.report.renderer.pdf.PdfRenderer;
import jp.co.systembase.report.renderer.xls.XlsRenderer;
import jp.co.systembase.report.renderer.xlsx.XlsxRenderer;

// 機能サンプル 動的要素(デザイナのみ)
public class ExampleCustomize {

	public static void main(String[] args) throws Throwable {

		// カスタマイザを指定せずに、Reportオブジェクトを生成します
		Report report = new Report(ReadUtil.readJson("report/example_customize.rrpt"));
		report.fill(new ReportDataSource(getDataTable()));
		ReportPages pages = report.getPages();

		// PDF出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_customize.pdf");
			try{
				pages.render(new PdfRenderer(fos));
			}finally{
				fos.close();
			}
		}

		// XLS出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_customize.xls");
			try{
				HSSFWorkbook workBook = new HSSFWorkbook();
				XlsRenderer renderer = new XlsRenderer(workBook);
				renderer.newSheet("example_render");
				pages.render(renderer);
				workBook.write(fos);
			}finally{
				fos.close();
			}
		}

		// XLSX出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_customize.xlsx");
			try{
				XSSFWorkbook workBook = new XSSFWorkbook();
				XlsxRenderer renderer = new XlsxRenderer(workBook);
				renderer.newSheet("example_render");
				pages.render(renderer);
				workBook.write(fos);
			}finally{
				fos.close();
			}
		}
	}

	private static DataTable getDataTable(){
		DataTable ret = new DataTable();
		ret.setFieldNames("NUM");
		ret.addRecord().puts(50);
		ret.addRecord().puts(40);
		ret.addRecord().puts(30);
		ret.addRecord().puts(20);
		ret.addRecord().puts(10);
		ret.addRecord().puts(0);
		ret.addRecord().puts(-10);
		ret.addRecord().puts(-20);
		ret.addRecord().puts(-30);
		ret.addRecord().puts(-40);
		ret.addRecord().puts(-50);
		return ret;
	}

}
