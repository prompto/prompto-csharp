
using prompto.value;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace prompto.writer {

    public class CSVWriter {

	    public static String write(ListValue docsValue, ListValue headersValue, IDictionary<String, Object> mappings, char? separator, char? encloser)
        {
			IList<String> headers = ListValueToStringList(headersValue);
            CSVWriter writer = new CSVWriter(headers, mappings, separator, encloser);
			IList<IDictionary<String, String>> docs = ListValueToDictionaryList(docsValue);
			return writer.write(docs);
	    }

        private static IList<IDictionary<String, String>> ListValueToDictionaryList(ListValue listValue)
        {
			IList<Object> objs = (IList<Object>)listValue.GetStorableData();
			return objs.Where(val => val is IDictionary<String, IValue>).Select(val => ValueDictToStringDict((IDictionary<String, IValue>)val)).ToList();
        }

        private static IDictionary<String, String> ValueDictToStringDict(IDictionary<string, IValue> val)
        {
            return val.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
        }

        private static IList<string> ListValueToStringList(ListValue listValue)
        {
			IList<Object> objs = (IList<Object>)listValue.GetStorableData();
			return objs.Select(val => val.ToString()).ToList();
	    }

        IList<String> headers;
		IDictionary<String, Object> mappings;
	    String separator;
	    String encloser;
	
	    public CSVWriter(IList<String> headers, IDictionary<String, Object> mappings, char? separator, char? encloser) {
		    this.headers = headers;
		    this.mappings = mappings;
		    this.separator = separator==null ? "," : separator.ToString();
		    this.encloser = encloser==null ? "\"" : encloser.ToString();
	    }

	    private String write(IList<IDictionary<String, String>> docs) {
			StringWriter writer = new StringWriter();
        	write(writer, docs);
			return writer.ToString();
		}

	    private void write(StringWriter writer, IList<IDictionary<String, String>> docs) {
		    IList<String> headers = this.headers;
		    if(mappings!=null)
			    headers = headers.Select(val => mappings.ContainsKey(val) ? mappings[val].ToString() : val).ToList();
            writeRow(writer, headers);
		    foreach(IDictionary<String, String> doc in docs)
			    writeRecord(writer, doc);
		
	    }

	    private void writeRecord(StringWriter writer, IDictionary<String, String> doc) {
			List<String> values = headers
					.Select(header => doc.ContainsKey(header) ? doc[header] : "")
					.ToList();
		    writeRow(writer, values);
	    }

	    private void writeRow(StringWriter writer, IList<String> values) {
		    String row = String.Join(separator, values
					.Select(value => escapeIfRequired(value))
				    .Select(value => encloseIfRequired(value))
				    .ToList());
		    writer.Write(row);
		    writer.Write("\n");
	    }
	
	    private String escapeIfRequired(String value) {
		    return value.Replace(separator, "\\" + separator);
	    }

	    private String encloseIfRequired(String value) {
		    return value.Contains("\n") ? encloser + value.Replace(encloser, "\\" + encloser) + encloser : value;
	    }

    }

}
