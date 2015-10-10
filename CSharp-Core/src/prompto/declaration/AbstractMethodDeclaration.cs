using prompto.runtime;
using System;
using prompto.error;
using prompto.type;
using prompto.grammar;
using prompto.utils;
using prompto.parser;
using prompto.value;

namespace prompto.declaration
{

	public class AbstractMethodDeclaration : BaseMethodDeclaration
    {

   
        public AbstractMethodDeclaration(String name, ArgumentList arguments, IType returnType)
			: base(name, arguments)
        {
			this.returnType = returnType!=null ? returnType : VoidType.Instance;
        }

        
		public override IType check(Context context)
        {
            if (arguments != null)
                arguments.check(context);
            Context local = context.newLocalContext();
            registerArguments(local);
            return returnType;
        }

		public void check(ConcreteCategoryDeclaration declaration, Context context)
		{
			// TODO Auto-generated method stub

		}


        override
		public IValue interpret(Context context)
        {
            throw new SyntaxError("Should never get there !");
        }

		override
		public void ToDialect(CodeWriter writer) {
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
			writer.append("abstract def ");
			writer.append(name);
			writer.append(" (");
			arguments.ToDialect(writer);
			writer.append(")");
			if(returnType!=null && returnType!=VoidType.Instance) {
				writer.append("->");
				returnType.ToDialect(writer);
			}
		}

		protected void toEDialect(CodeWriter writer) {
			writer.append("define ");
			writer.append(name);
			writer.append(" as abstract method ");
			arguments.ToDialect(writer);
			if(returnType!=null && returnType!=VoidType.Instance) {
				writer.append("returning ");
				returnType.ToDialect(writer);
			}
		}

		protected void toODialect(CodeWriter writer) {
			writer.append("abstract ");
			if(returnType!=null && returnType!=VoidType.Instance) {
				returnType.ToDialect(writer);
				writer.append(" ");
			}
			writer.append("method ");
			writer.append(name);
			writer.append(" (");
			arguments.ToDialect(writer);
			writer.append(");");
		}
    }

}