using System.Collections.Generic;
using System;
using prompto.runtime;
using System.Text;
using prompto.parser;
using prompto.declaration;
using prompto.utils;
using prompto.expression;
using prompto.param;

namespace prompto.grammar
{

    public class ArgumentList : List<Argument>
    {

        public ArgumentList()
        {

        }

        public ArgumentList(ArgumentList arguments)
            : base(arguments)
        {
        }

		public void CheckLastAnd()
		{
			Argument argument = this[this.Count - 1];
			if(argument!=null && argument.Parameter!=null && argument.getExpression() is AndExpression) 
			{
				AndExpression and = (AndExpression)argument.getExpression();
				if(and.Left is UnresolvedIdentifier) 
				{
					String id = ((UnresolvedIdentifier)and.Left).getName();
					if (Char.IsLower(id[0]))
					{
						this.RemoveAt(this.Count - 1);
						// add AttributeArgument
						AttributeParameter parameter = new AttributeParameter(id);
						Argument attribute = new Argument(parameter, null);
						this.Add(attribute);
                        // fix last argument
                        argument.Expression = and.Right;
						this.Add(argument);
					}
				}
			}
		}
 
		public override string ToString ()
		{
			String s = "(";
			if(this.Count>0)
			{
				foreach (Argument argument in this) {
					s += argument.ToString ();
					s += ", ";
				}
				s = s.Substring (0, s.Length - 2);
			}
			s += ")";
			return s;
		}

        public Argument find(String name)
        {
            foreach (Argument argument in this)
            {
				if (name.Equals(argument.GetName()))
                    return argument;
            }
            return null;
        }

        public ArgumentList makeArguments(Context context, IMethodDeclaration declaration)
        {
            ArgumentList arguments = new ArgumentList();
            foreach (Argument argument in this)
                arguments.Add(argument.makeArgument(context, declaration));
            return arguments;
        }

		public void ToDialect(CodeWriter writer) {
			switch(writer.getDialect()) {
			case Dialect.E:
				ToEDialect(writer);
				break;
			case Dialect.O:
				ToODialect(writer);
				break;
			case Dialect.M:
				ToMDialect(writer);
				break;
			}
		}

		private void ToEDialect(CodeWriter writer) {
			int idx = 0;
			// anonymous argument before 'with'
			if(this.Count>0 && this[0].Parameter==null) {
				writer.append(' ');
				this[idx++].ToDialect(writer);
			}
			if(idx<this.Count) {
				writer.append(" with ");
				this[idx++].ToDialect(writer);
				writer.append(", ");
				while(idx<this.Count-1) {
					this[idx++].ToDialect(writer);
					writer.append(", ");
				}
				writer.trimLast(2);
				if(idx<this.Count) {
					writer.append(" and ");				
					this[idx++].ToDialect(writer);
				}
			}
		}

		private void ToODialect(CodeWriter writer) {
			writer.append("(");
			foreach(Argument argument in this) {
				argument.ToDialect(writer);
				writer.append(", ");
			}
			if(this.Count>0)
				writer.trimLast(2);
			writer.append(")");
		}

		private void ToMDialect(CodeWriter writer) {
			writer.append("(");
			foreach(Argument argument in this) {
				argument.ToDialect(writer);
				writer.append(", ");
			}
			if(this.Count>0)
				writer.trimLast(2);
			writer.append(")");
		}

}
}
