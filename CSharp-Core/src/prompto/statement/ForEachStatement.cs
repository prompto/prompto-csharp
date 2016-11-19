using System;
using prompto.runtime;
using System.Collections.Generic;
using prompto.value;
using prompto.parser;
using prompto.type;
using prompto.expression;
using prompto.grammar;
using prompto.utils;
using prompto.error;


namespace prompto.statement
{

    public class ForEachStatement : BaseStatement
    {

        String v1, v2;
        IExpression source;
		StatementList statements;

        public ForEachStatement(String i1, String i2, IExpression source, StatementList instructions)
        {
            this.v1 = i1;
            this.v2 = i2 == null ? null : i2;
            this.source = source;
            this.statements = instructions;
        }

        public StatementList getInstructions()
        {
            return statements;
        }

        override
        public IType check(Context context)
        {
            IType srcType = source.check(context);
            IType elemType = srcType.checkIterator(context);
            return checkItemIterator(elemType, context);
        }


        private IType checkItemIterator(IType elemType, Context context)
        {
            Context child = context.newChildContext();
            String itemName = v2 == null ? v1 : v2;
            context.registerValue(new Variable(itemName, elemType));
            if (v2 != null)
                context.registerValue(new Variable(v1, IntegerType.Instance));
			return statements.check(child, null);
        }

        override
		public IValue interpret(Context context)
        {
			IType srcType = source.check (context);
			IType elemType = srcType.checkIterator(context);
			return interpretItemIterator(context, elemType);
        }

		private IEnumerator<IValue> getEnumerator(Context context, Object src) {
			// start with the most specialized implementation
			if (src is IContainer) 
				return ((IContainer) src).GetEnumerable(context).GetEnumerator();
			else if(src is IEnumerable<IValue>)
				return ((IEnumerable<IValue>)src).GetEnumerator();
			else if (src is IEnumerator<IValue>)
				return (IEnumerator<IValue>)src;
			else
				throw new InternalError("Should never get there!");
		}


		private IValue interpretItemIterator(Context context, IType elemType)
        {
            if (v2 == null)
				return interpretItemIteratorNoIndex(context, elemType);
            else
				return interpretItemIteratorWithIndex(context, elemType);
        }

		private IValue interpretItemIteratorNoIndex(Context context, IType elemType)
        {
			IValue src = source.interpret(context);
			IEnumerator<IValue> iterator = getEnumerator(context, src);
			while (iterator.MoveNext())
            {
                Context child = context.newChildContext();
                child.registerValue(new Variable(v1, elemType));
				child.setValue(v1, iterator.Current);
				IValue value = statements.interpret(child);
				if (value == BreakResult.Instance)
					break;
                if (value != null)
                    return value;
            }
            return null;
        }

		private IValue interpretItemIteratorWithIndex(Context context, IType elemType)
        {
            Int64 index = 0L;
			IValue src = source.interpret(context);
			IEnumerator<IValue> iterator = getEnumerator(context, src);
			while (iterator.MoveNext())
            {
                Context child = context.newChildContext();
                child.registerValue(new Variable(v2, elemType));
				child.setValue(v2, iterator.Current);
                child.registerValue(new Variable(v1, IntegerType.Instance));
                child.setValue(v1, new Integer(++index));
				IValue value = statements.interpret(child);
  				if (value == BreakResult.Instance)
					break;
                if (value != null)
                    return value;
            }
            return null;
        }

		override
		public void ToDialect(CodeWriter writer) {
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

		private void ToODialect(CodeWriter writer) {
			writer.append("for each (");
			writer.append(v1);
			if(v2!=null) {
				writer.append(", ");
				writer.append(v2);
			}
			writer.append(" in ");
			source.ToDialect(writer);
			writer.append(")");
			bool oneLine = statements.Count==1 && (statements[0] is SimpleStatement);
			if(!oneLine)
				writer.append(" {");
			writer.newLine();
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
			if(!oneLine) {
				writer.append("}");
				writer.newLine();
			}		
		}

		private void ToEDialect(CodeWriter writer) {
			writer.append("for each ");
			writer.append(v1);
			if(v2!=null) {
				writer.append(", ");
				writer.append(v2);
			}
			writer.append(" in ");
			source.ToDialect(writer);
			writer.append(":");
			writer.newLine();
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}

		private void toPDialect(CodeWriter writer) {
			writer.append("for ");
			writer.append(v1);
			if(v2!=null) {
				writer.append(", ");
				writer.append(v2);
			}
			writer.append(" in ");
			source.ToDialect(writer);
			writer.append(":");
			writer.newLine();
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}
    }

}
