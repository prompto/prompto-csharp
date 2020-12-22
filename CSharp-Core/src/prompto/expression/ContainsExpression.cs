using prompto.runtime;
using System;
using prompto.utils;
using prompto.error;
using prompto.value;
using prompto.type;
using prompto.grammar;
using prompto.declaration;
using prompto.store;
using System.Linq;

namespace prompto.expression
{

    public class ContainsExpression : BaseExpression, IPredicateExpression, IAssertion
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

        public override void ToDialect(CodeWriter writer)
        {
            left.ToDialect(writer);
            writer.append(" ");
            ContOpToDialect(writer);
            writer.append(" ");
            if (right is PredicateExpression)
                ((PredicateExpression)right).ContainsToDialect(writer);
            else
                right.ToDialect(writer);
        }

        public void ContOpToDialect(CodeWriter writer)
        {
            writer.append(oper.ToString().ToLower().Replace('_', ' '));
        }


        public override IType check(Context context)
        {
            if (right is PredicateExpression)
                return checkPredicate(context);
            else
                return checkValue(context);
        }

        private IType checkPredicate(Context context)
        {
            IType lt = left.check(context);
            if (lt is IterableType)
            {
                IType itemType = ((IterableType)lt).GetItemType();
                ArrowExpression arrow = ((PredicateExpression)right).ToArrowExpression();
                return arrow.CheckFilter(context, itemType);
            }
            else
                throw new SyntaxError("Expecting collection");
        }

        IType checkValue(Context context)
        {
            IType lt = left.check(context);
            IType rt = right.check(context);
            checkOperator(context, lt, rt);
            return BooleanType.Instance;
        }

        private void checkOperator(Context context, IType lt, IType rt)
        {
            switch (oper)
            {
                case ContOp.IN:
                case ContOp.NOT_IN:
                    rt.checkContains(context, lt);
                    break;
                case ContOp.HAS:
                case ContOp.NOT_HAS:
                    lt.checkContains(context, rt);
                    break;
                default:
                    lt.checkContainsAllOrAny(context, rt);
                    break;
            }
        }

        public override IValue interpret(Context context)
        {
            if (right is PredicateExpression)
                return interpretPredicate(context);

            else
                return interpretValue(context);
        }

        IValue interpretPredicate(Context context)
        {
            IValue lval = left.interpret(context);
            if (lval is IContainer)
            {

                IType itemType = ((ContainerType)lval.GetIType()).GetItemType();
                ArrowExpression arrow = ((PredicateExpression)right).ToArrowExpression();
                Predicate<IValue> predicate = arrow.GetFilter(context, itemType);
                return interpretPredicate(context, (IContainer)lval, predicate);
            }
            else
                throw new SyntaxError("Expecting collection");

        }
        private IValue interpretPredicate(Context context, IContainer lval, Predicate<IValue> predicate)
        {
            bool? result = null;
            switch (oper)
            {
                case ContOp.HAS_ALL:
                case ContOp.NOT_HAS_ALL:
                    result = allMatch(context, lval, predicate);
                    break;
                case ContOp.HAS_ANY:
                case ContOp.NOT_HAS_ANY:
                    result = anyMatch(context, lval, predicate);
                    break;
            }
            if (result != null)
            {
                if (oper.ToString().StartsWith("NOT_"))
                    result = !result;
                return BooleanValue.ValueOf(result.Value);
            }
            String lowerName = oper.ToString().ToLower().Replace('_', ' ');
            throw new SyntaxError("Illegal filter: " + lval.GetType().Name + " " + lowerName);
        }

        public bool allMatch(Context context, IContainer container, Predicate<IValue> predicate)
        {
            return container.GetEnumerable(context).All(item => predicate(item));
        }

        public bool anyMatch(Context context, IContainer container, Predicate<IValue> predicate)
        {
            return container.GetEnumerable(context).Any(item => predicate(item));
        }

        IValue interpretValue(Context context)
        {
            IValue lval = left.interpret(context);
            IValue rval = right.interpret(context);
            return interpret(context, lval, rval);
        }

        public IValue interpret(Context context, IValue lval, IValue rval)
        {
            bool? result = null;
            switch (oper)
            {
                case ContOp.IN:
                case ContOp.NOT_IN:
                    if (rval == NullValue.Instance)
                        result = false;
                    else if (rval is IContainer)
                        result = ((IContainer)rval).HasItem(context, lval);
                    break;
                case ContOp.HAS:
                case ContOp.NOT_HAS:
                    if (lval == NullValue.Instance)
                        result = false;
                    else if (lval is IContainer)
                        result = ((IContainer)lval).HasItem(context, rval);
                    break;
                case ContOp.HAS_ALL:
                case ContOp.NOT_HAS_ALL:
                    if (lval == NullValue.Instance || rval == NullValue.Instance)
                        result = false;
                    else if (lval is IContainer && rval is IContainer)
                        result = HasAll(context, (IContainer)lval, (IContainer)rval);
                    break;
                case ContOp.HAS_ANY:
                case ContOp.NOT_HAS_ANY:
                    if (lval == NullValue.Instance || rval == NullValue.Instance)
                        result = false;
                    else if (lval is IContainer && rval is IContainer)
                        result = HasAny(context, (IContainer)lval, (IContainer)rval);
                    break;
            }
            String name = Enum.GetName(typeof(ContOp), oper);
            if (result != null)
            {
                if (name.StartsWith("NOT_"))
                    result = !result;
                return BooleanValue.ValueOf(result.Value);
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

        public bool HasAll(Context context, IContainer container, IContainer items)
        {
            foreach (Object it in items.GetEnumerable(context))
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

        public bool HasAny(Context context, IContainer container, IContainer items)
        {
            foreach (Object it in items.GetEnumerable(context))
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

        public bool interpretAssert(Context context, TestMethodDeclaration test)
        {
            IValue lval = left.interpret(context);
            IValue rval = right.interpret(context);
            IValue result = interpret(context, lval, rval);
            if (result == BooleanValue.TRUE)
                return true;
            CodeWriter writer = new CodeWriter(test.Dialect, context);
            this.ToDialect(writer);
            String expected = writer.ToString();
            String actual = lval.ToString() + " " + oper.ToString() + " " + rval.ToString();
            test.printAssertionFailed(context, expected, actual);
            return false;
        }

        public void checkQuery(Context context)
        {
            AttributeDeclaration decl = left.CheckAttribute(context);
            if (decl == null)
                throw new SyntaxError("Expected an attribute, got: " + left.ToString());
            else if (!decl.Storable)
                throw new SyntaxError(decl.GetName() + " is not storable");
            IType rt = right.check(context);
            checkOperator(context, decl.getIType(), rt);
        }


        public void interpretQuery(Context context, IQueryBuilder builder)
        {
            AttributeDeclaration decl = left.CheckAttribute(context);
            if (decl == null || !decl.Storable)
                throw new SyntaxError("Unable to interpret predicate");
            IValue value = right.interpret(context);
            MatchOp matchOp = getMatchOp(context, decl.getIType(), value.GetIType(), this.oper, false);
            if (value is IInstance)
                value = ((IInstance)value).GetMemberValue(context, "dbId", false);
            AttributeInfo info = decl.getAttributeInfo();
            Object data = value == null ? null : value.GetStorableData();
            builder.Verify<Object>(info, matchOp, data);
            if (oper.ToString().StartsWith("NOT_"))
                builder.Not();
        }

        private MatchOp getMatchOp(Context context, IType fieldType, IType valueType, ContOp oper, bool reverse)
        {
            if (reverse)
            {
                ContOp? reversed = oper.reverse();
                if (!reversed.HasValue)
                    throw new SyntaxError("Cannot reverse " + this.oper);
                return getMatchOp(context, valueType, fieldType, reversed.Value, false);
            }
            if ((fieldType == TextType.Instance || valueType == CharacterType.Instance) &&
                    (valueType == TextType.Instance || valueType == CharacterType.Instance))
            {
                switch (oper)
                {
                    case ContOp.HAS:
                    case ContOp.NOT_HAS:
                        return MatchOp.CONTAINS;
                }
            }
            if (valueType is ContainerType)
            {
                switch (oper)
                {
                    case ContOp.IN:
                    case ContOp.NOT_IN:
                        return MatchOp.IN;
                }
            }
            if (fieldType is ContainerType)
            {
                switch (oper)
                {
                    case ContOp.HAS:
                    case ContOp.NOT_HAS:
                        return MatchOp.HAS;
                }
            }
            throw new SyntaxError("Unsupported operator: " + oper.ToString());
        }


    }
}
