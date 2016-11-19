using System;
using prompto.type;
using prompto.runtime;
using prompto.value;
using System.IO;
using prompto.error;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using prompto.utils;
using prompto.parser;
using ICSharpCode.SharpZipLib.Zip;

namespace prompto.expression
{
	public class BlobExpression : IExpression
	{
		IExpression source;

		public BlobExpression (IExpression source)
		{
			this.source = source;
		}

		public IType check(Context context)
		{
			source.check (context);
			return BlobType.Instance;
		}

		public void ToDialect(CodeWriter writer) {
			writer.append("Blob");
			switch(writer.getDialect()) {
			case Dialect.E:
				writer.append(" from ");
				source.ToDialect(writer);
				break;
			case Dialect.O:
			case Dialect.M:
				writer.append('(');
				source.ToDialect(writer);
				writer.append(')');
				break;
			}
		}

		public IValue interpret(Context context)
		{
			IValue value = source.interpret (context);
			try {
				Dictionary<String, byte[]> datas = CollectData(context, value);
				byte[] zipped = ZipData(datas);
				return new Blob("application/zip", zipped);
			} catch(IOException e) {
				throw new ReadWriteError(e.Message);
			}
		}

		private Dictionary<String, byte[]> CollectData(Context context, IValue value) {
			Dictionary<String, byte[]> binaries = new Dictionary<String, byte[]>();
			// create textual data
			using(MemoryStream stream = new MemoryStream()) {
				using (TextWriter text = new StreamWriter(stream)) {
					using (JsonWriter writer = new JsonTextWriter (text)) {
						value.ToJson (context, writer, null, null, binaries);
					}
				}
				binaries ["value.json"] = stream.ToArray();
				return binaries;
			}
		}

		private byte[] ZipData(Dictionary<String, byte[]> datas) {
			using(MemoryStream output = new MemoryStream()) {
				using(ZipOutputStream zip = new ZipOutputStream(output)) {
					zip.IsStreamOwner = false;
					foreach(KeyValuePair<String, byte[]> part in datas) {
						ZipEntry entry = new ZipEntry(part.Key);
						zip.PutNextEntry(entry);
						zip.Write(part.Value, 0, part.Value.Length);
						zip.CloseEntry();
					}
					zip.Flush ();
				}
				output.Flush ();
				return output.ToArray();
			}
		}
	}
}

