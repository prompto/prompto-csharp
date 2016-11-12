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

		public static IType InferElementType(Context context, IEnumerable<IExpression> expressions)
		{
			List<IType> types = new List<IType>();
			foreach (IExpression exp in expressions)
				types.Add(exp.check(context));
			return InferElementType(context, types);
		}

		public static IType InferElementType(Context context, IEnumerable<IValue> values)
		{
			List<IType> types = new List<IType>();
			foreach (IValue value in values)
				types.Add(value.GetIType());
			return InferElementType(context, types);
		}

		public static IType InferElementType(Context context, List<IType> types)
		{
			if (types.Count == 0)
				return MissingType.Instance;
			IType lastType = null;
			foreach (IType type in types)
			{
				if (lastType == null)
					lastType = type;
				else if (!lastType.Equals(type))
				{
					if (lastType.isAssignableFrom(context, type))
					{
						// lastType is less specific
					}
					else if (type.isAssignableFrom(context, lastType))
						lastType = type; // elemType is less specific
					else {
						IType common = inferCommonRootType(context, lastType, type);
						if (common != null)
							lastType = common;
						else
							throw new SyntaxError("Incompatible types: " + type.ToString() + " and " + lastType.ToString());
					}
				}
			}
			return lastType;
		}


		private static IType inferCommonRootType(Context context, IType type1, IType type2)
		{
			if (type1 is CategoryType && type2 is CategoryType)
				return inferCommonRootType(context, (CategoryType)type1, (CategoryType)type2, true);
			else
				return null;
		}


		private static IType inferCommonRootType(Context context, CategoryType type1, CategoryType type2, bool trySwap)
		{
			CategoryDeclaration decl1 = context.getRegisteredDeclaration<CategoryDeclaration>(type1.GetTypeName());
			if (decl1.getDerivedFrom() != null)
			{
				foreach (String name in decl1.getDerivedFrom())
				{
					CategoryType parentType = new CategoryType(name);
					if (parentType.isAssignableFrom(context, type2))
						return parentType;
				}
				// climb up the tree
				foreach (String name in decl1.getDerivedFrom())
				{
					CategoryType parentType = new CategoryType(name);
					IType commonType = inferCommonRootType(context, parentType, type2);
					if (commonType != null)
						return commonType;
				}
			}
			if (trySwap)
				return inferCommonRootType(context, type2, type1, false);
			else
				return null;
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
			else {
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
			dict[typeof(Period)] = PeriodType.Instance;
			dict[typeof(Document)] = DocumentType.Instance;
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