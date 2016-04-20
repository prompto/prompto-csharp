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

		public bool HasMember(String name)
		{
			return values.ContainsKey (name);
		}

		public override IValue GetMember(Context context, String name, bool autoCreate)
		{
            IValue result;
			bool exists = values.TryGetValue (name, out result);
			if(autoCreate && !exists)
            {
                result = new Document();
                values[name] = result;
            }
            return result;
        }


		public override void SetMember(Context context, String name, IValue value)
        {
            values[name] = value;
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
