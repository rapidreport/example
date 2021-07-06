package example;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;

import javax.imageio.ImageIO;

import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;

import jp.co.systembase.core.DataTable;
import jp.co.systembase.report.Report;
import jp.co.systembase.report.ReportPages;
import jp.co.systembase.report.data.ReportDataSource;
import jp.co.systembase.report.renderer.ImageMap;
import jp.co.systembase.report.renderer.pdf.PdfRenderer;
import jp.co.systembase.report.renderer.pdf.imageloader.PdfImageLoader;
import jp.co.systembase.report.renderer.xls.XlsRenderer;
import jp.co.systembase.report.renderer.xls.imageloader.XlsImageLoader;
import jp.co.systembase.report.renderer.xlsx.XlsxRenderer;
import jp.co.systembase.report.renderer.xlsx.imageloader.XlsxImageLoader;

//機能サンプル 動的画像の表示
public class ExampleImage {

	public static void main(String[] args) throws Exception {

		// 帳票定義を読込みます
		Report report = new Report(ReadUtil.readJson("report/example_image.rrpt"));

		// 帳票にデータを渡します
		report.fill(new ReportDataSource(getDataTable()));

		// ページ分割を行います
		ReportPages pages = report.getPages();

		// イメージマップを作成します
		ImageMap imageMap = getImageMap();

		// PDF出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_image.pdf");
			try {
				PdfRenderer renderer = new PdfRenderer(fos);
				renderer.imageLoaderMap.put("image", new PdfImageLoader(imageMap));
				pages.render(renderer);
			} finally {
				fos.close();
			}
		}

		// XLS出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_image.xls");
			try {
				HSSFWorkbook workBook = new HSSFWorkbook();
				XlsRenderer renderer = new XlsRenderer(workBook);
				renderer.imageLoaderMap.put("image", new XlsImageLoader(imageMap));
				renderer.newSheet("example_image");
				pages.render(renderer);
				workBook.write(fos);
			} finally {
				fos.close();
			}
		}

		// XLSX出力
		{
			FileOutputStream fos = new FileOutputStream("output/example_image.xlsx");
			try {
				XSSFWorkbook workBook = new XSSFWorkbook();
				XlsxRenderer renderer = new XlsxRenderer(workBook);
				renderer.imageLoaderMap.put("image", new XlsxImageLoader(imageMap));
				renderer.newSheet("example_image");
				pages.render(renderer);
				workBook.write(fos);
			} finally {
				fos.close();
			}
		}
	}

	private static DataTable getDataTable() throws Exception {
		DataTable ret = new DataTable();
		ret.setFieldNames("code", "name");
		ret.addRecord().puts(1, "ハクサンイチゲ");
		ret.addRecord().puts(2, "ニッコウキスゲ");
		ret.addRecord().puts(3, "チングルマ");
		ret.addRecord().puts(4, "コマクサ");
		return ret;
	}


	// イメージマップを作成します
	private static ImageMap getImageMap() throws IOException {
		ImageMap ret = new ImageMap();
		ret.put(1, ImageIO.read(new File("report\\image1.jpg")));
		ret.put(2, ImageIO.read(new File("report\\image2.jpg")));
		ret.put(3, ImageIO.read(new File("report\\image3.jpg")));
		ret.put(4, ImageIO.read(new File("report\\image4.jpg")));
		return ret;
	}

}
