using prompto.parser;
using prompto.runtime;
using System.Text;
using System;
using prompto.type;

namespace prompto.utils
{

	public class CodeWriter
	{

		public class Indenter
		{

			public String indents = "";
			public bool isStartOfLine = true;
			public CodeWriter writer;

			public Indenter(CodeWriter writer)
			{
				this.writer = writer;
			}

			public void appendTabsIfRequired(String s)
			{
				appendTabsIfRequired(s[s.Length - 1]);
			}

			public void appendTabsIfRequired(char c)
			{
				if (isStartOfLine)
					writer.sb.Append(indents);
				isStartOfLine = c == '\n';
			}

			public void indent()
			{
				indents += '\t';
			}

			public void dedent()
			{
				if (indents.Length == 0)
					throw new Exception("Illegal dedent!");
				indents = indents.Substring(1);
			}
		}

		Dialect dialect;
		Context context;
		StringBuilder sb;
		Indenter indenter;

		public CodeWriter(Dialect dialect, Context context)
		{
			this.dialect = dialect;
			this.context = context;
			this.sb = new StringBuilder();
			indenter = new Indenter(this);
		}

		public CodeWriter(Dialect dialect, Context context, StringBuilder sb, Indenter indenter)
		{
			this.dialect = dialect;
			this.context = context;
			this.sb = sb;
			this.indenter = indenter;
		}

		public Context getContext()
		{
			return context;
		}

		public CodeWriter appendRaw(String s)
		{
			sb.Append(s);
			return this;
		}

		public CodeWriter append(String s)
		{
			indenter.appendTabsIfRequired(s);
			sb.Append(s);
			return this;
		}

		public CodeWriter append(char c)
		{
			indenter.appendTabsIfRequired(c);
			sb.Append(c);
			return this;
		}


		public CodeWriter trimLast(int count)
		{
			sb.Length -= count;
			return this;
		}

		public CodeWriter indent()
		{
			indenter.indent();
			return this;
		}

		public CodeWriter dedent()
		{
			indenter.dedent();
			return this;
		}

		public CodeWriter newLine()
		{
			append('\n');
			return this;
		}

		public override String ToString()
		{
			return sb.ToString();
		}

		public Dialect getDialect()
		{
			return dialect;
		}

		public bool isGlobalContext()
		{
			return context.isGlobalContext();
		}

		public CodeWriter newLocalWriter()
		{
			return new CodeWriter(dialect, context.newLocalContext(), sb, indenter);
		}

		public CodeWriter newChildWriter()
		{
			return newChildWriter(context.newChildContext());
		}

		public CodeWriter newChildWriter(Context context)
		{
			return new CodeWriter(dialect, context, sb, indenter);
		}

		public CodeWriter newMemberWriter()
		{
			Context ctx = this.context.newLocalContext();
			ctx.setParentContext(this.context);
			return new CodeWriter(dialect, ctx, sb, indenter);
		}

		public CodeWriter newInstanceWriter(CategoryType type)
		{
			return new CodeWriter(dialect, context.newInstanceContext(type, false), sb, indenter);
		}

		public CodeWriter newDocumentWriter()
		{
			return new CodeWriter(dialect, context.newDocumentContext(null, false), sb, indenter);
		}


	}

}