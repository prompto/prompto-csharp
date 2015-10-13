using prompto.runtime;
using prompto.value;
using System;

namespace prompto.store
{

	public class StorableDocument : IStorable
	{

		Document document = null;

		public bool Dirty {
			get {
				return document != null;
			}
			set {
				if (!value)
					document = null;
				else if (document == null)
					document = new Document ();
			}
		}

		public Document asDocument ()
		{
			return document;
		}

		public void setMember (Context context, String name, IValue value)
		{
			if (document == null)
				document = new Document ();
			document.SetMember (context, name, value);
		}

	}

}
