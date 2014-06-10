package example;

import java.awt.Color;
import java.awt.Graphics;
import java.awt.image.BufferedImage;
import java.io.FileOutputStream;

import jp.co.systembase.core.DataTable;
import jp.co.systembase.report.Report;
import jp.co.systembase.report.ReportDesign;
import jp.co.systembase.report.ReportPages;
import jp.co.systembase.report.ReportSetting;
import jp.co.systembase.report.ReportUtil;
import jp.co.systembase.report.component.ElementDesign;
import jp.co.systembase.report.component.Region;
import jp.co.systembase.report.data.ReportDataSource;
import jp.co.systembase.report.renderer.RenderUtil;
import jp.co.systembase.report.renderer.pdf.PdfRenderer;
import jp.co.systembase.report.renderer.pdf.PdfRendererSetting;
import jp.co.systembase.report.renderer.xls.XlsRenderer;
import jp.co.systembase.report.renderer.xls.XlsRendererSetting;
import jp.co.systembase.report.renderer.xlsx.XlsxRenderer;
import jp.co.systembase.report.renderer.xlsx.XlsxRendererSetting;
import jp.co.systembase.report.textformatter.ITextFormatter;

import org.apache.poi.hssf.usermodel.HSSFPatriarch;
import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.xssf.usermodel.XSSFDrawing;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;

import com.lowagie.text.pdf.PdfContentByte;

// 機能サンプル カスタム書式/要素
public class ExampleExtention {

	public static void main(String[] args) throws Exception {

		// 郵便番号フォーマッタが設定されたSettingオブジェクトを用意します
		ReportSetting setting = new ReportSetting();
		setting.textFormatterMap.put("yubin", new YubinTextFormatter());

		Report report = new Report(ReadUtil.readJson("report/example_extention.rrpt"), setting);
		report.fill(new ReportDataSource(getDataTable()));
		ReportPages pages = report.getPages();

		// PDF出力
		{
			// チェックボックスレンダラが設定されたSettingオブジェクトを用意します
			PdfRendererSetting pdfSetting = new PdfRendererSetting();
			pdfSetting.elementRendererMap.put("checkbox", new PdfCheckBoxRenderer());

			FileOutputStream fos = new FileOutputStream("output/example_extention.pdf");
			try {
				PdfRenderer renderer = new PdfRenderer(fos, pdfSetting);
				pages.render(renderer);
			} finally {
				fos.close();
			}
		}

		// XLS出力
		{
			// チェックボックスレンダラが設定されたSettingオブジェクトを用意します
			XlsRendererSetting xlsSetting = new XlsRendererSetting();
			xlsSetting.elementRendererMap.put("checkbox", new XlsCheckBoxRenderer());

			FileOutputStream fos = new FileOutputStream("output/example_extention.xls");
			try {
				HSSFWorkbook workBook = new HSSFWorkbook();
				XlsRenderer renderer = new XlsRenderer(workBook, xlsSetting);
				renderer.newSheet("example_extention");
				pages.render(renderer);
				workBook.write(fos);
			} finally {
				fos.close();
			}
		}

		// XLSX出力
		{
			// チェックボックスレンダラが設定されたSettingオブジェクトを用意します
			XlsxRendererSetting xlsxSetting = new XlsxRendererSetting();
			xlsxSetting.elementRendererMap.put("checkbox", new XlsxCheckBoxRenderer());

			FileOutputStream fos = new FileOutputStream("output\\example_extention.xlsx");
			try {
				XSSFWorkbook workBook = new XSSFWorkbook();
				XlsxRenderer renderer = new XlsxRenderer(workBook, xlsxSetting);
				renderer.newSheet("example_extention");
				pages.render(renderer);
				workBook.write(fos);
			} finally {
				fos.close();
			}
		}
	}

	private static DataTable getDataTable() throws Exception {
		DataTable ret = new DataTable();
		ret.setFieldNames("check");
		ret.addRecord().puts(true);
		ret.addRecord().puts(false);
		ret.addRecord().puts(true);
		ret.addRecord().puts(false);
		return ret;
	}

	// 郵便番号フォーマッタ
	public static class YubinTextFormatter implements ITextFormatter 
	{
		public String format(Object v, ElementDesign design)
		{
			if (v == null)
			{
				return null;
			}
			String _v = v.toString();
			if (_v.length() > 3)
			{
				return _v.substring(0, 3) + "-" + _v.substring(3);
			}
			else
			{
				return _v;
			}
		}
	}

	// チェックボックスを描く要素レンダラ(PDF)
	public static class PdfCheckBoxRenderer
	  implements jp.co.systembase.report.renderer.pdf.elementrenderer.IElementRenderer
	{
		public void render(
				PdfRenderer renderer, 
				ReportDesign reportDesign, 
				Region region, 
				ElementDesign design, 
				Object data)
		{
			Region r = region.toPointScale(reportDesign);
			PdfContentByte cb = renderer.writer.getDirectContent();
			float x = r.left + r.getWidth() / 2;
			float y = r.top + r.getHeight() / 2;
			float w = 12;
			cb.saveState();
			cb.rectangle(renderer.trans.x(x - w / 2), renderer.trans.y(y - w / 2), w, -w);
			cb.stroke();
			if (ReportUtil.condition(data)){
				cb.setColorFill(RenderUtil.getColor("steelblue"));
				cb.moveTo(renderer.trans.x(x - w / 2), renderer.trans.y(y - w / 4));
				cb.lineTo(renderer.trans.x(x - w / 4), renderer.trans.y(y + w / 2));
				cb.lineTo(renderer.trans.x(x + w / 2), renderer.trans.y(y - w / 2));
				cb.lineTo(renderer.trans.x(x - w / 4), renderer.trans.y(y));
				cb.fill();
			}
			cb.restoreState();
		}
	}

	// チェックボックスを描く要素レンダラ(XLS)
	public static class XlsCheckBoxRenderer
	  implements jp.co.systembase.report.renderer.xls.elementrenderer.IElementRenderer
	{
		public void collect(
				XlsRenderer renderer, 
				ReportDesign reportDesign, 
				Region region, 
				ElementDesign design, 
				Object data)
		{
			Region r = region.toPointScale(reportDesign);
			jp.co.systembase.report.renderer.xls.component.Shape shape =
					new jp.co.systembase.report.renderer.xls.component.Shape();
			shape.region = r;
			shape.renderer = new CheckBoxShapeRenderer(data);
			renderer.currentPage.shapes.add(shape);
		}

		private static BufferedImage checkedImage = null;
		private static BufferedImage noCheckedImage = null;

		private static void createImage()
		{
			if (checkedImage == null)
			{
				checkedImage = new BufferedImage(40, 40, BufferedImage.TYPE_INT_RGB);
				Graphics g = checkedImage.getGraphics();
				g.setColor(Color.WHITE);
				g.fillRect(0, 0, 40, 40);
				g.setColor(Color.BLACK);
				g.drawRect(10, 10, 20, 20);
				g.setColor(RenderUtil.getColor("steelblue"));
				int x[] = {10, 15, 30, 15};
				int y[] = {15, 30, 10, 20};
				g.fillPolygon(x, y, 4);
			}
			if (noCheckedImage == null)
			{
				noCheckedImage = new BufferedImage(40, 40, BufferedImage.TYPE_INT_RGB);
				Graphics g = noCheckedImage.getGraphics();
				g.setColor(Color.WHITE);
				g.fillRect(0, 0, 40, 40);
				g.setColor(Color.BLACK);
				g.drawRect(10, 10, 20, 20);
			}
		}

		public static class CheckBoxShapeRenderer
		  implements jp.co.systembase.report.renderer.xls.elementrenderer.IShapeRenderer
		{
			public Object data;
			public CheckBoxShapeRenderer(Object data)
			{
				this.data = data;
			}
			public void render(
					jp.co.systembase.report.renderer.xls.component.Page page, 
					jp.co.systembase.report.renderer.xls.component.Shape shape)
			{
				createImage();
				int index;
				if (ReportUtil.condition(this.data))
				{
					index = page.renderer.getImageIndex(checkedImage);
				}
				else
				{
					index = page.renderer.getImageIndex(noCheckedImage);
				}
				if (index > 0)
				{
					HSSFPatriarch p = page.renderer.sheet.getDrawingPatriarch();
					p.createPicture(shape.getHSSFClientAnchor(page.topRow), index);
				}
			}
		}
	}

	// チェックボックスを描く要素レンダラ(XLSX)
	public static class XlsxCheckBoxRenderer
	  implements jp.co.systembase.report.renderer.xlsx.elementrenderer.IElementRenderer
	{
		public void collect(
				XlsxRenderer renderer, 
				ReportDesign reportDesign, 
				Region region, 
				ElementDesign design, 
				Object data)
		{
			Region r = region.toPointScale(reportDesign);
			jp.co.systembase.report.renderer.xlsx.component.Shape shape =
					new jp.co.systembase.report.renderer.xlsx.component.Shape();
			shape.region = r;
			shape.renderer = new CheckBoxShapeRenderer(data);
			renderer.currentPage.shapes.add(shape);
		}

		private static BufferedImage checkedImage = null;
		private static BufferedImage noCheckedImage = null;

		private static void createImage()
		{
			if (checkedImage == null)
			{
				checkedImage = new BufferedImage(40, 40, BufferedImage.TYPE_INT_RGB);
				Graphics g = checkedImage.getGraphics();
				g.setColor(Color.WHITE);
				g.fillRect(0, 0, 40, 40);
				g.setColor(Color.BLACK);
				g.drawRect(10, 10, 20, 20);
				g.setColor(RenderUtil.getColor("steelblue"));
				int x[] = {10, 15, 30, 15};
				int y[] = {15, 30, 10, 20};
				g.fillPolygon(x, y, 4);
			}
			if (noCheckedImage == null)
			{
				noCheckedImage = new BufferedImage(40, 40, BufferedImage.TYPE_INT_RGB);
				Graphics g = noCheckedImage.getGraphics();
				g.setColor(Color.WHITE);
				g.fillRect(0, 0, 40, 40);
				g.setColor(Color.BLACK);
				g.drawRect(10, 10, 20, 20);
			}
		}

		public static class CheckBoxShapeRenderer
		  implements jp.co.systembase.report.renderer.xlsx.elementrenderer.IShapeRenderer
		{
			public Object data;
			public CheckBoxShapeRenderer(Object data)
			{
				this.data = data;
			}
			public void render(
					jp.co.systembase.report.renderer.xlsx.component.Page page, 
					jp.co.systembase.report.renderer.xlsx.component.Shape shape)
			{
				createImage();
				int index;
				if (ReportUtil.condition(this.data))
				{
					index = page.renderer.getImageIndex(checkedImage);
				}
				else
				{
					index = page.renderer.getImageIndex(noCheckedImage);
				}
				if (index >= 0)
				{
					XSSFDrawing d = page.renderer.sheet.createDrawingPatriarch();
					d.createPicture(shape.getXSSFClientAnchor(page.topRow), index);
				}
			}
		}
	}

}
