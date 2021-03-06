﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using prompto.type;
using prompto.value;

namespace prompto.reader
{

	public abstract class JSONReader
	{

		public static object read(String json)
		{
			byte[] byteArray = Encoding.UTF8.GetBytes(json);
			Stream stream = new MemoryStream(byteArray);
			JsonReader reader = new JsonTextReader(new StreamReader(stream));
			object node = new JsonSerializer().Deserialize(reader); 
			return nodeToValue(node);
		}

		private static IValue nodeToValue(object node)
		{
			if (node == null)
				return NullValue.Instance;
			else if (node is bool)
				return value.BooleanValue.ValueOf((bool)node);
			else if (node is int)
				return new value.IntegerValue((int)node);
			else if (node is long)
				return new value.IntegerValue((long)node);
			else if (node is float)
				return new value.DecimalValue((float)node);
			else if (node is double)
				return new value.DecimalValue((double)node);
			else if (node is string)
				return new value.TextValue((string)node);
			else if (node is JObject)
				return nodeToDocument((JObject)node);
			else if (node is JArray)
				return nodeToList((JArray)node);
			else
				throw new Exception("unsupported node type: " + node.GetType().Name);
	
		}

		private static IValue nodeToValue(JToken node)
		{
			if (node == null)
				return NullValue.Instance;
			else 
				switch (node.Type)
				{
					case JTokenType.Null:
						return NullValue.Instance;
					case JTokenType.Boolean:
						return value.BooleanValue.ValueOf(node.Value<bool>());
					case JTokenType.Integer:
						return new value.IntegerValue(node.Value<long>());
					case JTokenType.Float:
						return new value.DecimalValue(node.Value<double>());
					case JTokenType.String:
						return new value.TextValue(node.Value<string>());
					case JTokenType.Array:
						return nodeToList(node.Value<JArray>());
					case JTokenType.Object:
						return nodeToDocument(node.Value<JObject>());
					default:
						throw new Exception("unsupported node type: " + node.Type);
			}

		}

		private static value.DocumentValue nodeToDocument(JObject node)
		{
			Dictionary<String, IValue> values = new Dictionary<String, IValue>();
			foreach (JProperty prop in node.Properties())
			{
				values.Add(prop.Name, nodeToValue(prop.Value));
			}
			return new DocumentValue(values);
		}


		private static ListValue nodeToList(JArray node)
		{
			ListValue list = new ListValue(AnyType.Instance);
			foreach (JToken child in node)
			{
				list.Add(nodeToValue(child));
			}
			return list;
		}

	}
}
