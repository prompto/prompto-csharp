using prompto.runtime;
using System;
using prompto.error;
using prompto.value;
using BooleanValue = prompto.value.BooleanValue;
using prompto.type;
using prompto.grammar;
using prompto.utils;
using prompto.declaration;
using prompto.store;

namespace prompto.expression
{

	public class CompareExpression : BaseExpression, IPredicateExpression, IAssertion
	{

		IExpression left;
		CmpOp oper;
		IExpression right;

		public CompareExpression(IExpression left, CmpOp oper, IExpression right)
		{
			this.left = left;
			this.oper = oper;
			this.right = right;
		}

		public override void ToDialect(CodeWriter writer)
		{
			left.ToDialect(writer);
			writer.append(" ");
			OperToDialect(writer);
			writer.append(" ");
			right.ToDialect(writer);
		}

		public void OperToDialect(CodeWriter writer)
		{
			switch (oper)
			{
				case CmpOp.GT:
					writer.append(">");
					break;
				case CmpOp.GTE:
					writer.append(">=");
					break;
				case CmpOp.LT:
					writer.append("<");
					break;
				case CmpOp.LTE:
					writer.append("<=");
					break;
			}
		}


		public override IType check(Context context)
		{
			IType lt = left.check(context);
			IType rt = right.check(context);
			lt.checkCompare(context, rt);
			return BooleanType.Instance;
		}

		public override IValue interpret(Context context)
		{
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
			return compare(context, lval, rval);
		}

		private BooleanValue compare(Context context, IValue lval, IValue rval)
		{
			Int32 cmp = lval.CompareTo(context, rval);
			switch (oper)
			{
				case CmpOp.GT:
					return BooleanValue.ValueOf(cmp > 0);
				case CmpOp.LT:
					return BooleanValue.ValueOf(cmp < 0);
				case CmpOp.GTE:
					return BooleanValue.ValueOf(cmp >= 0);
				case CmpOp.LTE:
					return BooleanValue.ValueOf(cmp <= 0);
				default:
					throw new SyntaxError("Illegal compare operand: " + oper.ToString());
			}
		}

		public bool interpretAssert(Context context, TestMethodDeclaration test)
		{
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
			IValue result = compare(context, lval, rval);
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
			decl.GetIType(context).checkCompare(context, rt);
		}


		public void interpretQuery(Context context, IQueryBuilder builder)
		{
			AttributeDeclaration decl = left.CheckAttribute(context);
			if (decl == null || !decl.Storable)
				throw new SyntaxError("Unable to interpret predicate");
			IValue value = right.interpret(context);
			AttributeInfo info = decl.getAttributeInfo();
			if (value is IInstance)
				value = ((IInstance)value).GetMemberValue(context, "dbId", false);
			MatchOp matchOp = getMatchOp();
			builder.Verify(info, matchOp, value == null ? null : value.GetStorableData());
			switch (oper)
			{
				case CmpOp.GTE:
				case CmpOp.LTE:
					builder.Not();
					break;
			}
		}

		private MatchOp getMatchOp()
		{
			switch (oper)
			{
				case CmpOp.GT:
				case CmpOp.LTE:
					return MatchOp.GREATER;
				case CmpOp.GTE:
				case CmpOp.LT:
					return MatchOp.LESSER;
				default:
					throw new InvalidValueError(oper.ToString());
			}
		}
	}
}
