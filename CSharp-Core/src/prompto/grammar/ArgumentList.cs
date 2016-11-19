using prompto.runtime;
using prompto.utils;
using System;
using prompto.parser;
using prompto.argument;

namespace prompto.grammar {

public class ArgumentList : ObjectList<IArgument> {

	public ArgumentList() {
	}

	public ArgumentList(IArgument argument) {
		this.Add(argument);
	}
	
	public void register(Context context) {
		foreach(IArgument argument in this) 
			argument.register(context);
	}

	public void check(Context context) {
		foreach(IArgument argument in this) 
			argument.check(context);
	}

	public IArgument find(String name) {
		foreach(IArgument argument in this) {
			if(name.Equals(argument.GetName()))
					return argument;
		}
		return null;
	}
		public void ToDialect(CodeWriter writer) {
			if(this.Count==0)
				return;
			switch(writer.getDialect()) {
			case Dialect.E:
				ToEDialect(writer);
				break;
			case Dialect.O:
				ToODialect(writer);
				break;
			case Dialect.M:
				toPDialect(writer);
				break;
			}
		}

		private void ToEDialect(CodeWriter writer) {
			IArgument last = this.Count>0 ? this[this.Count-1] : null;
			writer.append("receiving ");
			foreach(IArgument argument in this) {
				if(argument==last)
					break;
				argument.ToDialect(writer);
				writer.append(", ");
			}
			if(this.Count>1) {
				writer.trimLast(2);
				writer.append(" and ");
			}
			last.ToDialect(writer);
			writer.append(" ");
		}

		private void ToODialect(CodeWriter writer) {
			foreach(IArgument argument in this) {
				argument.ToDialect(writer);
				writer.append(", ");
			}
			writer.trimLast(2);
		}

		private void toPDialect(CodeWriter writer) {
			foreach(IArgument argument in this) {
				argument.ToDialect(writer);
				writer.append(", ");
			}
			writer.trimLast(2);
		}
}

}