using System;
using prompto.runtime;
using prompto.error;
using prompto.value;
using prompto.parser;
using prompto.type;
using prompto.utils;


namespace prompto.expression
{

    public class ItemSelector : SelectorExpression
    {

        IExpression item;

        public ItemSelector(IExpression item)
        {
            this.item = item;
        }

        public ItemSelector(IExpression parent, IExpression item)
            : base(parent)
        {
            this.item = item;
        }

        public IExpression getItem()
        {
            return item;
        }

        override
        public void ToDialect(CodeWriter writer)
        {
			parent.ToDialect (writer);
			writer.append ("[");
			item.ToDialect (writer);
			writer.append("]");
        }

        override
        public IType check(Context context)
        {
            IType parentType = parent.check(context);
            IType itemType = item.check(context);
            return parentType.checkItem(context, itemType);
        }

        override
		public IValue interpret(Context context)
        {
			IValue o = parent.interpret(context);
			if (o == null || o == NullValue.Instance)
                throw new NullReferenceError();
			IValue i = item.interpret(context);
			if (i == null || i == NullValue.Instance)
                throw new NullReferenceError();
            if (o is IContainer && i is IValue)
                return ((IContainer)o).GetItem(context, i);
            else
                throw new SyntaxError("Illegal: " + parent.GetType().Name + "[" + item.GetType().Name + "]");
         }
    }
}
