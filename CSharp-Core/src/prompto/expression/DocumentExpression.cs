using prompto.error;
using prompto.runtime;
using System;
using prompto.parser;
using prompto.type;
using prompto.value;
using prompto.utils;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using ICSharpCode.SharpZipLib.Zip;

namespace prompto.expression
{

    public class DocumentExpression : IExpression
    {
		IExpression source;

		public DocumentExpression(IExpression source)
		{
			this.source = source;
		}

        public IType check(Context context)
        {
			if (source != null)
				source.check (context);
            return DocumentType.Instance;
        }

		public IValue interpret(Context context)
        {
			if (source == null)
				return new Document ();
			else {
				IValue value = source.interpret (context);
				return documentFromValue (context, value);
			}
        }

		private Document documentFromValue(Context context, IValue value) {
			if(value is Blob)
				return documentFromBlob(context, (Blob)value);
			else
				throw new NotSupportedException();
		}

		private Document documentFromBlob(Context context, Blob blob) {
			if("application/zip"!=blob.MimeType)
				throw new NotSupportedException();
			try {
				Dictionary<String, byte[]> parts = ReadParts(blob.Data);
				JObject value = ReadValue(parts);
				JToken token;
				if(!value.TryGetValue("type", out token))
					throw new InvalidDataException("Expecting a 'type' field!");
				IType type = new ECleverParser(token.ToString()).parse_standalone_type();
				if(type!=DocumentType.Instance)
					throw new InvalidDataException("Expecting a Document type!");
				if(!value.TryGetValue("value", out token))
					throw new InvalidDataException("Expecting a 'value' field!");
				return (Document)type.ReadJSONValue(context, token, parts);
			} catch(Exception e) {
				throw new ReadWriteError(e.Message);
			}
		}

		public static Dictionary<String, byte[]> ReadParts(byte[] data)
		{
			Dictionary<String, byte[]> binaries = new Dictionary<String, byte[]>();
			using(MemoryStream input = new MemoryStream(data)) {
				using (ZipInputStream zip = new ZipInputStream(input)) {
					zip.IsStreamOwner = false;
					for(;;) {
						ZipEntry entry = zip.GetNextEntry();
						if(entry==null)
							break;
						data = IOUtils.ReadStreamFully(zip);
						binaries[entry.Name] = data;
						zip.CloseEntry();
					}
					return binaries;
				}
			} 
		}


		public static JObject ReadValue(Dictionary<String, byte[]> parts)
		{
			byte[] data;
			if(!parts.TryGetValue("value.json", out data))
				throw new InvalidDataException("Expecting a 'value.json' part!");
			String json = System.Text.Encoding.UTF8.GetString (data);
			dynamic obj = JsonConvert.DeserializeObject(json);
			if (obj is JObject)
				return (JObject)obj;
			else
				throw new InvalidDataException("Expecting a JSON object!");
		}

        public void ToDialect(CodeWriter writer)
        {
			writer.append("Document");
			switch(writer.getDialect()) {
			case Dialect.E:
				if(source!=null) {
					writer.append(" from ");
					source.ToDialect(writer);
				}
				break;
			case Dialect.O:
			case Dialect.M:
				writer.append('(');
				if(source!=null)
					source.ToDialect(writer);
				writer.append(')');
				break;
			}
        }
    }

}