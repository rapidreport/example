package example;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.util.List;
import java.util.Map;
import java.util.logging.Logger;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.google.appengine.api.datastore.Query;

import jp.co.systembase.report.Report;
import jp.co.systembase.report.ReportPages;
import jp.co.systembase.report.data.ReportDataSource;
import jp.co.systembase.report.renderer.pdf.PdfRenderer;

public class Example1 extends HttpServlet {

	private static final long serialVersionUID = 3738559528821408763L;
	private static final Logger log = Logger.getLogger(Example1.class.getName());

	public void doGet(HttpServletRequest req, HttpServletResponse resp)
			throws IOException, ServletException {
		try {
			// データを取得します
			List<Map<String, Object>> data;
			{
				Query q = new Query("Example1");
				q.addSort("no");
				data = Util.fetchData(q);
			}
			
			// 帳票定義を読込みます
			Report report = new Report(Util.readJson(
					getServletContext().getRealPath("WEB-INF/report/example1.rrpt")));
			
			// 帳票にデータを渡し、ページ分割を行います
			report.fill(new ReportDataSource(data));
			ReportPages pages = report.getPages();
			
			// PDF出力
			{
				ByteArrayOutputStream byteout = new ByteArrayOutputStream();
				PdfRenderer renderer = new PdfRenderer(byteout);
				// バックスラッシュ文字を円マーク文字に変換します
				renderer.setting.replaceBackslashToYen = true;
				pages.render(renderer);
				
				resp.setContentType("application/pdf");
				resp.setHeader("Content-Disposition","attachment; filename=example1.pdf");
				resp.setContentLength(byteout.size());
				OutputStream out = resp.getOutputStream();
				try {
					out.write(byteout.toByteArray());
				} finally{
					out.close();
				}
			}
			
		} catch (Exception e) {
			log.warning(e.getMessage());
		}
	}

}
