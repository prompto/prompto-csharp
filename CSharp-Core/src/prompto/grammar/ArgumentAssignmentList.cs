using System.Collections.Generic;
using System;
using prompto.runtime;
using System.Text;
using prompto.parser;
using prompto.declaration;
using prompto.utils;
using prompto.expression;
using prompto.argument;

namespace prompto.grammar
{

    public class ArgumentAssignmentList : List<ArgumentAssignment>
    {

        public ArgumentAssignmentList()
        {

        }

        public ArgumentAssignmentList(ArgumentAssignmentList assignments)
            : base(assignments)
        {
        }

		public void CheckLastAnd()
		{
			ArgumentAssignment assignment = this[this.Count - 1];
			if(assignment!=null && assignment.getArgument()!=null && assignment.getExpression() is AndExpression) 
			{
				AndExpression and = (AndExpression)assignment.getExpression();
				if(and.Left is UnresolvedIdentifier) 
				{
					String id = ((UnresolvedIdentifier)and.Left).getName();
					if (Char.IsLower(id[0]))
					{
						this.RemoveAt(this.Count - 1);
						// add AttributeArgument
						AttributeArgument argument = new AttributeArgument(id);
						ArgumentAssignment attribute = new ArgumentAssignment(argument, null);
						this.Add(attribute);
						// fix last assignment
						assignment.setExpression(and.Right);
						this.Add(assignment);
					}
				}
			}
		}
 
		public override string ToString ()
		{
			String s = "(";
			if(this.Count>0)
			{
				foreach (ArgumentAssignment aa in this) {
					s += aa.ToString ();
					s += ", ";
				}
				s = s.Substring (0, s.Length - 2);
			}
			s += ")";
			return s;
		}

        public ArgumentAssignment find(String name)
        {
            foreach (ArgumentAssignment assignment in this)
            {
				if (name.Equals(assignment.GetName()))
                    return assignment;
            }
            return null;
        }

        public ArgumentAssignmentList makeAssignments(Context context, IMethodDeclaration declaration)
        {
            ArgumentAssignmentList assignments = new ArgumentAssignmentList();
            foreach (ArgumentAssignment assignment in this)
                assignments.Add(assignment.makeAssignment(context, declaration));
            return assignments;
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
				toPDialect(writer);
				break;
			}
		}

		private void ToEDialect(CodeWriter writer) {
			int idx = 0;
			// anonymous argument before 'with'
			if(this.Count>0 && this[0].getArgument()==null) {
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
			foreach(ArgumentAssignment as_ in this) {
				as_.ToDialect(writer);
				writer.append(", ");
			}
			if(this.Count>0)
				writer.trimLast(2);
			writer.append(")");
		}

		private void toPDialect(CodeWriter writer) {
			writer.append("(");
			foreach(ArgumentAssignment as_ in this) {
				as_.ToDialect(writer);
				writer.append(", ");
			}
			if(this.Count>0)
				writer.trimLast(2);
			writer.append(")");
		}

}
}
