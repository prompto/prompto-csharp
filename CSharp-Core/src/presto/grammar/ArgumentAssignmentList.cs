using System.Collections.Generic;
using System;
using presto.runtime;
using System.Text;
using presto.parser;
using presto.declaration;
using presto.utils;


namespace presto.grammar
{

    public class ArgumentAssignmentList : List<ArgumentAssignment>
    {

        public ArgumentAssignmentList()
        {

        }

        public ArgumentAssignmentList(ArgumentAssignment assignment)
        {
            this.Add(assignment);
        }

        public ArgumentAssignmentList(ArgumentAssignmentList assignments)
            : base(assignments)
        {
        }

        public ArgumentAssignmentList(ArgumentAssignmentList assignments, ArgumentAssignment assignment)
            : base(assignments)
        {
            this.Add(assignment);
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

        /* for unified grammar */
        public void add(ArgumentAssignment assignment)
        {
            this.Add(assignment);
        }

        public void addAll(ArgumentAssignmentList assignments)
        {
            this.AddRange(assignments);
        }
        
        public ArgumentAssignment find(String name)
        {
            foreach (ArgumentAssignment assignment in this)
            {
                if (name.Equals(assignment.getName()))
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
				toEDialect(writer);
				break;
			case Dialect.O:
				toODialect(writer);
				break;
			case Dialect.P:
				toPDialect(writer);
				break;
			}
		}

		private void toEDialect(CodeWriter writer) {
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

		private void toODialect(CodeWriter writer) {
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
