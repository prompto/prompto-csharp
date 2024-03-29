using System;
using System.Collections.Generic;
using prompto.runtime;
using prompto.type;
using Newtonsoft.Json;
using prompto.store;
using Newtonsoft.Json.Linq;

namespace prompto.value
{
	public interface IValue
	{
		bool IsMutable();
		IType GetIType();
		void SetIType(IType type);
		IValue Add(Context context, IValue value);
		IValue Subtract(Context context, IValue iValue);
		IValue Multiply(Context context, IValue iValue);
		IValue Divide(Context context, IValue iValue);
		IValue IntDivide(Context context, IValue iValue);
		IValue Modulo(Context context, IValue iValue);
		IValue GetMemberValue(Context context, String attrName, bool autoCreate);
		void SetMemberValue(Context context, String attrName, IValue value);
		IValue GetItem(Context context, IValue item);
		void SetItem(Context context, IValue item, IValue value);
		Int32 CompareTo(Context context, IValue value);
		Object ConvertTo(Context context, Type type);
		bool Equals(Context context, IValue value);
		bool Roughly(Context context, IValue value);
		bool Contains(Context context, IValue value);
		JToken ToJsonToken();
		void ToJson(Context context, JsonWriter generator, Object instanceId, String fieldName, bool withType, Dictionary<String, byte[]> binaries);
        IValue ToDocumentValue(Context context);
        object GetStorableData();
		void CollectStorables(List<IStorable> storables);

	}
}
