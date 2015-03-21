using System;
using System.Collections.Generic;
using System.Globalization;
using presto.value;
using presto.type;
using presto.expression;
using presto.runtime;
using presto.error;

namespace presto.utils
{

    public class Utils
    {

        public static bool equal(Object o1, Object o2)
        {
            if (o1 == o2)
                return true;
            if (o1 == null || o2 == null)
                return false;
            return o1.Equals(o2);
        }

        public static String capitalizeFirst(String value)
        {
            return Char.ToUpper(value[0]) + value.Substring(1);
        }

        public static Character[] stringToCharacterArray(String value)
        {
            char[] chars = value.ToCharArray();
            List<Character> list = new List<Character>(chars.Length);
            for (int i = 0; i < chars.Length; i++)
                list.Add(new Character(chars[i]));
            return list.ToArray();
        }

        public static T downcast<T>(Object actual)
        {
            if (actual != null && typeof(T).IsAssignableFrom(actual.GetType()))
                return (T)actual;
            else
                return default(T);
        }

        public static bool EqualDictionaries<K, V>(Dictionary<K, V> d1, Dictionary<K, V> d2)
        {
            if (d1.Keys.Count != d2.Keys.Count)
                return false;
            foreach (KeyValuePair<K, V> kvp in d1)
            {
                V v2;
                if (!d2.TryGetValue(kvp.Key, out v2))
                    return false;
                if (!v2.Equals(kvp.Value))
                    return false;
            }
            return true;
        }

		public static IType InferElementType(Context context, IEnumerable<IExpression> expressions)
		{
			List<IType> types = new List<IType> ();
			foreach (IExpression exp in expressions)
				types.Add (exp.check(context));
			return InferElementType (context, types);
		}

		public static IType InferElementType(Context context, IEnumerable<IValue> values)
		{
			List<IType> types = new List<IType> ();
			foreach (IValue value in values)
				types.Add (value.GetType(context));
			return InferElementType (context, types);
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
					if (type.isAssignableTo(context, lastType))
					{
						// lastType is less specific
					}
					else if (lastType.isAssignableTo(context, type))
						lastType = type; // elemType is less specific
					else
						throw new SyntaxError("Incompatible types: " + type.ToString() + " and " + lastType.ToString());
				}
			}
			return lastType;
		}
    }
}