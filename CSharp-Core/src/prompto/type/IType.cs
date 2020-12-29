using prompto.runtime;
using System;
using prompto.value;
using prompto.utils;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using prompto.store;
using prompto.declaration;
using prompto.expression;

namespace prompto.type
{

	public interface IType
	{
	
		String GetTypeName ();

		TypeFamily GetFamily();

		IType Anyfy();

        IType Resolve(Context context);

		bool IsMutable(Context context);

		IType AsMutable(Context context, bool mutable);

		IType checkAdd (Context context, IType other, bool tryReverse);

		IType checkSubstract (Context context, IType other);

		IType checkMultiply (Context context, IType other, bool tryReverse);

		IType checkDivide (Context context, IType other);

		IType checkIntDivide (Context context, IType rt);

		IType checkModulo (Context context, IType rt);

		void checkCompare (Context context, IType other);

		IType checkItem (Context context, IType itemType);

		IType checkRange (Context context, IType other);

		void checkContains (Context context, IType other);

		void checkContainsAllOrAny (Context context, IType other);

		IType checkIterator (Context context);

		IType checkSlice (Context context);

		IType checkMember (Context context, String name);

        IType checkStaticMember(Context context, String name);

        IRange newRange (Object left, Object right);

		ISet<IMethodDeclaration> getMemberMethods(Context context, String name);

        ISet<IMethodDeclaration> getStaticMemberMethods(Context context, String name);

        IValue getStaticMemberValue(Context context, string name);

        void checkUnique (Context context);

		void checkExists (Context context);

		void checkAssignableFrom (Context context, IType other);

		bool isAssignableFrom(Context context, IType other);

		bool isMoreSpecificThan (Context context, IType other);

		Comparer<IValue> getComparer(Context context, IExpression key, bool descending);

		String ToString (Object value);

		void ToDialect(CodeWriter writer);

		void ToDialect (CodeWriter writer, bool skipMutable);

		Type ToCSharpType (Context context);

		IValue ConvertCSharpValueToIValue (Context context, Object value);

		IValue ReadJSONValue (Context context, JToken json, Dictionary<String, byte[]> parts);
     }
 
}