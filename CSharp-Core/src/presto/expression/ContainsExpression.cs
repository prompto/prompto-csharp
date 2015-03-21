using presto.runtime;
using System;
using presto.utils;
using presto.error;
using Boolean = presto.value.Boolean;
using presto.value;
using presto.parser;
using presto.type;
using presto.grammar;

namespace presto.expression
{

    public class ContainsExpression : IExpression
    {

        IExpression left;
        ContOp oper;
        IExpression right;

        public ContainsExpression(IExpression left, ContOp oper, IExpression right)
        {
            this.left = left;
            this.oper = oper;
            this.right = right;
        }

        public void ToDialect(CodeWriter writer)
        {
			left.ToDialect(writer);
			writer.append(" ");
			ContOpToDialect(writer);
			writer.append(" ");
			right.ToDialect(writer);
        }

		public void ContOpToDialect(CodeWriter writer) {
			writer.append(oper.ToString().ToLower().Replace('_', ' '));
		}


        public IType check(Context context)
        {
            IType lt = left.check(context);
            IType rt = right.check(context);
            switch (oper)
            {
                case ContOp.IN:
                case ContOp.NOT_IN:
                    return rt.checkContains(context, lt);
                case ContOp.CONTAINS:
                case ContOp.NOT_CONTAINS:
                    return lt.checkContains(context, rt);
                default:
                    return lt.checkContainsAllOrAny(context, rt);
            }
        }

		public IValue interpret(Context context)
        {
            String name = Enum.GetName(typeof(ContOp), oper);
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
            bool? result = null;
            switch (oper)
            {
                case ContOp.IN:
                case ContOp.NOT_IN:
                    if(rval is IContainer)
                        result = ((IContainer)rval).HasItem(context, lval);
                    break;
                case ContOp.CONTAINS:
                case ContOp.NOT_CONTAINS:
                    if(lval is IContainer)
                        result = ((IContainer)lval).HasItem(context, rval);
                    break;
                case ContOp.CONTAINS_ALL:
                case ContOp.NOT_CONTAINS_ALL:
                    if (lval is IContainer && rval is IContainer)
                        result = ContainsAll(context, (IContainer)lval, (IContainer)rval);
                    break;
                case ContOp.CONTAINS_ANY:
                case ContOp.NOT_CONTAINS_ANY:
                    if (lval is IContainer && rval is IContainer)
                        result = ContainsAny(context, (IContainer)lval, (IContainer)rval);
                    break;
            }
            if (result != null)
            {
                if (name.StartsWith("NOT_"))
                    result = !result;
                return Boolean.ValueOf(result.Value);
            }
            if (name.EndsWith("IN"))
            {
				IValue tmp = lval;
                lval = rval;
                rval = tmp;
            }
            String lowerName = name.ToLower().Replace('_', ' ');
            throw new SyntaxError("Illegal comparison: " + lval.GetType().Name + " " + lowerName + " " + rval.GetType().Name);
        }

        public bool ContainsAll(Context context, IContainer container, IContainer items)
        {
            foreach (Object it in items.GetItems(context))
            {
                Object item = it;
                if (item is IExpression)
                    item = ((IExpression)item).interpret(context);
                if (item is IValue)
                {
                    if (!container.HasItem(context, (IValue)item))
                        return false;
                }
                else
                    throw new SyntaxError("Illegal contain: Text + " + item.GetType().Name);
            }
            return true;
        }
        public bool ContainsAny(Context context, IContainer container, IContainer items)
        {
            foreach (Object it in items.GetItems(context))
            {
                Object item = it;
                if (item is IExpression)
                    item = ((IExpression)item).interpret(context);
                if (item is IValue)
                {
                    if (container.HasItem(context, (IValue)item))
                        return true;
                }
                else
                    throw new SyntaxError("Illegal contain: Text + " + item.GetType().Name);
            }
            return false;
        }
    }
}
