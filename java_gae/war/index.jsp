<%@ page contentType="text/html; charset=UTF-8" language="java"%>
<%@ page import="java.util.Date" %>
<%@ page import="example.Util" %>
<html>
<head>
<link href="./global.css" rel="stylesheet" type="text/css"/>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<script type="text/javascript">
function example2_click() {
	var error_message = document.getElementById('error_message');
	var start_date = document.getElementById('start_date');
	var end_date = document.getElementById('end_date');
	error_message.innerHTML = ''
	if (start_date.value.length > 0 && !dateValidate(start_date.value)){
		error_message.innerHTML = '開始日付の形式が不正です';
		return;
	}
	if (end_date.value.length > 0 && !dateValidate(end_date.value)){
		error_message.innerHTML = '終了日付の形式が不正です';
		return;
	}	
	var f = document.getElementById('example2');
	f.submit();
}
function dateValidate(v) {
	if(v.match(/^\d{4}\/\d{2}\/\d{2}$/)){
		var y = v.substr(0, 4) - 0;
		var m = v.substr(5, 2) - 1;
		var d = v.substr(8, 2) - 0;
		if(m >= 0 && m <= 11 && d >= 1 && d <= 31){
			var dt = new Date(y, m, d);
			if (dt.getFullYear() == y && dt.getMonth() == m && dt.getDate() == d){
				return true;
			}
		}
	}
	return false;
}
</script>
</head>
<body>

	<a href="http://rapidreport.systembase.co.jp"> 
	<img src="./logo.png" width="240px" height="64px" class="logo" />
	</a>
	<hr />

	<p>
	帳票ツール「RapidReport」のデモサイトです。<a href="https://developers.google.com/appengine/?hl=ja">Google App Engine</a> を用いたデモを実行できます。
	</p>
	
	<p>
	RapidReportについては<a href="http://rapidreport.systembase.co.jp/">http://rapidreport.systembase.co.jp/</a>をご覧ください。
	</p>
	
	<p>
	RapidReportインストールフォルダ内の example\java_gae というフォルダに本サイトのソースコードが含まれています。
	</p>
	
	<br/><br/>
	<h1>デモ1 見積書</h1>
	
	<p>
	[実行]ボタンを押すと、<a href="http://rapidreport.systembase.co.jp/example1.html">「基本サンプル 見積書」</a>の帳票を出力します。
	</p>
	
	<p>
	出力される帳票の内容は固定ですが、動的に生成を行っています。<br/>
	対応するソースファイルは Example1.java です。
	</p>
	
	<form id="example1" action="/example1">
	<p>
		<input type="submit" value="実行" />
	</p>
	</form>

<%
String kaisha = request.getParameter("kaisha");
String startDate = request.getParameter("start_date");
String endDate = request.getParameter("end_date");
String errorMessage = (String)request.getAttribute("error_message");

if (kaisha == null){
	kaisha = "株式会社 システムベース";
}
if (startDate == null){
	startDate = "2013/04/01";
}
if (endDate == null){
	endDate = "2013/09/30";
}
%>

	<br /><br/>
	<h1>デモ2 売上明細表</h1>

	<p>
	[実行]ボタンを押すと、<a href="http://rapidreport.systembase.co.jp/example2.html">「基本サンプル 売上明細表」</a>の帳票を出力します。
	</p>
	
	<p>
	入力した[会社名]と[集計期間]は、帳票内に出力されます。
	また、[集計期間]はデータの検索条件として利用されます。<br/>
	対応するソースファイルは Example2.java です。
	</p>
	
	<form id="example2" action="/example2">
		<p>
			会社名：<input type="text" name="kaisha" value="<%=Util.escapeHtml(kaisha)%>" size="30" maxlength="20"/>
		</p>
		<p>
			集計期間 (YYYY/MM/DD形式) ：
			<input type="text" id="start_date" name="start_date" size="10" value="<%=Util.escapeHtml(startDate)%>"/>
			～
			<input type="text" id="end_date" name="end_date" size="10" value="<%=Util.escapeHtml(endDate)%>"/><br/>
		</p>
		
		<p>
		<div id="error_message"><%=Util.escapeHtml(errorMessage)%></div>
		</p>
		
		<p>
			<input type="button" value="実行" onclick="example2_click()"/>
		</p>
	</form>
</body>
</html>