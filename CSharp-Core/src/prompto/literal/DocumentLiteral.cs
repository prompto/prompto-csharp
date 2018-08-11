using prompto.runtime;
using prompto.error;
using prompto.value;
using prompto.type;

namespace prompto.literal
{

	public class DocumentLiteral : Literal<Document>
	{

		// we can only compute keys by evaluating key expressions
		// so we can't just inherit from Document
		// so we keep the full entry list.
		DocEntryList entries;

		public DocumentLiteral()
			: base("{}", new Document())
		{
			this.entries = new DocEntryList();
		}

		public DocumentLiteral(DocEntryList entries)
			: base(entries.ToString(), new Document())
		{
			this.entries = entries;
		}


		public override IType check(Context context)
		{
			return DocumentType.Instance;
		}


		public override IValue interpret(Context context)
		{
			if (entries.Count > 0)
			{
				check(context); // to compute itemType
				Document doc = new Document();
				foreach (DictEntry e in entries)
				{
					Text key = e.getKey().asText();
					IValue val = e.getValue().interpret(context);
					doc.SetMember(key.Value, val);
				}
				return doc;
			}
			else
				return value;
		}

	}

}
