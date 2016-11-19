using System;
using prompto.parser;
using prompto.type;
using prompto.runtime;
using prompto.value;
using prompto.utils;

namespace prompto.statement
{

	/* make comment a statement to fit in the general toDialect mechanism */
	public class CommentStatement : BaseStatement
	{

		String text;

		public CommentStatement (String text)
		{
			this.text = text;
		}

		public override IType check (Context context)
		{
			return VoidType.Instance;
		}

		public override IValue interpret (Context context)
		{
			return null;
		}

		public override void ToDialect (CodeWriter writer)
		{
			String[] lines = text.Split ("\n".ToCharArray());
			for (int i = 0; i < lines.Length; i++)
				lines [i] = uncomment (lines [i]);
			switch (writer.getDialect ()) {
			case Dialect.E:
			case Dialect.O:
				if (lines.Length > 1) {
					writer.append ("/*");
					foreach (String line in lines) {
						writer.append (line);
						writer.newLine ();
					}
					writer.trimLast (1);
					writer.append ("*/");
					writer.newLine ();
				} else {
					writer.append ("//");
					writer.append (lines [0]);
					writer.newLine ();
				}
				break;
			case Dialect.M:	
				foreach (String line in lines) {
					writer.append ("#");
					writer.append (line);
					writer.newLine ();
				}
				break;
			}		
		}

		private static String uncomment (String line)
		{
			if (line.StartsWith ("#"))
				return line.Substring (1);
			else if (line.StartsWith ("//"))
				return line.Substring (2);
			else
				return line;
		}

	}
}