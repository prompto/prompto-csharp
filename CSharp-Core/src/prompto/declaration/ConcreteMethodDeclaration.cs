using System;
using prompto.runtime;
using prompto.parser;
using prompto.statement;
using prompto.grammar;
using prompto.type;
using prompto.utils;
using prompto.value;
using prompto.argument;


namespace prompto.declaration
{

	public class ConcreteMethodDeclaration : BaseMethodDeclaration
    {

        protected StatementList statements;

       public ConcreteMethodDeclaration(String name, ArgumentList arguments, IType returnType, StatementList statements)
            : base(name, arguments, returnType)
        {
            this.statements = statements;
			foreach (IStatement s in statements) {

				if (s is DeclarationStatement<IDeclaration>)
					((DeclarationStatement<IDeclaration>)s).getDeclaration ().SetClosureOf (this);
			}
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
			case Dialect.S:
				toSDialect(writer);
				break;
			}
		}

		protected virtual void toSDialect(CodeWriter writer) {
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
			writer.append(" as method ");
			arguments.ToDialect(writer);
			if(returnType!=null && returnType!=VoidType.Instance) {
				writer.append("returning ");
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

		public virtual void check(ConcreteCategoryDeclaration declaration, Context context)
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
                return !isTemplate();
            else
                return true;
        }
		public override bool isTemplate()
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
			return statements.check(context, returnType);
        }

        public IType checkChild(Context context)
        {
            if (arguments != null)
                arguments.check(context);
            Context child = context.newChildContext();
            registerArguments(child);
			return statements.check(child, returnType);
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