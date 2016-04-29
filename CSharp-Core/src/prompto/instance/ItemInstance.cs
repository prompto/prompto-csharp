using System;
using prompto.runtime;
using prompto.error;
using prompto.value;
using prompto.parser;
using prompto.expression;
using prompto.type;
using prompto.utils;


namespace prompto.instance {

    public class ItemInstance : IAssignableSelector
    {

        IAssignableInstance parent;
        IExpression item;

        public ItemInstance(IAssignableInstance parent, IExpression item)
        {
            this.parent = parent;
            this.item = item;
        }

        public ItemInstance(IExpression item)
        {
            this.item = item;
        }

        public void SetParent(IAssignableInstance parent)
        {
            this.parent = parent;
        }

        public IExpression getItem()
        {
            return item;
        }

		public void ToDialect(CodeWriter writer, IExpression expression)
        {
			parent.ToDialect (writer, null);
			writer.append ("[");
			item.ToDialect (writer);
			writer.append("]");
        }

		public IType checkAssignValue(Context context, IExpression expression)
        {
			// called when a[3] = value
            IType itemType = item.check(context);
			return parent.checkAssignItem(context, itemType);
        }

		public IType checkAssignMember(Context context, String memberName)
        {
			// called when a[3].member = value
			return AnyType.Instance; // TODO 
       }

		public IType checkAssignItem(Context context, IType itemType)
        {
			// called when a[3][x] = value
			IType thisItemType = item.check(context);
			IType parentType = parent.checkAssignItem(context, thisItemType);
			return parentType.checkItem(context, itemType); 
		}

        public void assign(Context context, IExpression expression)
        {
			IValue root = parent.interpret(context);
			if(!root.IsMutable())
				throw new NotMutableError();
			IValue elem = item.interpret(context);
			IValue value = expression.interpret(context);
			root.SetItem(context, elem, value);
        }

        public IValue interpret(Context context)
        {
			IValue parent = this.parent.interpret(context);
			IValue item = this.item.interpret(context);
            if (parent is IContainer)
                return ((IContainer)parent).GetItem(context, item);
             else
                throw new SyntaxError("Unknown item/key: " + item.GetType().Name);
        }
    }

}
