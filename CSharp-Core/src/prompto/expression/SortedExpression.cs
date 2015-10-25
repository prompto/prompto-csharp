using System;
using prompto.runtime;
using prompto.error;
using prompto.parser;
using prompto.type;
using prompto.value;
using prompto.utils;
using prompto.grammar;


namespace prompto.expression
{

    public class SortedExpression : IExpression
    {

        IExpression source;
        IExpression key;

        public SortedExpression(IExpression source)
        {
            this.source = source;
        }

		public SortedExpression(IExpression source, IExpression key)
        {
            this.source = source;
			this.key = key;
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

		private void toEDialect(CodeWriter writer) {
			writer.append("sorted ");
			source.ToDialect(writer);
			if(key!=null) {
				writer.append(" with ");
				IExpression keyExp = key;
				if(keyExp is UnresolvedIdentifier) try {
					keyExp = ((UnresolvedIdentifier)keyExp).resolve(writer.getContext(), false);
				} catch (SyntaxError /*e*/) {
					// TODO add warning 
				}
				if(keyExp is InstanceExpression)
					((InstanceExpression)keyExp).ToDialect(writer, false);
				else
					keyExp.ToDialect(writer);
				writer.append(" as key");
			}
		}	

		private void toODialect(CodeWriter writer) {
			writer.append("sorted (");
			source.ToDialect(writer);
			if(key!=null) {
				writer.append(", key = ");
				key.ToDialect(writer);
			}
			writer.append(")");
		}

		private void toPDialect(CodeWriter writer) {
			toODialect(writer);
		}

        public IType check(Context context)
        {
            IType type = source.check(context);
			if (!(type is ListType || type is SetType))
                throw new SyntaxError("Unsupported type: " + type);
            return type;
        }

        public IValue interpret(Context context)
        {
            IType type = source.check(context);
			if (!(type is ListType || type is SetType))
                throw new SyntaxError("Unsupported type: " + type);
			IValue o = source.interpret(context);
            if (o == null)
                throw new NullReferenceError();
			if (!(o is IContainer))
                throw new InternalError("Unexpected type:" + o.GetType().Name);
            IType itemType = ((ContainerType)type).GetItemType();
            if (itemType is CategoryType)
				return ((CategoryType)itemType).sort(context, (IContainer)o, key);
            else
				return itemType.sort(context, (IContainer)o);
        }

		public void setKey(IExpression key)
        {
			this.key = key;
        }

    }
}
