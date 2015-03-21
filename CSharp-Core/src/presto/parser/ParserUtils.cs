using System.Collections.Generic;
using System;
using System.Reflection;

namespace presto.parser
{

    public class ParserUtils
    {

        public static Dictionary<int, String> extractTokenNames(Type klass)
        {
            Dictionary<int, String> result = new Dictionary<int, String>();
            foreach (FieldInfo f in klass.GetFields())
            {
                if (f.FieldType != typeof(Int32))
                    continue;
                if (!f.Name.Equals(f.Name.ToUpper()))
                    continue;
                FieldAttributes mask = FieldAttributes.Public | FieldAttributes.Static | FieldAttributes.Literal;
                if ((f.Attributes & mask) != mask)
                    continue;
                Int32 value = (Int32)f.GetValue(null);
                result[value] = f.Name;
            }
            return result;
        }
    }
}
