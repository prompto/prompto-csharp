using presto.runtime;
using System;
using presto.value;
using presto.utils;

namespace presto.type
{

	public interface IType
	{
	
		String GetName ();

		IType checkAdd (Context context, IType other, bool tryReverse);

		IType checkSubstract (Context context, IType other);

		IType checkMultiply (Context context, IType other, bool tryReverse);

		IType checkDivide (Context context, IType other);

		IType checkIntDivide (Context context, IType rt);

		IType CheckModulo (Context context, IType rt);

		IType checkCompare (Context context, IType other);

		IType checkItem (Context context, IType itemType);

		IType checkRange (Context context, IType other);

		IType checkContains (Context context, IType other);

		IType checkContainsAllOrAny (Context context, IType other);

		IType checkIterator (Context context);

		IType checkSlice (Context context);

		IType CheckMember (Context context, String name);

		IRange newRange (Object left, Object right);

		IValue getMember (Context context, String name);

		void checkUnique (Context context);

		void checkExists (Context context);

		void checkAssignableTo (Context context, IType other);

		bool isAssignableTo (Context context, IType other);

		bool isMoreSpecificThan (Context context, IType other);

		ListValue sort (Context context, IContainer list);

		String ToString (Object value);

		void ToDialect (CodeWriter writer);

		Type ToCSharpType ();

		IValue ConvertCSharpValueToPrestoValue (Object value);


	}
 
}