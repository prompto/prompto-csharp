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
using prompto.argument;

namespace prompto.expression
{

    public class ConstructorExpression : IExpression
    {

        CategoryType type;
        IExpression copyFrom;
        ArgumentAssignmentList assignments;
		bool xchecked;

        public ConstructorExpression(CategoryType type, IExpression copyFrom, ArgumentAssignmentList assignments, bool xchecked)
        {
            this.type = type;
			this.copyFrom = copyFrom;
			this.assignments = assignments;
			this.xchecked = xchecked;
        }

        public CategoryType getType()
        {
            return type;
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
			Context context = writer.getContext();
			CategoryDeclaration cd = context.getRegisteredDeclaration<CategoryDeclaration>(this.type.GetTypeName());
			if(cd==null)
				throw new SyntaxError("Unknown category " + this.type.GetTypeName());
			checkFirstHomonym(context, cd);
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

		void checkFirstHomonym(Context context,  CategoryDeclaration decl)
		{
			if (xchecked)
				return;
			if(assignments!=null && assignments.Count>0)
            	checkFirstHomonym(context, decl, assignments[0]);
			xchecked = true;
		}

		void checkFirstHomonym(Context context, CategoryDeclaration decl, ArgumentAssignment assignment)
		{
			if(assignment.getArgument()==null) {
				IExpression exp = assignment.getExpression();
				// when coming from UnresolvedCall, could be an homonym
				string name = null;
				if(exp is UnresolvedIdentifier) 
					name = ((UnresolvedIdentifier)exp).getName();
				else if(exp is InstanceExpression)
					name = ((InstanceExpression)exp).getName();
				if(name!=null && decl.hasAttribute(context, name)) {
					// convert expression to name to avoid translation issues
					assignment.setArgument(new AttributeArgument(name));
					assignment.setExpression(null);
				}
			}
		}

		private void toPDialect(CodeWriter writer) {
			ToODialect(writer);
		}

		private void ToODialect(CodeWriter writer) {
			type.ToDialect (writer);
			ArgumentAssignmentList assignments = new ArgumentAssignmentList();
			if (copyFrom != null)
				assignments.Add(new ArgumentAssignment(new AttributeArgument("from"), copyFrom));
			if(this.assignments!=null)
				assignments.AddRange(this.assignments);
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
            checkFirstHomonym(context, cd);
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
            return cd.GetIType(context);
        }

        public IValue interpret(Context context)
        {
			CategoryDeclaration cd = context.getRegisteredDeclaration<CategoryDeclaration>(this.type.GetTypeName());
            if (cd == null)
				throw new SyntaxError("Unknown category " + this.type.GetTypeName());
            checkFirstHomonym(context, cd);
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