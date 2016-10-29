using prompto.runtime;
using System;
using prompto.value;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace prompto.type
{

    public class DocumentType : NativeType
    {

        static DocumentType instance_ = new DocumentType();

        public static DocumentType Instance
        {
            get
            {
                return instance_;
            }
        }

        private DocumentType()
			: base(TypeFamily.DOCUMENT)
        {
        }

		public override IType checkItem (Context context, IType itemType)
		{
			if (itemType == TextType.Instance)
				return AnyType.Instance;
			else
				return base.checkItem (context, itemType);
		}

		public override IType checkMember(Context context, String name)
        {
            return AnyType.Instance;
        }

        override
        public Type ToCSharpType()
        {
            return typeof(Document);
        }

        override
        public bool isAssignableTo(Context context, IType other)
        {
            return (other is DocumentType) || (other is AnyType);
        }

		public override IValue ReadJSONValue (Context context, JToken value, Dictionary<String, byte[]> parts) {
			if (!(value is JObject))
				throw new InvalidDataException ("Expecting a JSON object!");
			JObject obj = (JObject)value;
			Document instance = new Document();
			foreach(KeyValuePair<String, JToken> prop in obj) {
				IValue item = ReadJSONField(context, prop.Value, parts);
				instance.SetMember(context, prop.Key, item);
			}
			return instance;
		}

		private IValue ReadJSONField(Context context, JToken fieldData, Dictionary<String, byte[]> parts) {
			switch (fieldData.Type) {
			case JTokenType.Null:
				return NullValue.Instance;
			case JTokenType.Boolean:
				return prompto.value.Boolean.ValueOf (fieldData.Value<bool>());
			case JTokenType.Integer:
				return new prompto.value.Integer (fieldData.Value<long>());
			case JTokenType.Float:
				return new prompto.value.Decimal (fieldData.Value<double>());
			case JTokenType.String:
				return new prompto.value.Text (fieldData.Value<String>());
			case JTokenType.Array:
				throw new NotSupportedException ("Array");
			case JTokenType.Object:
				throw new NotSupportedException ("Object");
			default:
				throw new NotSupportedException (fieldData.Type.ToString ());
			}
		}
    }

}
