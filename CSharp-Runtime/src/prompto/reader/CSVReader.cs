﻿using prompto.value;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Linq;

namespace prompto.reader
{

    public abstract class CSVReader
    {

        public static List<String> readHeaders(String data, char? separator, char? encloser)
        {
            char sep = separator.GetValueOrDefault(',');
            char quote = encloser.GetValueOrDefault('"');
            StringReader reader = data == null ? null : new StringReader(data);
            CsvDocumentIterator iterator = new CsvDocumentIterator(reader, null, sep, quote);
            return iterator.ReadHeaders();
        }

        public static List<DocumentValue> read(String data, IDictionary<String, Object> columns, char? separator, char? encloser)
        {
            StringReader reader = data == null ? null : new StringReader(data);
            return read(reader, columns, separator, encloser);
        }

        public static List<DocumentValue> read(StringReader reader, IDictionary<String, Object> columns, char? separator, char? encloser)
        {
            List<DocumentValue> list = new List<DocumentValue>();
            IEnumerator<DocumentValue> iter = iterator(reader, columns, separator, encloser);
            while (iter.MoveNext())
                list.Add(iter.Current);
            return list;
        }

        public static IEnumerator<DocumentValue> iterator(String data, IDictionary<String, Object> columns, char? separator, char? encloser)
        {
            StringReader reader = data == null ? null : new StringReader(data);
            return iterator(reader, columns, separator, encloser);
        }

        public static IEnumerator<DocumentValue> iterator(StringReader reader, IDictionary<String, Object> columns, char? separator, char? encloser)
        {
            char sep = separator.GetValueOrDefault(',');
            char quote = encloser.GetValueOrDefault('"');

            return new CsvDocumentIterator(reader, columns, sep, quote);
        }

    }

    class CsvDocumentIterator : IEnumerator<DocumentValue>
    {

        StringReader reader;
        IDictionary<String, Object> columns;
        char sep;
        char quote;
        List<String> headers = null;
        Int32? peekedChar;
        Int32 nextChar = 0;
        DocumentValue current;


        public CsvDocumentIterator(StringReader reader, IDictionary<String, Object> columns, char sep, char quote)
        {
            this.reader = reader;
            this.columns = columns;
            this.sep = sep;
            this.quote = quote;
        }

        public List<String> ReadHeaders()
        {
            if (nextChar == 0)
                FetchChar(true);
            if (headers == null)
                ParseHeaders();
            return headers!=null ? headers : new List<String>();
        }

        public object Current
        {
            get
            {
                return MakeCurrent();
            }
        }

        DocumentValue IEnumerator<DocumentValue>.Current
        {
            get { return MakeCurrent(); }
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            current = null;
            if (nextChar == 0)
                FetchChar(true);
            if (headers == null)
                ParseHeaders();
            return nextChar > 0;
        }


        private void FetchChar()
        {
            FetchChar(false);
        }

        private void FetchChar(bool eatNewLine)
        {
            if (reader == null)
                nextChar = -1; // EOF
            else if (peekedChar != null)
            {
                int c = peekedChar.Value;
                peekedChar = null;
                nextChar = c;
            }
            else
            {
                int c = reader.Read();
                if (c == '\r')
                    FetchChar(eatNewLine);
                else if (eatNewLine && (c == '\n'))
                    FetchChar(eatNewLine);
                else
                    nextChar = c;
            }
        }

        private int PeekChar()
        {
            if (peekedChar == null)
            {
                int oldChar = nextChar;
                FetchChar();
                peekedChar = nextChar;
                nextChar = oldChar;
            }
            return peekedChar.Value;
        }


        private void ParseHeaders()
        {
            headers = ParseLine();
            if (columns != null)
            {
                for (int i = 0; i < headers.Count; i++)
                {
                    Object column;
                    if (columns.TryGetValue(headers[i], out column))
                        headers[i] = column.ToString();
                }
            }
        }

        private List<String> ParseLine()
        {
            List<String> list = new List<String>();
            while (ParseValue(list))
                ;
            if (nextChar == '\n')
                FetchChar();
            return list;
        }

        private bool ParseValue(List<String> list)
        {
            if (nextChar == sep)
                ParseEmptyValue(list);
            else if (nextChar == quote)
                ParseQuotedValue(list);
            else
                ParseUnquotedValue(list);
            return nextChar != -1 && nextChar != '\n';
        }


        private void ParseEmptyValue(List<String> list)
        {
            list.Add(null);
            FetchChar();
        }

        private void ParseQuotedValue(List<String> list)
        {
            FetchChar(); // consume the leading double quote
            ParseValue(quote, list);
            // look for next sep
            while (nextChar != sep && nextChar != -1 && nextChar != '\n')
                FetchChar();
            if (nextChar == sep)
                FetchChar();
        }

        private void ParseUnquotedValue(List<String> list)
        {
            ParseValue(sep, list);
        }

        private void ParseValue(char endChar, List<String> list)
        {
            StringBuilder sb = new StringBuilder();
            bool exit = false;
            for (; ; )
            {
                if (nextChar == -1)
                    exit = HandleEOF(sb, endChar, list);
                else if (nextChar == '\n')
                    exit = HandleNewLine(sb, endChar, list);
                else if (nextChar == endChar)
                    exit = HandleEndChar(sb, endChar, list);
                else if (nextChar == '\\')
                    exit = HandleEscape(sb, endChar, list);
                else
                    exit = HandleOtherChar(sb, endChar, list);
                if (exit)
                {
                    if (sb.Length > 0)
                        list.Add(sb.ToString());
                    return;
                }
            }
        }

        private bool HandleOtherChar(StringBuilder sb, char endChar, List<String> list)
        {
            sb.Append((char)nextChar);
            FetchChar();
            return false;
        }

        private bool HandleEscape(StringBuilder sb, char endChar, List<String> list)
        {
            if (PeekChar() != -1)
            {
                sb.Append((char)PeekChar());
                FetchChar();
            }
            FetchChar();
            return false;
        }

        private bool HandleEOF(StringBuilder sb, char endChar, List<String> list)
        {
            return true;
        }

        private bool HandleEndChar(StringBuilder sb, char endChar, List<String> list)
        {
            if (endChar == '"' && PeekChar() == endChar)
            {
                sb.Append((char)nextChar);
                FetchChar();
                FetchChar();
                return false;
            }
            else
            {
                FetchChar();
                return true;
            }
        }

        private bool HandleNewLine(StringBuilder sb, char endChar, List<String> list)
        {
            if (endChar == '"')
            {
                sb.Append((char)nextChar);
                FetchChar();
                return false;
            }
            else
            {
                return true;
            }
        }

        public DocumentValue MakeCurrent()
        {
            if (current != null)
                return current;
            if (nextChar == -1)
                return null;
            List<String> values = ParseLine();
            DocumentValue doc = new DocumentValue();
            for (int i = 0; i < headers.Count; i++)
            {
                if (i < values.Count)
                {
                    String strval = values[i];
                    IValue value = strval == null ? (IValue)NullValue.Instance : (IValue)new TextValue(strval);
                    doc.SetMember(headers[i], value);
                }
                else
                    doc.SetMember(headers[i], NullValue.Instance);
            }
            return doc;
        }
    }

}
