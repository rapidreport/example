package example;

import java.io.FileOutputStream;

import org.apache.poi.hssf.usermodel.HSSFWorkbook;

import jp.co.systembase.core.Cast;
import jp.co.systembase.core.DataTable;
import jp.co.systembase.report.Report;
import jp.co.systembase.report.ReportPages;
import jp.co.systembase.report.component.Content;
import jp.co.systembase.report.component.ElementDesign;
import jp.co.systembase.report.component.ElementDesigns;
import jp.co.systembase.report.component.Evaluator;
import jp.co.systembase.report.component.Region;
import jp.co.systembase.report.customizer.DefaultCustomizer;
import jp.co.systembase.report.data.ReportDataSource;
import jp.co.systembase.report.renderer.pdf.PdfRenderer;
import jp.co.systembase.report.renderer.xls.XlsRenderer;

// 機能サンプル 動的要素
public class ExampleRender {

	public static void main(String[] args) throws Throwable {

		// 第2引数にCustomizerオブジェクトを渡します
		Report report = new Report(ReadUtil.readJson("report/example_render.rrpt"), new Customizer());
		report.fill(new ReportDataSource(getDataTable()));
		ReportPages pages = report.getPages();

		// PDF出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_render.pdf");
			try{
				pages.render(new PdfRenderer(fos));
			}finally{
				fos.close();
			}
		}

		// XLS出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_render.xls");
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
	}

	// 要素に動的な修正を加えるカスタマイザ
	private static class Customizer extends DefaultCustomizer{
		public void renderContent(
				Content content,
				Evaluator evaluator,
				Region region,
				ElementDesigns elementDesigns) {
			// このメソッドはコンテントの描画が行われる直前に呼ばれます
			// "content_example"という識別子を持ったコンテントに対して処理を行います
			if ("content_example".equals(content.design.id)){
				// "graph"という識別子を持った要素を取得し、レイアウトと色を修正します
				ElementDesign e = elementDesigns.find("graph");
				// ".NUM"という式を評価することで、NUM列の値を得ます
				float num = Cast.toFloat(evaluator.evalTry(".NUM"));
				if (num >= 0){
					e.child("layout").put("x1", 100);
					e.child("layout").put("x2", 100 + num);
					e.put("fill_color", "lightblue");
				}else{
					e.child("layout").put("x1", 100 + num);
					e.child("layout").put("x2", 100);
					e.put("fill_color", "pink");
				}
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
