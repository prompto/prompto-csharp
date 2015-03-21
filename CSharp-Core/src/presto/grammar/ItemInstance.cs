using System;
using presto.runtime;
using presto.error;
using presto.value;
using presto.parser;
using presto.expression;
using presto.type;
using presto.utils;


namespace presto.grammar {

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

        public void ToDialect(CodeWriter writer)
        {
			parent.ToDialect (writer);
			writer.append ("[");
			item.ToDialect (writer);
			writer.append("]");
        }

        public void checkAssignValue(Context context, IExpression expression)
        {
            parent.checkAssignElement(context);
            IType itemType = item.check(context);
            if (itemType != IntegerType.Instance)
                throw new SyntaxError("Expecting an Integer, got:" + itemType.ToString());
        }

        public void checkAssignMember(Context context, String memberName)
        {
            // TODO Auto-generated method stub

        }

        public void checkAssignElement(Context context)
        {
            // TODO Auto-generated method stub

        }

        public void assign(Context context, IExpression expression)
        {
            Object obj = parent.interpret(context);
            if (!(obj is ListValue))
                throw new InvalidDataError("Expected a List, got:" + obj.GetType().Name);
            ListValue list = (ListValue)obj;
            Object idx = item.interpret(context);
            if (!(idx is Integer))
                throw new InvalidDataError("Expected an Integer, got:" + idx.GetType().Name);
            int index = (int)((Integer)idx).IntegerValue;
            if (index < 1 || index > list.Count)
                throw new IndexOutOfRangeError();
            list[index - 1] = expression.interpret(context);
        }

        public Object interpret(Context context)
        {
            Object parent = this.parent.interpret(context);
            Object item = this.item.interpret(context);
            if (parent is IContainer && item is IValue)
                return ((IContainer)parent).GetItem(context, (IValue)item);
             else
                throw new SyntaxError("Unknown item/key: " + item.GetType().Name);
        }
    }

}
