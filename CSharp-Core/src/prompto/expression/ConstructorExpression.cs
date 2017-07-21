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
        IExpression copyFrom;
        ArgumentAssignmentList assignments;

        public ConstructorExpression(CategoryType type, ArgumentAssignmentList assignments)
        {
            this.type = type;
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

		private void toPDialect(CodeWriter writer) {
			ToODialect(writer);
		}

		private void ToODialect(CodeWriter writer) {
			type.ToDialect (writer);
			ArgumentAssignmentList assignments = new ArgumentAssignmentList();
			if (copyFrom != null)
				assignments.add(new ArgumentAssignment(null, copyFrom));
			if(this.assignments!=null)
				assignments.addAll(this.assignments);
			assignments.ToDialect(writer);
		}

		private void ToEDialect(CodeWriter writer) {
			type.ToDialect (writer);
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
			CategoryDeclaration cd = context.getRegisteredDeclaration<CategoryDeclaration>(this.type.GetTypeName());
            if (cd == null)
				throw new SyntaxError("Unknown category " + this.type.GetTypeName());
            IType type = (CategoryType)cd.GetIType(context);
            cd.checkConstructorContext(context);
            if (copyFrom != null)
            {
                IType cft = copyFrom.check(context);
				if (!(cft is CategoryType) && (cft!=DocumentType.Instance))
					throw new SyntaxError("Cannot copy from " + cft.GetTypeName());
            }
            if (assignments != null)
            {
                foreach (ArgumentAssignment assignment in assignments)
                {
					if (!cd.hasAttribute(context, assignment.GetName()))
						throw new SyntaxError("\"" + assignment.GetName() +
							"\" is not an attribute of " + type.GetTypeName());
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
						IInstance copyInstance = (IInstance)copyObj;
						CategoryDeclaration cd = context.getRegisteredDeclaration<CategoryDeclaration>(type.GetTypeName());
	                    foreach (String name in copyInstance.GetMemberNames())
	                    {
	                        if (cd.hasAttribute(context, name))
							{
								IValue value = copyInstance.GetMember(context, name, false);
								if(value!=null && value.IsMutable() && !this.type.Mutable)
									throw new NotMutableError();
								instance.SetMember(context, name, value);
							}
	                    }
	                }
					else if (copyObj is Document) 
					{
						Document copyDoc = (Document)copyObj;
						CategoryDeclaration cd = context.getRegisteredDeclaration<CategoryDeclaration>(type.GetTypeName());
						foreach (String name in copyDoc.GetMemberNames())
						{
							if (cd.hasAttribute(context, name))
							{
								IValue value = copyDoc.GetMember(context, name, false);
								if(value!=null && value.IsMutable() && !this.type.Mutable)
									throw new NotMutableError();
								// TODO convert to attribute type, see Java version
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
						if(value!=null && value.IsMutable() && !this.type.Mutable)
							throw new NotMutableError();
						instance.SetMember(context, assignment.GetName(), value);
	                }
	            }
			} finally {
				instance.setMutable (this.type.Mutable);
			}
			return instance;
        }

    }
}