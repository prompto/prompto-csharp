using prompto.type;
using prompto.grammar;
using System;
using prompto.parser;
using System.Text;
using prompto.runtime;
using prompto.declaration;
using prompto.error;
using prompto.value;
using prompto.utils;


namespace prompto.expression
{

    public class ConstructorExpression : IExpression
    {

        CategoryType type;
		bool mutable;
        IExpression copyFrom;
        ArgumentAssignmentList assignments;

        public ConstructorExpression(CategoryType type, bool mutable, ArgumentAssignmentList assignments)
        {
            this.type = type;
			this.mutable = mutable;
            setAssignments(assignments);
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
                this.assignments.Remove(assignments[0]);
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

        public void ToDialect(CodeWriter writer)
        {
			switch(writer.getDialect()) {
			case Dialect.E:
				toEDialect(writer);
				break;
			case Dialect.O:
				toODialect(writer);
				break;
			case Dialect.S:
				toPDialect(writer);
				break;
			}
		}

		private void toPDialect(CodeWriter writer) {
			toODialect(writer);
		}

		private void toODialect(CodeWriter writer) {
			if(this.mutable)
				writer.append("mutable ");
			writer.append(type.GetName());
			ArgumentAssignmentList assignments = new ArgumentAssignmentList();
			if (copyFrom != null)
				assignments.add(new ArgumentAssignment(null, copyFrom));
			if(this.assignments!=null)
				assignments.addAll(this.assignments);
			assignments.ToDialect(writer);
		}

		private void toEDialect(CodeWriter writer) {
			if(this.mutable)
				writer.append("mutable ");
			writer.append(type.GetName());
			if (copyFrom != null) {
				writer.append(" from ");
				writer.append(copyFrom.ToString());
				if (assignments != null && assignments.Count>0)
					writer.append(",");
			}
			if (assignments != null)
				assignments.ToDialect(writer);
		}

        public IType check(Context context)
        {
			CategoryDeclaration cd = context.getRegisteredDeclaration<CategoryDeclaration>(type.GetName());
            if (cd == null)
				throw new SyntaxError("Unknown category " + type.GetName());
            type = (CategoryType)cd.GetType(context);
            cd.checkConstructorContext(context);
            if (copyFrom != null)
            {
                IType cft = copyFrom.check(context);
                if (!(cft is CategoryType))
					throw new SyntaxError("Cannot copy from " + cft.GetName());
            }
            if (assignments != null)
            {
                foreach (ArgumentAssignment assignment in assignments)
                {
					if (!cd.hasAttribute(context, assignment.GetName()))
						throw new SyntaxError("\"" + assignment.GetName() +
							"\" is not an attribute of " + type.GetName());
                    assignment.check(context);
                }
            }
            return type;
        }

        public IValue interpret(Context context)
        {
            IInstance instance = type.newInstance(context);
			instance.setMutable (true);
			try
			{
	            if (copyFrom != null)
	            {
	                Object copyObj = copyFrom.interpret(context);
	                if (copyObj is IInstance)
	                {
	                    IInstance initFrom = (IInstance)copyObj;
						CategoryDeclaration cd = context.getRegisteredDeclaration<CategoryDeclaration>(type.GetName());
	                    foreach (String name in initFrom.getMemberNames())
	                    {
	                        if (cd.hasAttribute(context, name))
							{
								IValue value = initFrom.GetMember(context, name, false);
								if(value!=null && value.IsMutable() && !this.mutable)
									throw new NotMutableError();
								instance.SetMember(context, name, value);
							}
	                    }
	                }
	            }
	            if (assignments != null)
	            {
	                foreach (ArgumentAssignment assignment in assignments)
	                {
	                    IValue value = assignment.getExpression().interpret(context);
						if(value!=null && value.IsMutable() && !this.mutable)
							throw new NotMutableError();
						instance.SetMember(context, assignment.GetName(), value);
	                }
	            }
	            return instance;
			} finally {
				instance.setMutable (this.mutable);
			}
        }

    }
}