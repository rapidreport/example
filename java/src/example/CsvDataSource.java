package example;

import java.io.IOException;
import java.io.Reader;
import java.math.BigDecimal;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;
import java.util.regex.Pattern;

import jp.co.systembase.report.data.UnknownFieldException;
import jp.co.systembase.report.data.IReportDataSource;

//CSVデータを帳票に渡すためのクラス
public class CsvDataSource implements IReportDataSource{


	private List<String> colNames;
	private List<List<String>> rows = new ArrayList<List<String>>();

	private static Pattern datePattern = Pattern.compile("^\\d{4}/\\d{1,2}/\\d{1,2}$");
	private static Pattern numPattern = Pattern.compile("^-?\\d+(\\.\\d*)?$");

	public CsvDataSource(Reader r) throws IOException{
		// 最初の行から列名を読み込みます
		colNames = readCsv(r);
		// データ本体を読み込みます
		while(true){
			List<String> row = readCsv(r);
			if (row == null){
				break;
			}else{
				rows.add(row);
			}
		}
	}

	// データの行数を返します
	@Override
	public int size() {
		return rows.size();
	}

	// i行目のkey列の値を返します
	@Override
	public Object get(int i, String key) throws UnknownFieldException {
		int j = colNames.indexOf(key);
		if (j == -1){
			// 不明な列名が指定されたら例外を発生させます
			throw new UnknownFieldException(this, i, key);
		}else{
			return parseValue(rows.get(i).get(j));
		}
	}

	// CSVデータを1行読み込みます
	private List<String> readCsv(Reader r) throws IOException{
		int c = r.read();
		if (c == -1){
			return null;
		}
		List<String> ret = new ArrayList<String>();
		StringBuffer sb = new StringBuffer();
		boolean q = false;
		boolean qe = false;
		boolean cr = false;
		while(c != -1){
			if (c == '"'){ // ダブルクオート["]の処理
				if (!q){
					q = true;
				}else if (!qe){
					qe = true;
				}else{
					// 値にダブルクオートを追加
					sb.append("\"");
					qe = false;
				}
			}else if (c == '\r'){ // CRならばフラグを立てる
				cr = true;
			}else{
				if (qe){
					q = false;
					qe = false;
				}
				if (!q && cr && c == '\n'){ // CRLFならば行の区切り
					break;
				}
				cr = false;
				if (!q && c == ','){ // カンマ[,]ならば値の区切り
					ret.add(sb.toString());
					sb = new StringBuffer();
				}else if (c == '\n'){ // LFならば値に改行[CRLF]を追加
					if (q){
						sb.append("\r\n");
					}
				}else{
					// それ以外の文字を値に追加
					sb.append((char)c);
				}
			}
			c = r.read();
		}
		ret.add(sb.toString());
		return ret;
	}

	// 値を適切な型に変換して返します
	private Object parseValue(String v){
		try{
			if (v != null){
				if (datePattern.matcher(v).find()){
					SimpleDateFormat df = new SimpleDateFormat("yyyy/M/d");
					return df.parse(v);
				}else if (numPattern.matcher(v).find()){
					return new BigDecimal(v);
				}
			}
		}catch(Exception e){
		}
		return v;
	}

}
