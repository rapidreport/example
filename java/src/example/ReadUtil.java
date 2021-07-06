package example;

/*
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.LinkedHashMap;
import java.util.Map;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;

public class ReadUtil {

	private ReadUtil(){};

	public static Map<?, ?> readJson(String path) throws IOException{
		InputStream is = new FileInputStream(path);
		try {
			ObjectMapper mapper = new ObjectMapper();
			mapper.configure(JsonParser.Feature.ALLOW_UNQUOTED_FIELD_NAMES, true);
			return mapper.readValue(is, new TypeReference<LinkedHashMap<String,Object>>(){});
		}finally {
			is.close();
		}
	}
}
*/

import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;
import java.util.Map;

import net.arnx.jsonic.JSON;
import net.arnx.jsonic.JSONException;

public class ReadUtil {

	private ReadUtil() {};

	public static Map<?, ?> readJson(String path) throws JSONException, IOException {
		InputStream is = new FileInputStream(path);
		try {
			Reader r = new InputStreamReader(is, "UTF-8");
			try {
				JSON json = new JSON();
				json.setMaxDepth(256);
				return (Map<?, ?>) JSON.decode(r);
			} finally {
				r.close();
			}
		} finally {
			is.close();
		}
	}
}
