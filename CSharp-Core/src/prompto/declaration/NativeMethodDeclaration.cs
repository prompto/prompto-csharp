using prompto.runtime;
using prompto.csharp;
using System;
using prompto.statement;
using prompto.type;
using prompto.utils;
using prompto.value;
using prompto.param;

namespace prompto.declaration
{

    public class NativeMethodDeclaration : ConcreteMethodDeclaration
    {

        public NativeMethodDeclaration(String name, ParameterList parameters, IType returnType, StatementList statements)
            : base(name, parameters, returnType, statements)
        
        {
        }

        
		protected override IType checkStatements(Context context)
		{
			if(returnType!=null)
				return returnType;
			else
				return VoidType.Instance;
		}
    
		protected override IType fullCheck(Context context, bool isStart)
        {
            if (isStart)
            {
                context = context.newLocalContext();
                registerParameters(context);
            }
            if (parameters != null)
                parameters.check(context);
			CSharpNativeCall stmt = FindStatement();
            return stmt.checkNative(context, returnType);
        }

        
		public override IValue interpret(Context context)
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


		protected override void ToMDialect(CodeWriter writer) {
			writer.append("def ");
			if(this.getMemberOf()==null)
				writer.append("native ");
			writer.append(name);
			writer.append(" (");
			parameters.ToDialect(writer);
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


		protected override void ToODialect(CodeWriter writer) {
			if(returnType!=null  && returnType!=VoidType.Instance) {
				returnType.ToDialect(writer);
				writer.append(" ");
			}
			if (this.getMemberOf() == null)
				writer.append("native ");
			writer.append("method ");
			writer.append(name);
			writer.append(" (");
			parameters.ToDialect(writer);
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
		protected void ToEDialect(CodeWriter writer) {
			writer.append("define ");
			writer.append(name);
			writer.append(" as ");
			if (this.getMemberOf() == null)
				writer.append("native ");
			writer.append("method ");
			parameters.ToDialect(writer);
			if(returnType!=null && returnType!=VoidType.Instance) {
				writer.append("returning ");
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
