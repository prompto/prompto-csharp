using prompto.runtime;
using System;
using System.Collections.Generic;
using prompto.value;
using Decimal = prompto.value.DecimalValue;
using BooleanValue = prompto.value.BooleanValue;
using prompto.parser;
using prompto.type;
using prompto.grammar;
using prompto.utils;
using prompto.error;
using prompto.declaration;
using prompto.store;

namespace prompto.expression
{

    public class EqualsExpression : BaseExpression, IPredicateExpression, IAssertion
    {

        IExpression left;
        EqOp oper;
        IExpression right;

        public EqualsExpression(IExpression left, EqOp oper, IExpression right)
        {
            this.left = left;
            this.oper = oper;
            this.right = right;
        }

        public override void ToDialect(CodeWriter writer)
        {
            left.ToDialect(writer);
            writer.append(operatorToString(writer.getDialect()));
            right.ToDialect(writer);
        }

        const string VOWELS = "AEIO"; // sufficient here

        public String operatorToString(Dialect dialect)
        {
            switch (oper)
            {
                case EqOp.IS:
                    return " is ";
                case EqOp.IS_NOT:
                    return " is not ";
                case EqOp.IS_A:
                    return " is a" + (VOWELS.IndexOf(right.ToString()[0]) >= 0 ? "n " : " ");
                case EqOp.IS_NOT_A:
                    return " is not a" + (VOWELS.IndexOf(right.ToString()[0]) >= 0 ? "n " : " ");
                case EqOp.EQUALS:
                    switch (dialect)
                    {
                        case Dialect.E:
                            return " = ";
                        case Dialect.O:
                        case Dialect.M:
                            return " == ";
                        default:
                            throw new Exception("Unimplemented!");
                    }
                case EqOp.NOT_EQUALS:
                    switch (dialect)
                    {
                        case Dialect.E:
                            return " <> ";
                        case Dialect.O:
                        case Dialect.M:
                            return " != ";
                        default:
                            throw new Exception("Unimplemented!");
                    }
                case EqOp.ROUGHLY:
                    switch (dialect)
                    {
                        case Dialect.E:
                            return " ~ ";
                        case Dialect.O:
                        case Dialect.M:
                            return " ~= ";
                        default:
                            throw new Exception("Unimplemented!");
                    }
                case EqOp.CONTAINS:
                    return " contains ";
                case EqOp.NOT_CONTAINS:
                    return " not contains ";
                default:
                    throw new Exception("Unimplemented!");
            }
        }

        public override IType check(Context context)
        {
            IType lt = left.check(context);
            IType rt = right.check(context);
            checkOperator(context, lt, rt);
            return BooleanType.Instance; 
        }

        private void checkOperator(Context context, IType lt, IType rt)
        {
            if (oper == EqOp.CONTAINS || oper == EqOp.NOT_CONTAINS)
            {
                if (lt is ContainerType)
                    lt = ((ContainerType)lt).GetItemType();
                if (rt is ContainerType)
                    rt = ((ContainerType)rt).GetItemType();
                if (!(lt is TextType) || !(rt is TextType || rt is CharacterType))
                    throw new SyntaxError("'contains' is only supported for textual values!");
            }
            // can compare EQUALS all objects
        }

        public override IValue interpret(Context context)
        {
            IValue lval = left.interpret(context);
            if (lval == null)
                lval = NullValue.Instance;
            IValue rval = right.interpret(context);
            if (rval == null)
                rval = NullValue.Instance;
            return interpret(context, lval, rval);
        }

        public IValue interpret(Context context, IValue lval, IValue rval)
        {
            bool equal = false;
            switch (oper)
            {
                case EqOp.IS:
                    equal = lval == rval;
                    break;
                case EqOp.IS_NOT:
                    equal = lval != rval;
                    break;
                case EqOp.IS_A:
                    equal = isA(context, lval, rval);
                    break;
                case EqOp.IS_NOT_A:
                    equal = !isA(context, lval, rval);
                    break;
                case EqOp.EQUALS:
                    equal = lval.Equals(context, rval);
                    break;
                case EqOp.NOT_EQUALS:
                    equal = !lval.Equals(context, rval);
                    break;
                case EqOp.CONTAINS:
                    equal = lval.Contains(context, rval);
                    break;
                case EqOp.NOT_CONTAINS:
                    equal = !lval.Contains(context, rval);
                    break;
                case EqOp.ROUGHLY:
                    equal = lval.Roughly(context, (IValue)rval);
                    break;
            }
            return BooleanValue.ValueOf(equal);
        }

        private bool isA(Context context, IValue lval, IValue rval)
        {
            if (rval is TypeValue)
            {
                IType actual = lval.GetIType();
                if (actual == NullType.Instance)
                    return false;
                IType toCheck = ((TypeValue)rval).GetValue();
                return toCheck.isAssignableFrom(context, actual);
            }
            else
                return false;
        }


        public Context downcast(Context context, bool setValue)
        {
            if (oper == EqOp.IS_A)
            {
                String name = readLeftName();
                if (name != null)
                {
                    INamed value = context.getRegisteredValue<INamed>(name);
                    IType targetType = ((TypeExpression)right).getType().Resolve(context);
                    IType sourceType = value.GetIType(context);
                    if (sourceType.IsMutable(context))
                        targetType = targetType.AsMutable(context, true);
                    Context local = context.newChildContext();
                    value = new LinkedVariable(targetType, value);
                    local.registerValue(value, false);
                    if (setValue)
                        local.setValue(name, new LinkedValue(context, targetType));
                    context = local;
                }
            }
            return context;
        }

        private String readLeftName()
        {
            if (left is InstanceExpression)
                return ((InstanceExpression)left).getName();
            else if (left is UnresolvedIdentifier)
                return ((UnresolvedIdentifier)left).getName();
            return null;
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
            String actual = lval.ToString() + operatorToString(test.Dialect) + rval.ToString();
            test.printAssertionFailed(context, expected, actual);
            return false;
        }

        public void checkQuery(Context context)
        {
            AttributeDeclaration decl = left.CheckAttribute(context);
            if (decl == null)
                throw new SyntaxError("Expected an attribute, found: " + left.ToString());
            else if (!decl.Storable)
                throw new SyntaxError(decl.GetName() + " is not storable");
            IType rt = right.check(context);
            checkOperator(context, decl.getIType(), rt);
        }

        public void interpretQuery(Context context, IQueryBuilder builder)
        {
            AttributeDeclaration decl = left.CheckAttribute(context);
            if (decl == null || !decl.Storable)
                throw new SyntaxError("Unable to interpret predicate");
            IValue value = right.interpret(context);
            if (value is IInstance)
                value = ((IInstance)value).GetMemberValue(context, "dbId", false);
            AttributeInfo info = decl.getAttributeInfo();
            Object data = value == null ? null : value.GetStorableData();
            MatchOp match = GetMatchOp();
            builder.Verify<Object>(info, match, data);
            if (oper == EqOp.NOT_EQUALS || oper == EqOp.IS_NOT || oper == EqOp.NOT_CONTAINS)
                builder.Not();
        }

        private MatchOp GetMatchOp()
        {
            switch (oper)
            {
                case EqOp.IS:
                case EqOp.IS_NOT:
                case EqOp.EQUALS:
                case EqOp.NOT_EQUALS:
                    return MatchOp.EQUALS;
                case EqOp.ROUGHLY:
                    return MatchOp.ROUGHLY;
                case EqOp.CONTAINS:
                case EqOp.NOT_CONTAINS:
                    return MatchOp.CONTAINS;
                default:
                    throw new NotSupportedException();
            }
        }

 
    }

}
