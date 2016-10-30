using prompto.runtime;
using System;
using prompto.error;
using prompto.value;
using Decimal = prompto.value.Decimal;
using Boolean = prompto.value.Boolean;
using DateTime = prompto.value.DateTime;
using prompto.parser;
using prompto.type;
using prompto.grammar;
using prompto.utils;
using prompto.declaration;
using prompto.store;

namespace prompto.expression
{

	public class CompareExpression : IPredicateExpression, IAssertion
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

		public void ToDialect(CodeWriter writer)
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


		public IType check(Context context)
		{
			IType lt = left.check(context);
			IType rt = right.check(context);
			return lt.checkCompare(context, rt);
		}

		public IValue interpret(Context context)
		{
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
			return compare(context, lval, rval);
		}

		private Boolean compare(Context context, IValue lval, IValue rval)
		{
			Int32 cmp = lval.CompareTo(context, rval);
			switch (oper)
			{
				case CmpOp.GT:
					return Boolean.ValueOf(cmp > 0);
				case CmpOp.LT:
					return Boolean.ValueOf(cmp < 0);
				case CmpOp.GTE:
					return Boolean.ValueOf(cmp >= 0);
				case CmpOp.LTE:
					return Boolean.ValueOf(cmp <= 0);
				default:
					throw new SyntaxError("Illegal compare operand: " + oper.ToString());
			}
		}

		public bool interpretAssert(Context context, TestMethodDeclaration test)
		{
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
			IValue result = compare(context, lval, rval);
			if (result == Boolean.TRUE)
				return true;
			CodeWriter writer = new CodeWriter(test.Dialect, context);
			this.ToDialect(writer);
			String expected = writer.ToString();
			String actual = lval.ToString() + " " + oper.ToString() + " " + rval.ToString();
			test.printFailure(context, expected, actual);
			return false;
		}

		public void interpretQuery(Context context, IQueryBuilder builder)
		{
			String name = null;
			IValue value = null;
			if (left is UnresolvedIdentifier)
			{
				name = ((UnresolvedIdentifier)left).getName();
				value = right.interpret(context);
			}
			else if (left is InstanceExpression)
			{
				name = ((InstanceExpression)left).getName();
				value = right.interpret(context);
			}
			else if (right is UnresolvedIdentifier)
			{
				name = ((UnresolvedIdentifier)right).getName();
				value = left.interpret(context);
			}
			else if (right is InstanceExpression)
			{
				name = ((InstanceExpression)right).getName();
				value = left.interpret(context);
			}
			if (name == null)
				throw new SyntaxError("Unable to interpret predicate");
			else {
				AttributeDeclaration decl = context.findAttribute(name);
				AttributeInfo info = decl == null ? null : decl.getAttributeInfo();
				if (value is IInstance)
					value = ((IInstance)value).GetMember(context, "dbId", false);
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
