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

        
        public override void ToDialect(CodeWriter writer)
        {
			parent.ToDialect (writer);
			writer.append ("[");
			item.ToDialect (writer);
			writer.append("]");
        }

        
        public override IType check(Context context)
        {
            IType parentType = parent.check(context);
            if (parentType == null)
                throw new SyntaxError("Unknown parent: " + parent.ToString());
            IType itemType = item.check(context);
            return parentType.checkItem(context, itemType);
        }

        
		public override IValue interpret(Context context)
        {
			IValue o = parent.interpret(context);
			if (o == null || o == NullValue.Instance)
                throw new NullReferenceError();
			IValue i = item.interpret(context);
			if (i == null || i == NullValue.Instance)
                throw new NullReferenceError();
            return o.GetItem(context, i);
         }
    }
}
