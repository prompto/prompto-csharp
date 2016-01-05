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
				return decl.GetType(context);
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
			// try IValue
			if(value is IValue)
                return (IValue)value;
			// try native
            IType result;
            typeToPrestoMap.TryGetValue(type, out result);
            if (result != null)
				return result.ConvertCSharpValueToPromptoValue(value);
			// try List
			if (value.GetType().IsGenericType && typeof(List<>).IsAssignableFrom(value.GetType().GetGenericTypeDefinition()) && returnType is ListType) {
				Type elemType = GetItemTypeFromListType (type);
				IType itemType = ((ListType)returnType).GetItemType();
				List<IValue> list = new List<IValue>();
				foreach(Object obj in (IEnumerable)value) {
					IValue val = ConvertCSharpValueToPromptoValue(context, obj, elemType, itemType);
					list.Add(val);
				}
				return new ListValue(itemType, list);

			}
			// try Category
			NativeCategoryDeclaration decl = context.getNativeBinding(type);
			if(decl!=null)
				return new NativeInstance(decl, value);
			else if(returnType==AnyType.Instance)
				return new NativeInstance(AnyNativeCategoryDeclaration.Instance, value);
            else
                throw new InternalError("Unable to convert:" + value.GetType().Name);
        }
    }
}