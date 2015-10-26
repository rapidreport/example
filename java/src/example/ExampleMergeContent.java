package example;

import java.io.FileOutputStream;
import java.text.SimpleDateFormat;

import jp.co.systembase.core.DataTable;
import jp.co.systembase.report.Report;
import jp.co.systembase.report.ReportDesign;
import jp.co.systembase.report.ReportPages;
import jp.co.systembase.report.data.ReportDataSource;
import jp.co.systembase.report.renderer.pdf.PdfRenderer;
import jp.co.systembase.report.renderer.xls.XlsRenderer;
import jp.co.systembase.report.renderer.xlsx.XlsxRenderer;

import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;

// 機能サンプル 差込コンテント
public class ExampleMergeContent {

	public static void main(String[] args) throws Throwable {
		
		// 差込みを行うコンテントを、あらかじめ共有コンテントへ登録しておきます
		ReportDesign sharedReport = new ReportDesign(ReadUtil.readJson("report/example_shared.rrpt"));
		Report.addSharedContent("company_info", sharedReport);
		
		Report report = new Report(ReadUtil.readJson("report/example_mergecontent.rrpt"));
		report.globalScope.put("company_name", "株式会社ラピッドレポート");
		report.globalScope.put("tel", "0000-11-2222");
		report.fill(new ReportDataSource(getDataTable()));
		
		// PDF出力
		ReportPages pages = report.getPages();
		{
			FileOutputStream fos = new FileOutputStream("output/example_mergecontent.pdf");
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
			FileOutputStream fos = new FileOutputStream("output/example_mergecontent.xls");
			try{
				HSSFWorkbook workBook = new HSSFWorkbook();
				XlsRenderer renderer = new XlsRenderer(workBook);
				renderer.newSheet("example_mergecontent");
				pages.render(renderer);
				workBook.write(fos);
			}finally{
				fos.close();
			}
		}
		
		// XLSX出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_mergecontent.xlsx");
			try{
				XSSFWorkbook workBook = new XSSFWorkbook();
				XlsxRenderer renderer = new XlsxRenderer(workBook);
				renderer.newSheet("example_mergecontent");
				pages.render(renderer);
				workBook.write(fos);
			}finally{
				fos.close();
			}
		}
	}
	
	private static DataTable getDataTable() throws Exception {
		DataTable ret = new DataTable();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd");
		ret.setFieldNames("mitsumoriNo", "mitsumoriDate", "tanto",
				"tokuisaki1", "tokuisaki2",
				"hinmei", "irisu", "hakosu", "tani", "tanka");
		ret.addRecord().puts(101, sdf.parse("2013/03/01"),
				"営業一部 佐藤太郎", "株式会社 岩手商事", "北上支社",
				"ノートパソコン", 1, 10, "台", 70000);
		ret.addRecord().puts(101, sdf.parse("2013/03/01"),
				"営業一部 佐藤太郎", "株式会社 岩手商事", "北上支社",
				"モニター", 1, 10, "台", 20000);
		ret.addRecord().puts(101, sdf.parse("2013/03/01"),
				"営業一部 佐藤太郎", "株式会社 岩手商事", "北上支社",
				"プリンタ", 1, 2, "台", 25000);
		ret.addRecord().puts(101, sdf.parse("2013/03/01"),
				"営業一部 佐藤太郎", "株式会社 岩手商事", "北上支社",
				"トナーカートリッジ", 2, 2, "本", 5000);
		return ret;
	}

}
