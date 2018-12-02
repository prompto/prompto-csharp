using System.Collections.Generic;

namespace prompto.utils
{
	public abstract class DictionaryUtils
	{
		public static bool AreEqual<K, V>(Dictionary<K, V> d1, Dictionary<K, V> d2)
		{
			if (d1.Keys.Count != d2.Keys.Count)
				return false;
			foreach (KeyValuePair<K, V> kvp in d1)
			{
				V v2;
				if (!d2.TryGetValue(kvp.Key, out v2))
					return false;
				if (!v2.Equals(kvp.Value))
					return false;
			}
			return true;
		}

	}
}
