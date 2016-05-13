using prompto.runtime;
using prompto.error;
using System;
using prompto.value;
using prompto.type;
using System.Collections.Generic;


namespace prompto.literal
{

	public class DictLiteral : Literal<Dict>
	{

		// we can only compute keys by evaluating key expressions
		// so we can't just inherit from Dictionary<String,Expression>.
		// so we keep the full entry list.
		bool mutable = false;
		DictEntryList entries;
		IType itemType = null;

		public DictLiteral (bool mutable)
			: base ("{}", new Dict (MissingType.Instance))
		{
			this.entries = new DictEntryList ();
			this.mutable = mutable;
		}

		public DictLiteral (DictEntryList entries, bool mutable)
			: base (entries.ToString (), new Dict (MissingType.Instance))
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
			IType lastType = null;
			foreach (DictEntry e in entries) {
				IType keyType = e.getKey ().check (context);
				if (keyType != TextType.Instance)
					throw new SyntaxError ("Illegal key type: " + keyType.ToString ());
				IType elemType = e.getValue ().check (context);
				if (lastType == null)
					lastType = elemType;
				else if (!lastType.Equals (elemType)) {
					if (elemType.isAssignableTo (context, lastType)) {
						// lastType is less specific
					} else if (lastType.isAssignableTo (context, elemType))
						lastType = elemType; // elemType is less specific
                    else
						throw new SyntaxError ("Incompatible value types: " + elemType.ToString () + " and " + lastType.ToString ());
				}
			}
			return lastType;
		}

        
		public override IValue interpret (Context context)
		{
			if (entries.Count > 0) {
				check (context); // to compute itemType
				Dictionary<Text, IValue> dict = new Dictionary<Text, IValue> ();
				foreach (DictEntry e in entries) {
					Text key = (Text)e.getKey ().interpret (context);
					IValue val = e.getValue ().interpret (context);
					dict [key] = val;
				}
				return new Dict (itemType, dict, mutable);
			} else
				return value;
		}

		public override void ToDialect (prompto.utils.CodeWriter writer)
		{
			if (mutable)
				writer.append ("mutable ");
			base.ToDialect (writer);
		}

	}

}
