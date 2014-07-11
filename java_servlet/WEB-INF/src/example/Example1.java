package example;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.text.SimpleDateFormat;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import jp.co.systembase.core.DataTable;
import jp.co.systembase.report.Report;
import jp.co.systembase.report.ReportPages;
import jp.co.systembase.report.data.ReportDataSource;
import jp.co.systembase.report.renderer.pdf.PdfRenderer;

public class Example1 extends HttpServlet {

	private static final long serialVersionUID = 1L;

	public void doGet(HttpServletRequest request, HttpServletResponse response)
			throws IOException, ServletException {

		try {
			// 帳票定義を読込みます
			Report report = new Report(ReadUtil.readJson(
					getServletContext().getRealPath("WEB-INF/report/example1.rrpt")));

			// 帳票にデータを渡します
			report.fill(new ReportDataSource(getDataTable()));

			// ページ分割を行います
			ReportPages pages = report.getPages();

			// PDF出力
			{
				ByteArrayOutputStream byteout = new ByteArrayOutputStream();
				PdfRenderer renderer = new PdfRenderer(byteout);
				// バックスラッシュ文字を円マーク文字に変換します
				renderer.setting.replaceBackslashToYen = true;
				pages.render(renderer);

				response.setContentType("application/pdf");
				response.setHeader("Content-Disposition","attachment; filename=example1.pdf");
				response.setContentLength(byteout.size());
				OutputStream out = response.getOutputStream();
				try {
					out.write(byteout.toByteArray());
				} finally{
					out.close();
				}
			}
		} catch (Exception e) {}
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

}
