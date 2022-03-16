using System;
using System.Xml;
using System.Linq;
using prompto.type;
using prompto.value;

namespace prompto.reader
{
    public class XMLReader
    {
        public static DocumentValue read(String xml, bool keepNamespace, bool keepAttributes)
        {
            return new XMLReader(keepNamespace, keepAttributes).Read(xml);
        }

 
        bool keepNamespace;
        bool keepAttributes;

        XMLReader(bool keepNamespace, bool keepAttributes)
        {
            this.keepNamespace = keepNamespace;
            this.keepAttributes = keepAttributes;
        }

        private DocumentValue Read(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return ConvertDocument(doc);
        }

        private DocumentValue ConvertDocument(XmlDocument doc)
        {
            var result = new DocumentValue();
            ConvertElement(result, doc.DocumentElement);
            return result;
        }

        private void ConvertElement(DocumentValue parent, XmlElement element)
        {
            var tagName = element.Name;
            // cater for repeated elements
            if (parent.HasMember(tagName))
                ConvertListElement(parent, tagName, element);
            else
                parent.SetMember(tagName, ConvertElementValue(element));
        }

        private void ConvertListElement(DocumentValue parent, String tagName, XmlElement element)
        {
            ListValue list = null;
            var current = parent.GetMember(tagName, false);
            if (current is ListValue)
			    list = (ListValue)current;
            else {
                list = new ListValue(AnyType.Instance);
                list.Add(current);
                parent.SetMember(tagName, list);
            }
            list.Add(ConvertElementValue(element));
        }

        private IValue ConvertElementValue(XmlElement element)
        {
            var hasAttributes = keepAttributes && element.Attributes.Count > 0;
            var hasChildren = ElementHasChildren(element);
            if (hasAttributes || hasChildren)
            {
                DocumentValue result = new DocumentValue();
                if (keepAttributes)
                {
                    foreach (XmlAttribute a in element.Attributes) {
                        result.SetMember("@" + a.Name, new TextValue(a.InnerText));
                    }
                }
                if (hasChildren)
                {
                    foreach (var node in element.ChildNodes)
                    {
                        if (node is XmlElement)
                            ConvertElement(result, (XmlElement)node);
                    }
                }
                else
                    result.SetMember("$value", new TextValue(element.InnerText));
                return result;
            }
            else
                return new TextValue(element.InnerText);
        }

        private bool ElementHasChildren(XmlElement element)
        {
            foreach (var node in element.ChildNodes)
            {
                if (node is XmlElement)
                    return true;
            }
            return false;
        }
    }
}
