using System;
using System.Collections.Generic;
using System.Globalization;
using prompto.value;
using prompto.type;
using prompto.expression;
using prompto.runtime;
using prompto.error;

namespace prompto.utils
{

    public class ObjectUtils
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

    }
}