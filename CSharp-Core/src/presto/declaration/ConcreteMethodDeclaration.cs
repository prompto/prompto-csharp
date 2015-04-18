using System;
using presto.runtime;
using presto.parser;
using presto.statement;
using presto.grammar;
using presto.type;
using presto.utils;
using presto.value;


namespace presto.declaration
{

	public class ConcreteMethodDeclaration : BaseMethodDeclaration, ICategoryMethodDeclaration
    {

        protected StatementList statements;

       public ConcreteMethodDeclaration(String name, ArgumentList arguments, IType returnType, StatementList statements)
            : base(name, arguments, returnType)
        {
            this.statements = statements;
        }

        override
        public void ToDialect(CodeWriter writer)
        {
			if(writer.isGlobalContext())
				writer = writer.newLocalWriter();
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

		protected virtual void toPDialect(CodeWriter writer) {
			writer.append("def ");
			writer.append(name);
			writer.append(" (");
			arguments.ToDialect(writer);
			writer.append(")");
			if(returnType!=null && returnType!=VoidType.Instance) {
				writer.append("->");
				returnType.ToDialect(writer);
			}
			writer.append(":\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}

		protected virtual void toEDialect(CodeWriter writer) {
			writer.append("define ");
			writer.append(name);
			writer.append(" as: method ");
			arguments.ToDialect(writer);
			if(returnType!=null && returnType!=VoidType.Instance) {
				writer.append("returning: ");
				returnType.ToDialect(writer);
				writer.append(" ");
			}
			writer.append("doing:\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}

		protected virtual void toODialect(CodeWriter writer) {
			if(returnType!=null && returnType!=VoidType.Instance) {
				returnType.ToDialect(writer);
				writer.append(" ");
			}
			writer.append("method ");
			writer.append(name);
			writer.append(" (");
			arguments.ToDialect(writer);
			writer.append(") {\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
			writer.append("}\n");
		}

        public StatementList getStatements()
        {
            return statements;
        }

		public void check(ConcreteCategoryDeclaration declaration, Context context)
		{
			// TODO Auto-generated method stub

		}

        override
        public IType check(Context context)
        {
            if (canBeChecked(context))
                return fullCheck(context);
            else
                return VoidType.Instance;
        }

        private bool canBeChecked(Context context)
        {
            if (context.isGlobalContext())
                return !mustBeBeCheckedInCallContext(context);
            else
                return true;
        }
        public bool mustBeBeCheckedInCallContext(Context context)
        {
            // if at least one argument is 'Code'
            if (arguments == null)
                return false;
            foreach (IArgument arg in arguments)
            {
                if (arg is CodeArgument)
                    return true;
            }
            return false;
        }

        virtual
        protected IType fullCheck(Context context)
        {
            if (context.isGlobalContext())
            {
                context = context.newLocalContext();
                registerArguments(context);
            }
            if (arguments != null)
                arguments.check(context);
            return statements.check(context);
        }

        public IType checkChild(Context context)
        {
            if (arguments != null)
                arguments.check(context);
            Context child = context.newChildContext();
            registerArguments(child);
            return statements.check(child);
        }

        override
		public IValue interpret(Context context)
        {
            context.enterMethod(this);
            try
            {
                return statements.interpret(context);
            }
            finally
            {
                context.leaveMethod(this);
            }
        }

        override
        public bool isEligibleAsMain()
        {
            if (arguments.Count == 0)
                return true;
            if (arguments.Count == 1)
            {
                IArgument arg = arguments[0];
                if (arg is CategoryArgument)
                {
                    IType type = ((CategoryArgument)arg).getType();
                    if (type is DictType)
                        return ((DictType)type).GetItemType() == TextType.Instance;
                }
            }
            return base.isEligibleAsMain();
        }

    }

}