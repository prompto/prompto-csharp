using prompto.error;
using prompto.runtime;
using System.Text;
using System;
using prompto.value;
using prompto.type;
using prompto.expression;
using prompto.declaration;
using prompto.utils;

namespace prompto.grammar
{

    public class CategorySymbol : Symbol, IExpression
    {

        ArgumentAssignmentList assignments;
        EnumeratedCategoryType type;

        public CategorySymbol(String name, ArgumentAssignmentList assignments)
            : base(name)
        {
            this.assignments = assignments;
        }

        public void setType(EnumeratedCategoryType type)
        {
            this.type = type;
        }

        override
        public IType GetType(Context context)
        {
            return type;
        }

        public EnumeratedCategoryType getType()
        {
            return type;
        }

        public void setAssignments(ArgumentAssignmentList assignments)
        {
            this.assignments = assignments;
        }

        public ArgumentAssignmentList getAssignments()
        {
            return assignments;
        }

        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (assignments != null)
                sb.Append(assignments.ToString());
            if (sb.Length == 0)
				sb.Append(type.GetName());
            return sb.ToString();
        }

		override
		public void ToDialect(CodeWriter writer) {
			writer.append(symbol);
			writer.append(" ");
			assignments.ToDialect(writer);
		}

        override
        public IType check(Context context)
        {
			EnumeratedCategoryDeclaration cd = context.getRegisteredDeclaration<EnumeratedCategoryDeclaration>(type.GetName());
            if (cd == null)
				throw new SyntaxError("Unknown category " + type.GetName());
            if (assignments != null)
            {
				context = context.newLocalContext ();
                foreach (ArgumentAssignment assignment in assignments)
                {
					if (!cd.hasAttribute(context, assignment.GetName()))
						throw new SyntaxError("\"" + assignment.GetName() + "\" is not an attribute of " + type.GetName());
                    assignment.check(context);
                }
            }
            return type;
        }

        override
        public IValue interpret(Context context)
        {
            IInstance instance = type.newInstance(context);
			instance.setMutable (true);
            if (assignments != null)
            {
				context = context.newLocalContext ();
                foreach (ArgumentAssignment assignment in assignments)
                {
					IValue value = assignment.getExpression().interpret(context);
					instance.SetMember(context, assignment.GetName(), value);
                }
            }
			instance.SetMember(context, "name", new Text(this.GetName()));
			instance.setMutable (false);
			return instance;
        }

    }
}
