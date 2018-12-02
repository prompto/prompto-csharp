using System;

namespace prompto.utils
{

	public class ObjectUtils
	{

		public static bool AreEqual(Object o1, Object o2)
		{
			if (o1 == o2)
				return true;
			if (o1 == null || o2 == null)
				return false;
			return o1.Equals(o2);
		}

		public static String CapitalizeFirst(String value)
		{
			return Char.ToUpper(value[0]) + value.Substring(1);
		}

		public static int CompareValues(Object value1, Object value2)
		{
			if (value1 == null && value2 == null)
				return 0;
			else if (value1 == null)
				return -1;
			else if (value2 == null)
				return 1;
			else if (value1 is IComparable)
				return ((IComparable)value1).CompareTo(value2);
			else
				return value1.ToString().CompareTo(value2.ToString());
		}


	}
}