package example;

import java.io.FileOutputStream;

import org.apache.poi.hssf.usermodel.HSSFWorkbook;

import jp.co.systembase.core.Cast;
import jp.co.systembase.core.DataTable;
import jp.co.systembase.report.Report;
import jp.co.systembase.report.ReportPages;
import jp.co.systembase.report.component.Content;
import jp.co.systembase.report.component.Evaluator;
import jp.co.systembase.report.component.Region;
import jp.co.systembase.report.customizer.DefaultCustomizer;
import jp.co.systembase.report.data.ReportDataSource;
import jp.co.systembase.report.renderer.pdf.PdfRenderer;
import jp.co.systembase.report.renderer.xls.XlsRenderer;

// 機能サンプル Contentサイズ変更
public class ExampleRegion {

	public static void main(String[] args) throws Throwable {

		// 第2引数にCustomizerオブジェクトを渡します
		Report report = new Report(ReadUtil.readJson("report/example_region.rrpt"), new Customizer());
		report.fill(new ReportDataSource(getDataTable()));
		ReportPages pages = report.getPages();

		// PDF出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_region.pdf");
			try{
				pages.render(new PdfRenderer(fos));
			}finally{
				fos.close();
			}
		}

		// XLS出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_region.xls");
			try{
				HSSFWorkbook workBook = new HSSFWorkbook();
				XlsRenderer renderer = new XlsRenderer(workBook);
				renderer.newSheet("example_region");
				pages.render(renderer);
				workBook.write(fos);
			}finally{
				fos.close();
			}
		}
	}

	// コンテントのサイズを動的に変更するカスタマイザ
	private static class Customizer extends DefaultCustomizer{
		@Override
		public Region contentRegion(
				Content content,
				Evaluator evaluator,
				Region region) {
			// "content_example"という識別子を持ったコンテントに対して処理を行います
			if ("content_example".equals(content.design.id)){
				// regionはコンテントの表示領域を表します
				// ".HEIGHT"という式を評価することでHEIGHT列の値を取得し、
				// コンテントの高さを設定します
				float height = Cast.toFloat(evaluator.evalTry(".HEIGHT"));
				Region ret = new Region(region); // regionのクローンを作成します
				ret.setHeight(height);
				return ret;
			}else{
				return region;
			}
		}
	}

	private static DataTable getDataTable(){
		DataTable ret = new DataTable();
		ret.setFieldNames("HEIGHT");
		ret.addRecord().puts(20);
		ret.addRecord().puts(30);
		ret.addRecord().puts(40);
		ret.addRecord().puts(50);
		ret.addRecord().puts(60);
		ret.addRecord().puts(70);
		ret.addRecord().puts(80);
		ret.addRecord().puts(90);
		ret.addRecord().puts(100);
		return ret;
	}

}
