using System.Collections.Generic;
using System;
using presto.value;
using DateTime = presto.value.DateTime;
using Boolean = System.Boolean;
using presto.error;
using presto.type;
using presto.declaration;

namespace presto.csharp
{
    public class CSharpClassType : CategoryType
    {

        static Dictionary<Type, IType> csharpToTypeMap = new Dictionary<Type, IType>();

        static CSharpClassType()
        {
            csharpToTypeMap[typeof(void)] = VoidType.Instance;
            csharpToTypeMap[typeof(bool)] = BooleanType.Instance;
            csharpToTypeMap[typeof(bool?)] = BooleanType.Instance;
            csharpToTypeMap[typeof(char)] = CharacterType.Instance;
            csharpToTypeMap[typeof(char?)] = CharacterType.Instance;
            csharpToTypeMap[typeof(int)] = IntegerType.Instance;
            csharpToTypeMap[typeof(int?)] = IntegerType.Instance;
            csharpToTypeMap[typeof(long)] = IntegerType.Instance;
            csharpToTypeMap[typeof(long?)] = IntegerType.Instance;
            csharpToTypeMap[typeof(float)] = DecimalType.Instance;
            csharpToTypeMap[typeof(float?)] = DecimalType.Instance;
            csharpToTypeMap[typeof(double)] = DecimalType.Instance;
            csharpToTypeMap[typeof(double?)] = DecimalType.Instance;
            csharpToTypeMap[typeof(string)] = TextType.Instance;
            csharpToTypeMap[typeof(Date)] = DateType.Instance;
            csharpToTypeMap[typeof(Time)] = TimeType.Instance;
            csharpToTypeMap[typeof(DateTime)] = DateTimeType.Instance;
            csharpToTypeMap[typeof(Period)] = PeriodType.Instance;
            csharpToTypeMap[typeof(object)] = AnyType.Instance;
        }

        internal Type klass;

        public CSharpClassType(Type klass)
            : base(klass.Name)
        {
            this.klass = klass;
        }

		public override Type ToSystemType ()
		{
			return this.klass;
		}

        public IType convertSystemTypeToPrestoType()
        {
            IType result;
            csharpToTypeMap.TryGetValue(klass, out result);
            if (result == null)
                return this;
            else
                return result;
        }

    
		public IValue convertSystemValueToPrestoValue(Object value, IType returnType)
        {
            if(value is IValue)
                return (IValue)value;
            IType result;
            csharpToTypeMap.TryGetValue(klass, out result);
            if (result != null)
                return result.convertSystemValueToPrestoValue(value);
			else if(returnType==AnyType.Instance)
				return new NativeInstance(AnyNativeCategoryDeclaration.Instance, value);
            else
                throw new InternalError("Unable to convert:" + value.GetType().Name);
        }
    }
}