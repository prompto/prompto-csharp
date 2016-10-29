using System;
using System.Collections.Generic;
using prompto.declaration;

namespace prompto.store
{
	public class MatchesPredicate<T> : IPredicate
	{

		AttributeInfo info;
		MatchOp match;
		T value;

		public MatchesPredicate(AttributeInfo info, MatchOp match, T value)
		{
			this.info = info;
			this.match = match;
			this.value = value;
		}

		public bool matches(Dictionary<String, Object> document)
		{
			Object data = null;
			document.TryGetValue(info.getName(), out data);
			switch (match)
			{
				case MatchOp.EQUALS:
					return matchesEQUALS(data);
				case MatchOp.ROUGHLY:
					return matchesROUGHLY(data);
				case MatchOp.CONTAINS:
					return matchesCONTAINS(data);
				case MatchOp.CONTAINED:
					return matchesCONTAINED(data);
				case MatchOp.GREATER:
					return matchesGREATER(data);
				case MatchOp.LESSER:
					return matchesLESSER(data);
				default:
					return false;
			}
		}

		private bool matchesGREATER(Object data)
		{
			if (data is IComparable && value is IComparable)
				return ((IComparable)data).CompareTo((IComparable)value) > 0;
			else
				return false;
		}

		private bool matchesLESSER(Object data)
		{
			if (data is IComparable && value is IComparable)
				return ((IComparable)data).CompareTo((IComparable)value) < 0;
			else
				return false;
		}

		private bool matchesCONTAINS(Object data)
		{
			if (data is String && value is String)
				return ((String)data).Contains((String)(object)value);
			else if (data is ICollection<T>)
				return ((ICollection<T>)data).Contains(value);
			else
				return false;
		}

		private bool matchesCONTAINED(Object data)
		{
			if (data is String && value is String)
				return ((String)(object)value).Contains((String)data);
			else if (value is ICollection<T> && data is T)
				return ((ICollection<T>)value).Contains((T)data);
			else
				return false;
		}

		private bool matchesROUGHLY(Object data)
		{
			if (data is String && value is String)
				return ((String)data).Equals((String)(object)value, StringComparison.InvariantCultureIgnoreCase);
			else
				return matchesEQUALS(data);
		}

		private bool matchesEQUALS(Object data)
		{
			if (data == null)
			return value == null;
		else
			return data.Equals(value);
	}

	}
	
}
