package example;

import java.util.Comparator;
import java.util.Map;

public class MultiKeyComparator implements Comparator<Map<String, Object>> {

	private String[] keys;

	public MultiKeyComparator(String...keys){
		this.keys = keys;
	}

	@Override
	public int compare(Map<String, Object> o1, Map<String, Object> o2) {
		for(String key: this.keys){
			Object v1 = o1.get(key);
			Object v2 = o2.get(key);
			if (v1 instanceof Comparable){
				@SuppressWarnings("unchecked")
				int c = ((Comparable<Object>)v1).compareTo(v2);
				if (c != 0){
					return c;
				}
			}
		}
		return 0;
	}

}
