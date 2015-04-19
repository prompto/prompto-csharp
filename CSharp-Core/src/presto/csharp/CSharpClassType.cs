using System.Collections.Generic;
using System;
using presto.value;
using DateTime = presto.value.DateTime;
using Boolean = System.Boolean;
using presto.error;
using presto.type;
using presto.declaration;
using presto.runtime;

namespace presto.csharp
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
            typeToPrestoMap[typeof(Date)] = DateType.Instance;
            typeToPrestoMap[typeof(Time)] = TimeType.Instance;
            typeToPrestoMap[typeof(DateTime)] = DateTimeType.Instance;
            typeToPrestoMap[typeof(Period)] = PeriodType.Instance;
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

		public IType ConvertCSharpTypeToPrestoType(Context context, IType returnType )
        {
            IType result;
            typeToPrestoMap.TryGetValue(type, out result);
            if (result != null)
                return result;
			NativeCategoryDeclaration decl = context.getNativeBinding(type);
			if(decl!=null)
				return decl.GetType(context);
			else if(returnType==AnyType.Instance)
				return returnType;
            else
                return null;
        }

    
		public IValue ConvertCSharpValueToPrestoValue(Context context, Object value, IType returnType)
        {
            if(value is IValue)
                return (IValue)value;
            IType result;
            typeToPrestoMap.TryGetValue(type, out result);
            if (result != null)
				return result.ConvertCSharpValueToPrestoValue(value);
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