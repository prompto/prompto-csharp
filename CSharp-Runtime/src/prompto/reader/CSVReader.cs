using prompto.value;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

namespace prompto.reader
{
	
	public abstract class CSVReader {

		public static List<Document> read(String data, IDictionary<String, Object> columns, char? separator, char? encloser)
		{
			StringReader reader = data==null ? null : new StringReader(data);
			return read(reader, columns, separator, encloser);
		}

		public static List<Document> read(StringReader reader, IDictionary<String, Object> columns, char? separator, char? encloser)
		{
			List<Document> list = new List<Document> ();
			IEnumerator<Document> iter = iterator(reader, columns, separator, encloser);
			while (iter.MoveNext ())
				list.Add (iter.Current);
			return list;
		}

		public static IEnumerator<Document> iterator(String data, IDictionary<String, Object> columns, char? separator, char? encloser)
		{
			StringReader reader = data==null ? null : new StringReader(data);
			return iterator(reader, columns, separator, encloser);
		}

		public static IEnumerator<Document> iterator (StringReader reader, IDictionary<String, Object> columns, char? separator, char? encloser)
		{
			char sep = separator.GetValueOrDefault (',');
			char quote = encloser.GetValueOrDefault ('"');

			return new CsvDocumentIterator (reader, columns, sep, quote);
		}
	
	}

	class CsvDocumentIterator : IEnumerator<Document> {

		StringReader reader;
		IDictionary<String, Object> columns;
		char sep;
		char quote;
		List<String> headers = null;
		String nextLine;
		Document current;


		public CsvDocumentIterator(StringReader reader, IDictionary<String, Object> columns, char sep, char quote)
		{
			this.reader = reader;
			this.columns = columns;
			this.sep = sep;
			this.quote = quote;
		}

		public object Current
		{
			get
			{
				return MakeCurrent();
			}
		}

		Document IEnumerator<Document>.Current
		{
			get { return MakeCurrent(); }
		}	

		public void Reset()
		{
			throw new NotSupportedException ();
		}

		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			nextLine = null;
			current = null;
			if(headers==null)
				ParseHeaders();
			if(nextLine==null)
				nextLine = NextLine();
			return nextLine!=null;
		}

		private String NextLine() {
			if(reader==null)
				return null;
			String line = reader.ReadLine();
			if(line!=null)
				return line;
			reader.Close();
			reader = null;
			return line;
		}

		private void ParseHeaders() {
			String line = NextLine();
			if(line!=null)
				headers = ParseLine(line);
			if (columns != null) {
				for (int i = 0; i < headers.Count; i++) {
					Object column;
					if (columns.TryGetValue (headers [i], out column))
						headers [i] = column.ToString();
				}
			}
		}

		private List<String> ParseLine(String line) {
			List<String> list = new List<String>();
			char[] chars = line.ToCharArray ();
			int nextIdx = 0;
			while(nextIdx<chars.Length)
				nextIdx = ParseValue(chars, nextIdx, list);
			return list;
		}

		private int ParseValue(char[] chars, int startIdx, List<String> list) {
			if(chars[startIdx]==sep) {
				list.Add(null);
				return startIdx + 1;
			} else if(chars[startIdx]==quote)
				return ParseQuotedValue(chars, startIdx + 1, list);
			else 
				return ParseUnquotedValue(chars, startIdx, list);
		}

		private int ParseQuotedValue(char[] chars, int startIdx, List<String> list) {
			int endIdx = ParseValue(chars, startIdx, quote, list);
			// look for next sep
			while(endIdx<chars.Length && chars[endIdx]!=sep)
				endIdx++;
			return endIdx + 1;
		}

		private int ParseUnquotedValue(char[] chars, int startIdx, List<String> list) {
			return ParseValue(chars, startIdx, sep, list);
		}

		private int ParseValue(char[] chars, int startIdx, char endChar, List<String> list) {
			bool escape = false;
			bool found = false;
			int endIdx = startIdx;
			while(endIdx<chars.Length) {
				if(chars[endIdx]==endChar) {
					if (endIdx < chars.Length - 1 && chars [endIdx + 1] == endChar) {
						escape = true;
						endIdx++;
					} else {
						found = true;
						break;
					}
				}
				if(chars[endIdx]=='\\') {
					escape = true;
					endIdx++;
				}
				if(endIdx<=chars.Length)
					endIdx++;
			}
			String value = escape ? 
				Unescape(chars, startIdx, endIdx, endChar) :
				new String(chars, startIdx, endIdx - startIdx);
			list.Add(value);
			return endIdx + (found ? 1 : 0); 
		}


		private String Unescape(char[] chars, int startIdx, int endIdx, char endChar) {
			StringBuilder sb = new StringBuilder();
			while(startIdx<endIdx) {
				if(chars[startIdx]=='\\')
					startIdx++;
				else if(chars[startIdx]==endChar && startIdx<endIdx-1 && chars[startIdx+1]==endChar)
					startIdx++;
				if(startIdx<endIdx)
					sb.Append(chars[startIdx++]);
			}
			return sb.ToString();
		}

		public Document MakeCurrent() {
			if(current!=null)
				return current;
			if (nextLine == null)
				return null;
			String line = nextLine;
			nextLine = null;
			List<String> values = ParseLine(line);
			Document doc = new Document();
			for(int i=0;i<headers.Count;i++) {
				if (i < values.Count) {
					String strval = values [i];
					IValue value = strval == null ? (IValue)NullValue.Instance : (IValue)new Text (strval);
					doc.SetMember (headers [i], value);
				}
				else
					doc.SetMember(headers[i], NullValue.Instance);
			}
			return doc;
		}
	}

}
