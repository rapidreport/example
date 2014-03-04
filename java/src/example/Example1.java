package example;

import java.io.FileOutputStream;
import java.text.SimpleDateFormat;

import jp.co.systembase.core.DataTable;
import jp.co.systembase.report.Report;
import jp.co.systembase.report.ReportPages;
import jp.co.systembase.report.data.ReportDataSource;
import jp.co.systembase.report.renderer.pdf.PdfRenderer;
import jp.co.systembase.report.renderer.xls.XlsRenderer;
import jp.co.systembase.report.renderer.xlsx.XlsxRenderer;

import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;

// 基本サンプル1 見積書
public class Example1 {

	public static void main(String[] args) throws Exception {

		// 帳票定義を読込みます
		Report report = new Report(ReadUtil.readJson("report\\example1.rrpt"));

		// 帳票にデータを渡します
		// ReportDataSourceにはResultSetまたはListを渡すことができます
		// Listの要素はMapまたはJavaBeansである必要があります
		// このサンプルにgetDataTableの実装を複数用意していますので、
		// いずれかのコメントアウトを外して試してみてください
		report.fill(new ReportDataSource(getDataTable()));

		// ページ分割を行います
		ReportPages pages = report.getPages();

		// PDF出力
		{
			FileOutputStream fos = new FileOutputStream("output\\example1.pdf");
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
			FileOutputStream fos = new FileOutputStream("output\\example1.xls");
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
			FileOutputStream fos = new FileOutputStream("output\\example1.xlsx");
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

	// DataTableを利用したサンプル
	private static DataTable getDataTable() throws Exception {
		// DataTableはList<Map>を拡張したクラスで
		// 列と行からなる表データを格納することができます
		DataTable ret = new DataTable();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd");
		ret.setFieldNames("mitsumoriNo", "mitsumoriDate",
				"tokuisaki1", "tokuisaki2",
				"hinmei", "irisu", "hakosu", "tani", "tanka");
		ret.addRecord().puts(101, sdf.parse("2013/03/01"),
				"株式会社 岩手商事", "北上支社",
				"ノートパソコン", 1, 10, "台", 70000);
		ret.addRecord().puts(101, sdf.parse("2013/03/01"),
				"株式会社 岩手商事", "北上支社",
				"モニター", 1, 10, "台", 20000);
		ret.addRecord().puts(101, sdf.parse("2013/03/01"),
				"株式会社 岩手商事", "北上支社",
				"プリンタ", 1, 2, "台", 25000);
		ret.addRecord().puts(101, sdf.parse("2013/03/01"),
				"株式会社 岩手商事", "北上支社",
				"トナーカートリッジ", 2, 2, "本", 5000);
		return ret;
	}

	/*
	// JavaBeans(getterメソッド)を利用したサンプル
	private static List<ExampleBean1> getDataTable() throws Exception {
		List<ExampleBean1> ret = new ArrayList<ExampleBean1>();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd");
		{
			ExampleBean1 r = new ExampleBean1();
			r.setTokuisaki1("株式会社 岩手商事");
			r.setTokuisaki2("北上支社");
			r.setMitsumoriNo(101);
			r.setMitsumoriDate(sdf.parse("2013/03/01"));
			r.setHinmei("ノートパソコン");
			r.setIrisu(1);
			r.setHakosu(10);
			r.setTani("台");
			r.setTanka(70000);
			ret.add(r);
		}
		{
			ExampleBean1 r = new ExampleBean1();
			r.setTokuisaki1("株式会社 岩手商事");
			r.setTokuisaki2("北上支社");
			r.setMitsumoriNo(101);
			r.setMitsumoriDate(sdf.parse("2013/03/01"));
			r.setHinmei("モニター");
			r.setIrisu(1);
			r.setHakosu(10);
			r.setTani("台");
			r.setTanka(20000);
			ret.add(r);
		}
		{
			ExampleBean1 r = new ExampleBean1();
			r.setTokuisaki1("株式会社 岩手商事");
			r.setTokuisaki2("北上支社");
			r.setMitsumoriNo(101);
			r.setMitsumoriDate(sdf.parse("2013/03/01"));
			r.setHinmei("プリンタ");
			r.setIrisu(1);
			r.setHakosu(2);
			r.setTani("台");
			r.setTanka(25000);
			ret.add(r);
		}
		{
			ExampleBean1 r = new ExampleBean1();
			r.setTokuisaki1("株式会社 岩手商事");
			r.setTokuisaki2("北上支社");
			r.setMitsumoriNo(101);
			r.setMitsumoriDate(sdf.parse("2013/03/01"));
			r.setHinmei("トナーカートリッジ");
			r.setIrisu(2);
			r.setHakosu(2);
			r.setTani("本");
			r.setTanka(5000);
			ret.add(r);
		}
		return ret;
	}
	*/

	/*
	// JavaBeans(publicフィールド)を利用したサンプル
	private static List<ExampleBean2> getDataTable() throws Exception {
		List<ExampleBean2> ret = new ArrayList<ExampleBean2>();
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd");
		{
			ExampleBean2 r = new ExampleBean2();
			r.tokuisaki1 = "株式会社 岩手商事";
			r.tokuisaki2 ="北上支社";
			r.mitsumoriNo = 101;
			r.mitsumoriDate = sdf.parse("2013/03/01");
			r.hinmei = "ノートパソコン";
			r.irisu = 1;
			r.hakosu = 10;
			r.tani = "台";
			r.tanka = 70000;
			ret.add(r);
		}
		{
			ExampleBean2 r = new ExampleBean2();
			r.tokuisaki1 = "株式会社 岩手商事";
			r.tokuisaki2 ="北上支社";			
			r.mitsumoriNo = 101;
			r.mitsumoriDate = sdf.parse("2013/03/01");
			r.hinmei = "モニター";
			r.irisu = 1;
			r.hakosu = 10;
			r.tani = "台";
			r.tanka = 20000;
			ret.add(r);
		}
		{
			ExampleBean2 r = new ExampleBean2();
			r.tokuisaki1 = "株式会社 岩手商事";
			r.tokuisaki2 ="北上支社";
			r.mitsumoriNo = 101;
			r.mitsumoriDate = sdf.parse("2013/03/01");
			r.hinmei = "プリンタ";
			r.irisu = 1;
			r.hakosu = 2;
			r.tani = "台";
			r.tanka = 25000;
			ret.add(r);
		}
		{
			ExampleBean2 r = new ExampleBean2();
			r.tokuisaki1 = "株式会社 岩手商事";
			r.tokuisaki2 ="北上支社";
			r.mitsumoriNo = 101;
			r.mitsumoriDate = sdf.parse("2013/03/01");
			r.hinmei = "トナーカートリッジ";
			r.irisu = 2;
			r.hakosu = 2;
			r.tani = "本";
			r.tanka = 5000;
			ret.add(r);
		}
		return ret;
	}
	*/

}
