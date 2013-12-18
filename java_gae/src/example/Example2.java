package example;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Date;
import java.util.List;
import java.util.Map;
import java.util.logging.Logger;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.google.appengine.api.datastore.Query;
import com.google.appengine.api.datastore.Query.CompositeFilterOperator;
import com.google.appengine.api.datastore.Query.FilterOperator;

import jp.co.systembase.report.Report;
import jp.co.systembase.report.ReportPages;
import jp.co.systembase.report.data.ReportDataSource;
import jp.co.systembase.report.renderer.pdf.PdfRenderer;

public class Example2 extends HttpServlet {

	private static final long serialVersionUID = -7602648285733302535L;
	private static final Logger log = Logger.getLogger(Example1.class.getName());

	@Override
	protected void doGet(HttpServletRequest req, HttpServletResponse resp)
			throws ServletException, IOException {
		try {
			// パラメータを取得します
			String kaisha = req.getParameter("kaisha");
			Date startDate = null;
			Date endDate = null;
			{
				// 開始/終了日付をDate型に変換します
				SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd");
				try{
					String _startDate = req.getParameter("start_date");
					if (_startDate.length() > 0){
						startDate = sdf.parse(_startDate);	
					}
				}catch(ParseException e){
					req.setAttribute("error_message", "開始日付の形式が不正です");
					req.getRequestDispatcher("/index.jsp").forward(req, resp);
					return;
				}
				try{
					String _endDate = req.getParameter("end_date");
					if (_endDate.length() > 0){
						endDate = sdf.parse(_endDate);	
					}
				}catch(ParseException e){
					req.setAttribute("error_message", "終了日付の形式が不正です");
					req.getRequestDispatcher("/index.jsp").forward(req, resp);
					return;
				}
			}
			
			// データを取得します
			List<Map<String, Object>> data;
			{
				Query q = new Query("Example2");
				q.addSort("uriageDate");
				List<Query.Filter> fl = new ArrayList<Query.Filter>();
				if (startDate != null){
					fl.add(FilterOperator.GREATER_THAN_OR_EQUAL.of("uriageDate", startDate));
				}
				if (endDate != null){
					fl.add(FilterOperator.LESS_THAN_OR_EQUAL.of("uriageDate", endDate));	
				}
				if (fl.size() == 1){
					q.setFilter(fl.get(0));
				}else if (fl.size() > 1){
					q.setFilter(CompositeFilterOperator.and(fl));
				}
				data = Util.fetchData(q);
				Collections.sort(data, new MultiKeyComparator("bumonCd", "denpyoNo", "detailNo"));
			}
			
			// 該当データが無ければエラーメッセージを返します
			if (data.size() == 0){
				req.setAttribute("error_message", "該当するデータがありません");
				req.getRequestDispatcher("/index.jsp").forward(req, resp);
				return;
			}

			// 帳票定義を読込みます
			Report report = new Report(Util.readJson(
					getServletContext().getRealPath("WEB-INF/report/example2.rrpt")));

			// globalScopeに値を登録します
			report.globalScope.put("startDate", startDate);
			report.globalScope.put("endDate",  endDate);
			report.globalScope.put("printDate", new Date());
			report.globalScope.put("kaisha", kaisha);
						
			// 帳票にデータを渡し、ページ分割を行います
			report.fill(new ReportDataSource(data));
			ReportPages pages = report.getPages();
			
			// PDFをレスポンスとして出力します
			ByteArrayOutputStream byteout = new ByteArrayOutputStream();
			PdfRenderer renderer = new PdfRenderer(byteout);
			pages.render(renderer);
			resp.setContentType("application/pdf");
			resp.setHeader("Content-Disposition","attachment; filename=example2.pdf");
			resp.setContentLength(byteout.size());
			OutputStream out = resp.getOutputStream();
			try {
				out.write(byteout.toByteArray());
			} finally{
				out.close();
			}
			
		} catch (Exception e) {
			log.warning(e.getMessage());
			resp.sendRedirect("/");
		}
	}

}
