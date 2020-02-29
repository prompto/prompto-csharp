using System;
using System.Collections.Generic;
using System.Linq;
using prompto.type;
using prompto.value;

namespace prompto.reader
{

	public abstract class YAMLReader
	{

		public static ListValue read(String yaml)
		{
			var reader = new YamlDotNet.Serialization.Deserializer();
			var parsed = reader.Deserialize<object>(yaml);
			if (parsed is IDictionary<object, object>)
			{
				var list = new List<object>();
				list.Add(parsed);
				parsed = list;
			}
			if (parsed is IList<object>)
				return convertList((IList<object>)parsed);
			else
				throw new InvalidOperationException(parsed.GetType().FullName);
		}

		private static IValue convert(object o)
        {
			if (o == null)
				return NullValue.Instance;
			else if (o is IList<object>)
				return convertList((IList<object>)o);
			else if (o is IDictionary<object, object>)
				return convertDocument((IDictionary<object, object>)o);
			else if (o is string)
				return convertInteger((string)o);
			else
				throw new NotImplementedException(o.GetType().FullName);
		}

		private static DocumentValue convertDocument(IDictionary<object, object> dict)
        {
			IDictionary<String, IValue> values = dict.ToDictionary(x => x.Key.ToString(), x => convert(x.Value));
			return new DocumentValue(values);

		}

		private static IValue convertInteger(string value)
		{
			try
			{
				return IntegerValue.Parse(value);
			}
			catch (FormatException e)
			{
				return convertDouble(value);
			}
		}

		private static IValue convertDouble(string value)
        {
            try
            {
				return DecimalValue.Parse(value);
            }
            catch(FormatException e)
            {
				return convertBoolean(value);
            }
        }

  
		private static IValue convertBoolean(string value)
		{
			try
			{
				return BooleanValue.Parse(value);
			}
			catch (FormatException e)
			{
				return convertText(value);
			}
		}


		private static TextValue convertText(object value)
        {
			return new TextValue(value.ToString());
        }

		private static ListValue convertList(IList<object> list)
        {
			IEnumerable<IValue> values = list.Select(o => convert(o));
			return new ListValue(AnyType.Instance, values, false);
        }


	}
}
