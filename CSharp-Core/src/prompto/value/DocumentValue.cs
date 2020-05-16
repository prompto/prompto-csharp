using System.Collections.Generic;
using System;
using prompto.value;
using prompto.runtime;
using prompto.type;
using Newtonsoft.Json;
using prompto.error;
using System.IO;

namespace prompto.value
{

    public class DocumentValue : BaseValue
    {
		IDictionary<String, IValue> values;

		public DocumentValue()
			: base (DocumentType.Instance)
		{
			values = new Dictionary<String, IValue>();
		}

		public DocumentValue(IDictionary<String, IValue> values)
			: base(DocumentType.Instance)
		{
			this.values = values;
		}

		public override bool IsMutable ()
		{
			return true;
		}

		public override object GetStorableData()
		{
			return values;
		}

		public override bool Equals(object rval)
		{
			if (!(rval is DocumentValue))
				return false;
			DocumentValue doc = (DocumentValue)rval;
			if (values.Count != doc.values.Count)
				return false;
			foreach (string key in values.Keys)
			{
				if (!doc.values.ContainsKey(key))
					return false;
				Object lival = values[key];
				Object rival = doc.values[key];
				if (!lival.Equals(rival))
					return false;
			}
			return true;
		}

		public ICollection<String> GetMemberNames()
		{
			return values.Keys;
		}

		public bool HasMember(String name)
		{
			return values.ContainsKey (name);
		}

		public override IValue GetMemberValue(Context context, String name, bool autoCreate)
		{
			switch (name)
			{
				case "count":
					return new IntegerValue(this.values.Count);
				case "keys":
					return GetKeys();
				case "values":
					return GetValues();
				default:
					return GetMember(name, autoCreate);
			}
		}

		private IValue GetValues()
		{
			ListValue list = new ListValue(AnyType.Instance);
			list.AddRange(this.values.Values);
			return list;
		}

		private IValue GetKeys()
		{
			HashSet<IValue> items = new HashSet<IValue>();
			foreach (String item in this.values.Keys)
				items.Add(new TextValue(item));
			return new SetValue(TextType.Instance, items);
		}

		public IValue GetMember(String name, bool autoCreate)
		{
            IValue result = null;
			if (values.TryGetValue(name, out result))
				return result;
			else if ("text" == name)
				return new TextValue(this.ToString());
			else if (autoCreate)
			{
				result = new DocumentValue();
				values[name] = result; 
				return result;
			} else
				return NullValue.Instance;
		
        }


		public void SetMember(String name, IValue value)
		{
			values[name] = value;
		}

		public override void SetMemberValue(Context context, String name, IValue value)
        {
            values[name] = value;
        }


		public override IValue GetItem (Context context, IValue index)
		{
			if (index is TextValue) {
				IValue result;
				if (values.TryGetValue (index.ToString(), out result))
					return result;
				else
					return NullValue.Instance;
			} else
				throw new InvalidDataError ("No such item:" + index.ToString ());
		}

		public override void SetItem (Context context, IValue item, IValue value)
		{
			if (!(item is TextValue))
				throw new InvalidDataError ("No such item:" + item.ToString ());
			values[item.ToString ()] = value;
		}

        public override IValue Add(Context context, IValue value)
        {
			if (value is DocumentValue)
			{
				Dictionary<String, IValue> result = new Dictionary<String, IValue>();
				foreach (string key in values.Keys)
				{
					result[key] = values[key];
				}
				IDictionary<String, IValue> other = ((DocumentValue)value).values;
				foreach (string key in other.Keys)
				{
					result[key] = other[key];
				}
				return new DocumentValue(result);
			}
			else
			    return base.Add(context, value);
        }


        public override string ToString()
		{
			Dictionary<String, byte[]> binaries = new Dictionary<String, byte[]>();
			// create textual data
			using (MemoryStream stream = new MemoryStream())
			{
				using (TextWriter text = new StreamWriter(stream))
				{
					using (JsonWriter generator = new JsonTextWriter(text))
					{
						generator.WriteStartObject();
						foreach (KeyValuePair<String, IValue> entry in values)
						{
							generator.WritePropertyName(entry.Key);
							if (entry.Value == null)
								generator.WriteNull();
							else {
								Object id = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(this);
								entry.Value.ToJson(null, generator, id, entry.Key, false, binaries);
							}
						}
						generator.WriteEndObject();
					}
				}
				return System.Text.Encoding.UTF8.GetString(stream.ToArray());
			}
		}
	

		public override void ToJson (Context context, JsonWriter generator, object instanceId, String fieldName, bool withType, Dictionary<string, byte[]> binaries)
		{
			try 
			{
				if(withType)
				{
					generator.WriteStartObject();
					generator.WritePropertyName("type");
					generator.WriteValue(DocumentType.Instance.GetTypeName());
					generator.WritePropertyName("value");
				}
				generator.WriteStartObject();
				foreach(KeyValuePair<String, IValue> entry in values) {
					generator.WritePropertyName(entry.Key);
					if(entry.Value==null)
						generator.WriteNull();
					else {
						Object id = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(this);
						entry.Value.ToJson(context, generator, id, entry.Key, withType, binaries);
					}
				}
				generator.WriteEndObject();
				if(withType)
					generator.WriteEndObject();
			} catch(Exception e) {
				throw new ReadWriteError(e.Message);
			}
		}
    }
}
