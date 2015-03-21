using presto.error;
using presto.runtime;
using System.Text;
using System;
using presto.value;
using presto.type;
using presto.expression;
using presto.declaration;
using presto.utils;

namespace presto.grammar
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
                sb.Append(type.getName());
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
            EnumeratedCategoryDeclaration cd = context.getRegisteredDeclaration<EnumeratedCategoryDeclaration>(type.getName());
            if (cd == null)
                throw new SyntaxError("Unknown category " + type.getName());
            if (assignments != null)
            {
                foreach (ArgumentAssignment assignment in assignments)
                {
                    if (!cd.hasAttribute(context, assignment.getName()))
                        throw new SyntaxError("\"" + assignment.getName() + "\" is not an attribute of " + type.getName());
                    assignment.check(context);
                }
            }
            return type;
        }

        override
        public IValue interpret(Context context)
        {
            IInstance instance = type.newInstance(context);
            if (assignments != null)
            {
                foreach (ArgumentAssignment assignment in assignments)
                {
					IValue value = assignment.getExpression().interpret(context);
                    instance.set(context, assignment.getName(), value);
                }
            }
            return instance;
        }

    }
}
