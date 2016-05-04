using System.Collections.Generic;
using System;
using prompto.value;
using prompto.runtime;
using prompto.type;
using Newtonsoft.Json;
using prompto.error;


namespace prompto.value
{

    public class Document : BaseValue
    {
		Dictionary<String, IValue> values = new Dictionary<String, IValue>();

		public Document()
			: base (DocumentType.Instance)
		{
		}

		public override bool IsMutable ()
		{
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

		public override IValue GetMember(Context context, String name, bool autoCreate)
		{
			return GetMember (name, autoCreate);
		}

		public IValue GetMember(String name)
		{
			return GetMember (name, false);
		}

		public IValue GetMember(String name, bool autoCreate)
		{
            IValue result;
			if (values.TryGetValue (name, out result))
				return result;
			else
				result = NullValue.Instance;
			if(autoCreate)
            {
                result = new Document();
                values[name] = result;
            }
            return result;
        }


		public void SetMember(String name, IValue value)
		{
			values[name] = value;
		}

		public override void SetMember(Context context, String name, IValue value)
        {
            values[name] = value;
        }


		public override IValue GetItem (Context context, IValue index)
		{
			if (index is Text) {
				IValue result;
				if (values.TryGetValue (index.ToString(), out result))
					return result;
				else
					return null;
			} else
				throw new InvalidDataError ("No such item:" + index.ToString ());
		}

		public override void SetItem (Context context, IValue item, IValue value)
		{
			if (!(item is Text))
				throw new InvalidDataError ("No such item:" + item.ToString ());
			values[item.ToString ()] = value;
		}

		public override void ToJson (Context context, JsonWriter generator, object instanceId, String fieldName, Dictionary<string, byte[]> binaries)
		{
			try {
				generator.WriteStartObject();
				generator.WritePropertyName("type");
				generator.WriteValue(DocumentType.Instance.GetName());
				generator.WritePropertyName("value");
				generator.WriteStartObject();
				foreach(KeyValuePair<String, IValue> entry in values) {
					generator.WritePropertyName(entry.Key);
					IValue value = entry.Value;
					if(value==null)
						generator.WriteNull();
					else {
						Object id = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(this);
						value.ToJson(context, generator, id, entry.Key, binaries);
					}
				}
				generator.WriteEndObject();
				generator.WriteEndObject();
			} catch(Exception e) {
				throw new ReadWriteError(e.Message);
			}
		}
    }
}
