using presto.runtime;
using presto.csharp;
using System;
using presto.statement;
using presto.grammar;
using presto.type;
using presto.utils;
using presto.value;


namespace presto.declaration
{

    public class NativeMethodDeclaration : ConcreteMethodDeclaration
    {

        public NativeMethodDeclaration(String name, ArgumentList arguments, IType returnType, StatementList statements)
            : base(name, arguments, returnType, statements)
        
        {
        }

        override
        protected IType fullCheck(Context context)
        {
            if (context.isGlobalContext())
            {
                context = context.newLocalContext();
                registerArguments(context);
            }
            if (arguments != null)
                arguments.check(context);
            IStatement stmt = FindStatement();
            return stmt.check(context);
        }

        override
		public IValue interpret(Context context)
        {
            context.enterMethod(this);
            try
            {
                IStatement stmt = FindStatement();
                return stmt.interpret(context);
            }
            finally
            {
                context.leaveMethod(this);
            }
        }

  
        private IStatement FindStatement()
        {
            foreach (IStatement stmt in statements)
            {
                if (stmt is CSharpNativeCall)
                    return stmt;
            }
            throw new System.NotImplementedException(); // TODO
        }

		override
		protected void toPDialect(CodeWriter writer) {
			writer.append("def native ");
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

		override
		protected void toODialect(CodeWriter writer) {
			if(returnType!=null  && returnType!=VoidType.Instance) {
				returnType.ToDialect(writer);
				writer.append(" ");
			}
			writer.append("native method ");
			writer.append(name);
			writer.append(" (");
			arguments.ToDialect(writer);
			writer.append(") {\n");
			writer.indent();
			foreach(IStatement statement in statements) {
				statement.ToDialect(writer);
				writer.newLine();
			}
			writer.dedent();
			writer.append("}\n");
		}

		override
		protected void toEDialect(CodeWriter writer) {
			writer.append("define ");
			writer.append(name);
			writer.append(" as: native method ");
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
			writer.append("\n");
		}

    }

}
