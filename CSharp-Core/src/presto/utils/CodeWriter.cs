using presto.parser;
using presto.runtime;
using System.Text;
using System;namespace presto.utils {

public class CodeWriter {
	
	public class Indenter {
		
		public String indents = "";
		public bool isStartOfLine = true;
		public CodeWriter writer;

		public Indenter(CodeWriter writer)
		{
			this.writer = writer;
		}

		public void appendTabsIfRequired(String s) {
			appendTabsIfRequired(s[s.Length-1]);
		}

		public void appendTabsIfRequired(char c) {
			if(isStartOfLine)
					writer.sb.Append(indents);
			isStartOfLine = c=='\n';
		}
		
		public void indent() {
			indents += '\t';
		}

		public void dedent() {
			if(indents.Length==0)
				throw new Exception("Illegal dedent!");
			indents = indents.Substring(1);
		}
	}
	
	Dialect dialect;
	Context context;
	StringBuilder sb;
	Indenter indenter;
	
	public CodeWriter(Dialect dialect, Context context) {
		this.dialect = dialect;
		this.context = context;
		this.sb = new StringBuilder();
		indenter = new Indenter(this);
	}
	
	public CodeWriter(Dialect dialect, Context context, StringBuilder sb, Indenter indenter) {
		this.dialect = dialect;
		this.context = context;
		this.sb = sb;
		this.indenter = indenter;
	}

	public Context getContext() {
		return context;
	}
	
	public void append(String s) {
		indenter.appendTabsIfRequired(s);
		sb.Append(s);
	}

	public void append(char c) {
		indenter.appendTabsIfRequired(c);
		sb.Append(c);
	}

	override
	public String ToString() {
			return sb.ToString();
	}

	public Dialect getDialect() {
		return dialect;
	}

	public void trimLast(int count) {
		sb.Length -= count;
	}

	public void indent() {
		indenter.indent();
	}

	public void dedent() {
		indenter.dedent();
	}

	public void newLine() {
		append('\n');
	}

	public bool isGlobalContext() {
		return context.isGlobalContext();
	}

	public CodeWriter newLocalWriter() {
		return new CodeWriter(dialect, context.newLocalContext(), sb, indenter);
	}

}
	 
}