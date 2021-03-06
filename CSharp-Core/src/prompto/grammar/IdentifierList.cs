using System;
using prompto.utils;
using prompto.parser;

namespace prompto.grammar {

    public class IdentifierList : ObjectList<String>
    {

	public static IdentifierList parse(String ids) {
		String[] parts = ids.Split(",".ToCharArray());
		IdentifierList result = new IdentifierList();
		foreach(String part in parts)
			result.Add(part);
		return result;
	}
	
	public IdentifierList() {		
	}

    public IdentifierList(String identifier)
    {
		this.Add(identifier);
	}

	public void ToDialect(CodeWriter writer, bool finalAnd)
    {
			switch(writer.getDialect()) {
			case Dialect.E:
				ToEDialect(writer, finalAnd);
				break;
			case Dialect.O:
				ToODialect(writer);
				break;
			case Dialect.M:
				toPDialect(writer);
				break;
			}

		}

		private void ToEDialect(CodeWriter writer, bool finalAnd) {
			switch(this.Count) {
			case 0:
				return;
			case 1:
				writer.append(this[0]);
				break;
			default:
				String last = this [this.Count - 1];
				foreach (String s in this) {
					if (finalAnd && Object.ReferenceEquals (s, last))
						break;
					writer.append (s);
					writer.append (", ");
				}
				writer.trimLast (2);
				if (finalAnd) {
					writer.append (" and ");
					writer.append (last);
				}
				break;
			}
		}

		private void ToODialect(CodeWriter writer) {
			if(this.Count>0) {
				foreach(String s in this) {
					writer.append(s);
					writer.append(", ");
				}
				writer.trimLast(2);
			}
		}

		private void toPDialect(CodeWriter writer) {
			ToODialect(writer);
		}
}

}
