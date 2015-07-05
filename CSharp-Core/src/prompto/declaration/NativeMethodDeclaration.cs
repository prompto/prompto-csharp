using prompto.runtime;
using prompto.csharp;
using System;
using prompto.statement;
using prompto.grammar;
using prompto.type;
using prompto.utils;
using prompto.value;


namespace prompto.declaration
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
			CSharpNativeCall stmt = FindStatement();
            return stmt.checkNative(context, returnType);
        }

        override
		public IValue interpret(Context context)
        {
            context.enterMethod(this);
            try
            {
				CSharpNativeCall stmt = FindStatement();
                return stmt.interpretNative(context, returnType);
            }
            finally
            {
                context.leaveMethod(this);
            }
        }

  
		private CSharpNativeCall FindStatement()
        {
            foreach (IStatement stmt in statements)
            {
                if (stmt is CSharpNativeCall)
					return (CSharpNativeCall)stmt;
            }
            throw new System.NotImplementedException(); // TODO
        }


		protected override void toSDialect(CodeWriter writer) {
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


		protected override void toODialect(CodeWriter writer) {
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