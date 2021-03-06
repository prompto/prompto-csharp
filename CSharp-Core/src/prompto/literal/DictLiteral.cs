using prompto.runtime;
using prompto.error;
using System;
using prompto.value;
using prompto.type;
using System.Collections.Generic;
using prompto.utils;

namespace prompto.literal
{

	public class DictLiteral : Literal<DictValue>
	{

		// we can only compute keys by evaluating key expressions
		// so we can't just inherit from Dictionary<String,Expression>.
		// so we keep the full entry list.
		bool mutable = false;
		DictEntryList entries;
		IType itemType = null;

		public DictLiteral (bool mutable)
			: base ("<:>", new DictValue (MissingType.Instance, mutable))
		{
			this.entries = new DictEntryList ();
			this.mutable = mutable;
		}

		public DictLiteral (DictEntryList entries, bool mutable)
			: base (entries.ToString (), new DictValue (MissingType.Instance, mutable))
		{
			this.entries = entries;
			this.mutable = mutable;
		}

       
		public override IType check (Context context)
		{
			if (itemType == null)
				itemType = inferElementType (context);
			return new DictType (itemType);
		}

		private IType inferElementType (Context context)
		{
			if (entries.Count == 0)
				return MissingType.Instance;
			TypeMap types = new TypeMap();
			foreach (DictEntry e in entries)
			{
				types.add(e.GetValue().check(context));
			}
			return types.inferType(context);
		}

        
		public override IValue interpret (Context context)
		{
			if (entries.Count > 0) {
				check (context); // to compute itemType
				Dictionary<TextValue, IValue> dict = new Dictionary<TextValue, IValue> ();
				foreach (DictEntry e in entries) {
					TextValue key = new TextValue(e.GetKey ().interpret(context));
					IValue val = e.GetValue ().interpret (context);
					dict [key] = val;
				}
				return new DictValue (itemType, mutable, dict);
			} else
				return value;
		}

		public override void ToDialect (prompto.utils.CodeWriter writer)
		{
			if (mutable)
				writer.append ("mutable ");
			this.entries.ToDialect (writer);
		}

	}

}
