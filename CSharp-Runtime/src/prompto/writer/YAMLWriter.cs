using System;
using prompto.value;
using System.Collections.Generic;
using System.Linq;

namespace prompto.writer
{
    public abstract class YAMLWriter
    {
        public static string write(ListValue value)
        {
            IList<object> docs = convertList(value);
            var writer = new YamlDotNet.Serialization.Serializer();
            return writer.Serialize(docs);
        }

        private static object convert(IValue value)
        {
            if (value == null || value == NullValue.Instance)
                return null;
            else if (value is ListValue)
                return convertList((ListValue)value);
            else if (value is DictValue)
                return convertDictionary((DictValue)value);
            else if (value is DocumentValue)
                return convertDocument((DocumentValue)value);
            else
            {
                object data = value.GetStorableData();
                return data.ToString();
            }
        }

        private static IList<object> convertList(ListValue list)
        {
            return list.Select(val => convert(val)).ToList();
        }

        private static IDictionary<object, object> convertDictionary(DictValue value)
        {
            IDictionary<TextValue, IValue> dict = value;
            return dict.ToDictionary(x => convertText(x.Key), x => convert(x.Value));
        }

        private static IDictionary<object, object> convertDocument(DocumentValue value)
        {
            IDictionary<string, IValue> dict = (IDictionary<string, IValue>)value.GetStorableData();
            return dict.ToDictionary(x => (object)x.Key, x => convert(x.Value));
        }


        private static object convertText(TextValue text)
        {
            return text.Value;
        }

    }
}
