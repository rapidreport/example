package example;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import com.google.appengine.api.datastore.DatastoreService;
import com.google.appengine.api.datastore.DatastoreServiceFactory;
import com.google.appengine.api.datastore.Entity;
import com.google.appengine.api.datastore.PreparedQuery;
import com.google.appengine.api.datastore.Query;

import net.arnx.jsonic.JSON;
import net.arnx.jsonic.JSONException;

public class Util {
	
	private Util() {};
	
	public static Map<?, ?> readJson(String path) throws JSONException, IOException {
		InputStream is = new FileInputStream(path);
		try {
			Reader r = new InputStreamReader(is, "UTF-8");
			try {
				return (Map<?, ?>)JSON.decode(r);
			} finally {
				r.close();
			}
		} finally {
			is.close();
		}
	}
	
	public static String escapeHtml(String v){
		if (v == null){
			return "";
		}
		return v.replaceAll("&", "&amp;")
				.replaceAll("\"", "&quot;")
				.replaceAll("<", "&lt;")
				.replaceAll(">", "&gt;");
	}
	
	public static List<Map<String, Object>> fetchData(Query q){
		DatastoreService datastore = DatastoreServiceFactory.getDatastoreService();
		PreparedQuery pq = datastore.prepare(q);
		List<Map<String, Object>> ret = new ArrayList<Map<String, Object>>();
		for(Entity en: pq.asIterable()){
			ret.add(en.getProperties());
		}
		return ret;
	}
}
