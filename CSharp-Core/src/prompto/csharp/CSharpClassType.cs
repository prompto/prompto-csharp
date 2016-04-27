using System.Collections.Generic;
using System;
using prompto.value;
using DateTime = prompto.value.DateTime;
using Boolean = System.Boolean;
using prompto.error;
using prompto.type;
using prompto.declaration;
using prompto.runtime;
using System.Collections;

namespace prompto.csharp
{
    public class CSharpClassType : CategoryType
    {

		static Dictionary<Type, IType> typeToPrestoMap = new Dictionary<Type, IType>();

        static CSharpClassType()
        {
            typeToPrestoMap[typeof(void)] = VoidType.Instance;
            typeToPrestoMap[typeof(bool)] = BooleanType.Instance;
            typeToPrestoMap[typeof(bool?)] = BooleanType.Instance;
            typeToPrestoMap[typeof(char)] = CharacterType.Instance;
            typeToPrestoMap[typeof(char?)] = CharacterType.Instance;
            typeToPrestoMap[typeof(int)] = IntegerType.Instance;
            typeToPrestoMap[typeof(int?)] = IntegerType.Instance;
            typeToPrestoMap[typeof(long)] = IntegerType.Instance;
            typeToPrestoMap[typeof(long?)] = IntegerType.Instance;
            typeToPrestoMap[typeof(float)] = DecimalType.Instance;
            typeToPrestoMap[typeof(float?)] = DecimalType.Instance;
            typeToPrestoMap[typeof(double)] = DecimalType.Instance;
            typeToPrestoMap[typeof(double?)] = DecimalType.Instance;
            typeToPrestoMap[typeof(string)] = TextType.Instance;
			typeToPrestoMap[typeof(DateTimeOffset)] = DateTimeType.Instance;
			typeToPrestoMap[typeof(DateTimeOffset?)] = DateTimeType.Instance;
			typeToPrestoMap[typeof(object)] = AnyType.Instance;
      }

        internal Type type;

		public CSharpClassType(Type type)
			: base(type.Name)
        {
			this.type = type;
        }

		public override Type ToCSharpType ()
		{
			return this.type;
		}

		public IType ConvertCSharpTypeToPromptoType(Context context, IType returnType )
		{
			return ConvertCSharpTypeToPromptoType (context, type, returnType);
		}


		public IType ConvertCSharpTypeToPromptoType(Context context, Type type, IType returnType )
		{
            IType result;
            typeToPrestoMap.TryGetValue(type, out result);
            if (result != null)
                return result;
			Type item = GetItemTypeFromListType (type);
			if (item != null) {
				IType itemType = ConvertCSharpTypeToPromptoType (context, item, null);
				return new ListType (itemType);
			}
			NativeCategoryDeclaration decl = context.getNativeBinding(type);
			if(decl!=null)
				return decl.GetIType(context);
			else if(returnType==AnyType.Instance)
				return returnType;
            else
                return null;
        }

		public static Type GetItemTypeFromListType(Type type)
		{
			
			if (type.IsGenericType) {
				if (type.GetGenericTypeDefinition () == typeof(List<>))
					return type.GetGenericArguments () [0];
			}
			return null;
		}
    
		public IValue ConvertCSharpValueToPromptoValue(Context context, Object value, IType returnType)
		{
			return ConvertCSharpValueToPromptoValue (context, value, type, returnType);
		}

		public static IValue ConvertCSharpValueToPromptoValue(Context context, Object value, Type type, IType returnType)
		{
			IValue result = ConvertIValue (value);
			if (result != null)
				return result;
			result = ConvertNative (value, type);
			if (result != null)
				return result;
			result = ConvertCategory (context, value, type, returnType);
			if (result != null)
				return result;
			result = ConvertDocument (context, value, type, returnType);
			if (result != null)
				return result;
			result = ConvertList (context, value, type, returnType);
			if (result != null)
				return result;
			result = ConvertSet (context, value, type, returnType);
			if (result != null)
				return result;
			result = ConvertDict (context, value, type, returnType);
			if (result != null)
				return result;
			result = ConvertIterator (context, value, type, returnType);
			if (result != null)
				return result;
			else
				throw new InternalError("Unable to convert:" + value.GetType().Name);
        }

		private static bool IsA(Type expected, Type actual) {
			if (expected.IsAssignableFrom (actual))
				return true;
			if (actual.IsGenericType && expected.IsAssignableFrom (actual.GetGenericTypeDefinition ()))
				return true;
			foreach(Type intf in actual.GetInterfaces()) {
				if (IsA (expected, intf))
					return true;
			}
			if (actual != typeof(Object))
				return IsA (expected, actual.BaseType);
			else
				return false;
		}

		private static IValue ConvertIterator(Context context, Object value, Type type, IType returnType) {
			if (returnType is IteratorType && IsA(typeof(IEnumerator<>), value.GetType())) {
				Type elemType = type.GetGenericArguments () [0];
				IType itemType = ((IteratorType)returnType).GetItemType();
				ConvertingEnumerator converter = new ConvertingEnumerator (context, (IEnumerator<Object>)value, elemType, itemType);
				return new IteratorValue (itemType, converter);
			} else
				return null; 
		}

		private static IValue ConvertDict(Context context, Object value, Type type, IType returnType) {
			if (returnType is DictType && IsA(typeof(Dictionary<,>), value.GetType())) {
				Type keyType = type.GetGenericArguments () [0];
				Type elemType = type.GetGenericArguments () [1];
				IType itemType = ((DictType)returnType).GetItemType();
				Dictionary<Text, IValue> dict = new Dictionary<Text, IValue>();
				foreach(Object obj in ((Dictionary<Object,Object>)value).Keys) {
					Text key = (Text)ConvertCSharpValueToPromptoValue(context, obj, keyType, TextType.Instance);
					Object val = ((Dictionary<Object,Object>)value) [obj];
					IValue ival = ConvertCSharpValueToPromptoValue(context, val, elemType, itemType);
					dict [key] = ival;
				}
				return new Dict(itemType, dict);
			} else
				return null; 
		}

		private static IValue ConvertSet(Context context, Object value, Type type, IType returnType) {
			if (returnType is SetType && IsA(typeof(HashSet<>), value.GetType())) {
				Type elemType = type.GetGenericArguments () [0];
				IType itemType = ((SetType)returnType).GetItemType();
				HashSet<IValue> set = new HashSet<IValue>();
				foreach(Object obj in (IEnumerable)value) {
					IValue val = ConvertCSharpValueToPromptoValue(context, obj, elemType, itemType);
					set.Add(val);
				}
				return new SetValue(itemType, set);
			} else
				return null; 
		}

		private static IValue ConvertList(Context context, Object value, Type type, IType returnType) {
			if (returnType is ListType && IsA(typeof(List<>), value.GetType())) {
				Type elemType = type.GetGenericArguments () [0];
				IType itemType = ((ListType)returnType).GetItemType();
				List<IValue> list = new List<IValue>();
				foreach(Object obj in (IEnumerable)value) {
					IValue val = ConvertCSharpValueToPromptoValue(context, obj, elemType, itemType);
					list.Add(val);
				}
				return new ListValue(itemType, list);
			} else
				return null; 
		}

		private static IValue ConvertDocument(Context context, Object value, Type type, IType returnType) {
			return null; // nothing to do until we have compiled mode
		}

		private static IValue ConvertCategory(Context context, Object value, Type type, IType returnType) {
			NativeCategoryDeclaration decl = context.getNativeBinding(type);
			if(decl!=null)
				return new NativeInstance(decl, value);
			else if(returnType==AnyType.Instance)
				return new NativeInstance(AnyNativeCategoryDeclaration.Instance, value);
			else
				return null;
		}

		private static IValue ConvertNative(Object value, Type type) {
			IType itype;
			if (typeToPrestoMap.TryGetValue (type, out itype))
				return itype.ConvertCSharpValueToPromptoValue (value);
			else
				return null;
		}


		private static IValue ConvertIValue(Object value) {
			if (value is IValue)
				return (IValue)value;
			else
				return null;
		}
    }

	class ConvertingEnumerator : IEnumerator<IValue>
	{
		Context context;
		IEnumerator<Object> source;
		Type itemType;
		IType elemType;
		IValue current;

		public ConvertingEnumerator(Context context, IEnumerator<Object> source, Type itemType, IType elemType)
		{
			this.context = context;
			this.source = source;
			this.itemType = itemType;
			this.elemType = elemType;
		}

		public object Current {
			get {
				return current;
			}
		}

		IValue IEnumerator<IValue>.Current {
			get {
				return current;
			}
		}			

		public bool MoveNext()
		{
			current = null;
			if (!source.MoveNext ())
				return false;
			current = CSharpClassType.ConvertCSharpValueToPromptoValue (context, source.Current, itemType, elemType);
			return true;
		}

		public void Reset()
		{
			source.Reset ();
		}

		public void Dispose()
		{
			source.Dispose ();
		}
	}

}