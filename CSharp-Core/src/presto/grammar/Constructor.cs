using System;
using System.Text;
using presto.runtime;
using presto.error;
using presto.parser;
using presto.value;
using presto.type;
using presto.declaration;
using presto.expression;
namespace presto.grammar
{

    public class Constructor : IExpression
    {

        CategoryType type;
        IExpression copyFrom;
        ArgumentAssignmentList assignments;

        public Constructor(CategoryType type)
        {
            this.type = type;
        }

        public CategoryType getType()
        {
            return type;
        }

        public void setAssignments(ArgumentAssignmentList assignments)
        {
            this.assignments = assignments;
            // in OOPS, first anonymous argument is copyFrom
            if (assignments != null && assignments.Count > 0 && assignments[0].getArgument() == null)
            {
                copyFrom = assignments[0].getExpression();
                this.assignments.RemoveAt(0);
            }
        }

        public ArgumentAssignmentList getAssignments()
        {
            return assignments;
        }

        public void setCopyFrom(IExpression copyFrom)
        {
            this.copyFrom = copyFrom;
        }

        public IExpression getCopyFrom()
        {
            return copyFrom;
        }

        public String ToDialect(Dialect dialect)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(type.getName());
            sb.Append(' ');
            if (copyFrom != null)
            {
                sb.Append("(from:");
                sb.Append(copyFrom.ToDialect(dialect));
                sb.Append(") ");
            }
            if (assignments != null)
            {
                sb.Append("with ");
                sb.Append(assignments.ToDialect(dialect));
            }
            return sb.ToString();
        }

        public IType check(Context context)
        {
            CategoryDeclaration cd = context.getRegisteredDeclaration<CategoryDeclaration>(type.getName());
            if (cd == null)
                throw new SyntaxError("Unknown category " + type.getName());
            type = (CategoryType)cd.GetType(context);
            cd.checkConstructorContext(context);
            if (copyFrom != null)
            {
                IType cft = copyFrom.check(context);
                if (!(cft is CategoryType))
                    throw new SyntaxError("Cannot copy from " + cft.getName());
            }
            if (assignments != null)
            {
                foreach (ArgumentAssignment assignment in assignments)
                {
                    if (!cd.hasAttribute(context, assignment.getName()))
                        throw new SyntaxError("\"" + assignment.getName() +
                            "\" is not an attribute of " + type.getName());
                    assignment.check(context);
                }
            }
            return type;
        }

        public Object interpret(Context context)
        {
            IInstance instance = type.newInstance(context);
            if (copyFrom != null)
            {
                Object copyObj = copyFrom.interpret(context);
                if (copyObj is IInstance)
                {
                    IInstance copyFrom2 = (IInstance)copyObj;
                    CategoryDeclaration cd = context.getRegisteredDeclaration<CategoryDeclaration>(type.getName());
                    foreach (String name in copyFrom2.getAttributeNames())
                    {
                        if (cd.hasAttribute(context, name))
                            instance.set(context, name, copyFrom2.GetMember(context, name));
                    }
                }
            }
            if (assignments != null)
            {
                foreach (ArgumentAssignment assignment in assignments)
                {
                    Object value = assignment.getExpression().interpret(context);
                    instance.set(context, assignment.getName(), (IValue)value);
                }
            }
            return instance;
        }

    }

}
