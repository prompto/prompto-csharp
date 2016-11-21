using prompto.runtime;
using System;
using prompto.value;
using prompto.utils;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using prompto.store;
using prompto.declaration;

namespace prompto.type
{

	public interface IType
	{
	
		String GetTypeName ();

		TypeFamily GetFamily();

		IType checkAdd (Context context, IType other, bool tryReverse);

		IType checkSubstract (Context context, IType other);

		IType checkMultiply (Context context, IType other, bool tryReverse);

		IType checkDivide (Context context, IType other);

		IType checkIntDivide (Context context, IType rt);

		IType checkModulo (Context context, IType rt);

		IType checkCompare (Context context, IType other);

		IType checkItem (Context context, IType itemType);

		IType checkRange (Context context, IType other);

		IType checkContains (Context context, IType other);

		IType checkContainsAllOrAny (Context context, IType other);

		IType checkIterator (Context context);

		IType checkSlice (Context context);

		IType checkMember (Context context, String name);

		IRange newRange (Object left, Object right);

		IValue getMemberValue (Context context, String name);

		ICollection<IMethodDeclaration> getMemberMethods(Context context, String name);

		void checkUnique (Context context);

		void checkExists (Context context);

		void checkAssignableFrom (Context context, IType other);

		bool isAssignableFrom(Context context, IType other);

		bool isMoreSpecificThan (Context context, IType other);

		ListValue sort (Context context, IContainer list, bool descending);

		String ToString (Object value);

		void ToDialect (CodeWriter writer);

		Type ToCSharpType ();

		IValue ConvertCSharpValueToIValue (Context context, Object value);

		IValue ReadJSONValue (Context context, JToken json, Dictionary<String, byte[]> parts);
}
 
}