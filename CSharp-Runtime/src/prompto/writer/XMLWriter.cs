using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace prompto.writer
{
    public class XMLWriter
    {
        public static string write(IDictionary<string, object> doc)
        {
            XmlDocument document = new XMLWriter().ConvertToDocument(doc);
            return DocumentDoString(document);
        }

       private static string DocumentDoString(XmlDocument document)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XmlDocument));
            TextWriter writer = new StringWriter();
            serializer.Serialize(writer, document);
            writer.Close();
            return writer.ToString();
        }

        XmlDocument document;

        private XmlDocument ConvertToDocument(IDictionary<string, object> doc)
        {
            var keys = doc.Keys.Where(s => !s.StartsWith("@")).ToList();
            if (keys.Count != 1)
                throw new InvalidDataException("Document must have a single root element");
            document = new XmlDocument();
            XmlElement root = ConvertRootDocument(keys[0], doc);
            document.AppendChild(root);
            return document;
        }

        private XmlElement ConvertRootDocument(string tagName, IDictionary<string, object> doc)
        {
            XmlElement element = document.CreateElement(tagName);
            SetElementAttributes(element, doc);
            ConvertValue(element, doc[tagName]);
            return element;
        }

        private void SetElementAttributes(XmlElement element, IDictionary<string, object> doc)
        {
            foreach (var key in doc.Keys.Where(s => s.StartsWith("@")))
                element.SetAttribute(key, doc[key].ToString());
        }

        private void ConvertValue(XmlElement parent, object value)
        {
            if (value == null)
                return;
            else if (value is IList<object>)
            {
                foreach(var item in (IList<object>)value)
                {
                    ConvertValue(parent, item);
                }
            }
            else if (value is ISet<object>)
            {
                foreach (var item in (ISet<object>)value)
                {
                    ConvertValue(parent, item);
                }
            }
            else if(value is IDictionary<string, object>)
                ConvertDictOrDocument(parent, (IDictionary<string, object>)value);

            else
                parent.InnerText = value.ToString();
        }

        private void ConvertDictOrDocument(XmlElement parent, IDictionary<string, object> value)
        {
            SetElementAttributes(parent, value);
            List<string> children = value.Keys.Where(s => !s.StartsWith("@") && !s.Equals("$value")).ToList();
            if (children.Count > 0)
            {
                foreach (var key in children)
                {
                    XmlElement element = document.CreateElement(key);
                    ConvertValue(element, value[key]);
                    parent.AppendChild(element);
                }
            }
            else
                parent.InnerText = value["$value"].ToString();
        }

    }
}
