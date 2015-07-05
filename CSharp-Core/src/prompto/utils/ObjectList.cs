using System.Collections.Generic;
using System;
using prompto.parser;
using System.Text;

namespace prompto.utils
{

	public class ObjectList<T> : List<T>
	{

		public ObjectList ()
		{
		}

		public ObjectList (ObjectList<T> assignments)
			: base (assignments)
		{
		}

    
		public override bool Equals (object obj)
		{
			if (!(obj is List<T>))
				return false;
			List<T> other = (List<T>)obj;
			if (this.Count != other.Count)
				return false;
			IEnumerator<T> enumThis = this.GetEnumerator ();
			IEnumerator<T> enumOther = other.GetEnumerator ();
			while (enumThis.MoveNext () && enumOther.MoveNext ()) {
				if (!Utils.equal (enumThis.Current, enumOther.Current))
					return false;
			}
			return true;
		}

		public override String  ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			foreach (T o in this) {
				sb.Append (o.ToString ());
				sb.Append (", ");
			}
			if (sb.Length >= 2)
				sb.Length -= 2;
			return sb.ToString ();
		}

	}

}
