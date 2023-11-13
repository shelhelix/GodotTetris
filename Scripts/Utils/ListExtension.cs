using System.Collections.Generic;

namespace GodotTetris.Scripts.Utils; 

public static class ListExtension {
	public static T GetOrDefault<T>(this List<T> list, int index) {
		if ( (index < 0) || (index >= list.Count) ) {
			return default;
		}
		return list[index];
	}
}