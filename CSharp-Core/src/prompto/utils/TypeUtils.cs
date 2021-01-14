using System;
using System.Collections.Generic;
using System.Globalization;
using prompto.value;
using prompto.type;
using prompto.expression;
using prompto.runtime;
using prompto.error;
using prompto.declaration;

namespace prompto.utils
{

    public class TypeUtils
    {

        public static T downcast<T>(Object actual)
        {
            if (actual != null && typeof(T).IsAssignableFrom(actual.GetType()))
                return (T)actual;
            else
                return default(T);
        }

        public static IType InferExpressionsType(Context context, IEnumerable<IExpression> expressions)
        {
            TypeMap types = new TypeMap();
            foreach (IExpression exp in expressions)
                types.add(exp.check(context));
            if (types.Count == 0)
                return MissingType.Instance;
            else
                return types.inferType(context);
        }

        public static IType InferValuesType(Context context, IEnumerable<IValue> values)
        {
            TypeMap types = new TypeMap();
            foreach (IValue value in values)
                types.add(value.GetIType());
            if (types.Count == 0)
                return MissingType.Instance;
            else
                return types.inferType(context);
        }

  
        public static IValue FieldToValue(Context context, String name, Object data)
        {
            if (data == null)
                return null;
            IType type = fieldType(context, name, data);
            return type.ConvertCSharpValueToIValue(context, data);
        }

        private static IType fieldType(Context context, String name, Object data)
        {
            if ("dbId".Equals(name))
                return typeToIType(data.GetType());
            else
            {
                AttributeDeclaration decl = context.getRegisteredDeclaration<AttributeDeclaration>(name);
                return decl.getIType();
            }
        }

        private static Dictionary<Type, IType> createTypeToITypeDict()
        {
            Dictionary<Type, IType> dict = new Dictionary<Type, IType>();
            dict[typeof(void)] = VoidType.Instance;
            dict[typeof(bool)] = BooleanType.Instance;
            dict[typeof(System.Boolean)] = BooleanType.Instance;
            dict[typeof(char)] = CharacterType.Instance;
            dict[typeof(Char)] = CharacterType.Instance;
            dict[typeof(int)] = IntegerType.Instance;
            dict[typeof(Int32)] = IntegerType.Instance;
            dict[typeof(long)] = IntegerType.Instance;
            dict[typeof(Int64)] = IntegerType.Instance;
            dict[typeof(Double)] = DecimalType.Instance;
            dict[typeof(String)] = TextType.Instance;
            dict[typeof(Guid)] = UUIDType.Instance;
            dict[typeof(System.DateTime)] = DateType.Instance; // TODO need a more specific time
            dict[typeof(TimeSpan)] = TimeType.Instance;
            dict[typeof(System.DateTime)] = DateTimeType.Instance;
            dict[typeof(PeriodValue)] = PeriodType.Instance;
            dict[typeof(DocumentValue)] = DocumentType.Instance;
            dict[typeof(Object)] = AnyType.Instance;
            return dict;
        }

        static Dictionary<Type, IType> typeToITypedict = createTypeToITypeDict();


        public static IType typeToIType(Type type)
        {
            return typeToITypedict[type];
        }

    }
}