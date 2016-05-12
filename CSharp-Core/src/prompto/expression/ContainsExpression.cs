using prompto.runtime;
using System;
using prompto.utils;
using prompto.error;
using Boolean = prompto.value.Boolean;
using prompto.value;
using prompto.parser;
using prompto.type;
using prompto.grammar;
using prompto.declaration;

namespace prompto.expression
{

	public class ContainsExpression : IExpression, IAssertion
	{

		IExpression left;
		ContOp oper;
		IExpression right;

		public ContainsExpression (IExpression left, ContOp oper, IExpression right)
		{
			this.left = left;
			this.oper = oper;
			this.right = right;
		}

		public void ToDialect (CodeWriter writer)
		{
			left.ToDialect (writer);
			writer.append (" ");
			ContOpToDialect (writer);
			writer.append (" ");
			right.ToDialect (writer);
		}

		public void ContOpToDialect (CodeWriter writer)
		{
			writer.append (oper.ToString ().ToLower ().Replace ('_', ' '));
		}


		public IType check (Context context)
		{
			IType lt = left.check (context);
			IType rt = right.check (context);
			switch (oper) {
			case ContOp.IN:
			case ContOp.NOT_IN:
				return rt.checkContains (context, lt);
			case ContOp.CONTAINS:
			case ContOp.NOT_CONTAINS:
				return lt.checkContains (context, rt);
			default:
				return lt.checkContainsAllOrAny (context, rt);
			}
		}

		public IValue interpret (Context context)
		{
			IValue lval = left.interpret (context);
			IValue rval = right.interpret (context);
			return interpret (context, lval, rval);
		}

		public IValue interpret (Context context, IValue lval, IValue rval)
		{
			bool? result = null;
			switch (oper) {
			case ContOp.IN:
			case ContOp.NOT_IN:
				if (rval == NullValue.Instance)
					result = false;
				else if (rval is IContainer)
					result = ((IContainer)rval).HasItem (context, lval);
				break;
			case ContOp.CONTAINS:
			case ContOp.NOT_CONTAINS:
				if (lval == NullValue.Instance)
					result = false;
				else if (lval is IContainer)
					result = ((IContainer)lval).HasItem (context, rval);
				break;
			case ContOp.CONTAINS_ALL:
			case ContOp.NOT_CONTAINS_ALL:
				if (lval == NullValue.Instance || rval == NullValue.Instance)
					result = false;
				else if (lval is IContainer && rval is IContainer)
					result = ContainsAll (context, (IContainer)lval, (IContainer)rval);
				break;
			case ContOp.CONTAINS_ANY:
			case ContOp.NOT_CONTAINS_ANY:
				if (lval == NullValue.Instance || rval == NullValue.Instance)
					result = false;
				else if (lval is IContainer && rval is IContainer)
					result = ContainsAny (context, (IContainer)lval, (IContainer)rval);
				break;
			}
			String name = Enum.GetName (typeof(ContOp), oper);
			if (result != null) {
				if (name.StartsWith ("NOT_"))
					result = !result;
				return Boolean.ValueOf (result.Value);
			}
			if (name.EndsWith ("IN")) {
				IValue tmp = lval;
				lval = rval;
				rval = tmp;
			}
			String lowerName = name.ToLower ().Replace ('_', ' ');
			throw new SyntaxError ("Illegal comparison: " + lval.GetType ().Name + " " + lowerName + " " + rval.GetType ().Name);
		}

		public bool ContainsAll (Context context, IContainer container, IContainer items)
		{
			foreach (Object it in items.GetEnumerable(context)) {
				Object item = it;
				if (item is IExpression)
					item = ((IExpression)item).interpret (context);
				if (item is IValue) {
					if (!container.HasItem (context, (IValue)item))
						return false;
				} else
					throw new SyntaxError ("Illegal contain: Text + " + item.GetType ().Name);
			}
			return true;
		}

		public bool ContainsAny (Context context, IContainer container, IContainer items)
		{
			foreach (Object it in items.GetEnumerable(context)) {
				Object item = it;
				if (item is IExpression)
					item = ((IExpression)item).interpret (context);
				if (item is IValue) {
					if (container.HasItem (context, (IValue)item))
						return true;
				} else
					throw new SyntaxError ("Illegal contain: Text + " + item.GetType ().Name);
			}
			return false;
		}

		public bool interpretAssert (Context context, TestMethodDeclaration test)
		{
			IValue lval = left.interpret (context);
			IValue rval = right.interpret (context);
			IValue result = interpret (context, lval, rval);
			if (result == Boolean.TRUE)
				return true;
			CodeWriter writer = new CodeWriter (test.Dialect, context);
			this.ToDialect (writer);
			String expected = writer.ToString ();
			String actual = lval.ToString () + " " + oper.ToString () + " " + rval.ToString ();
			test.printFailure (context, expected, actual);
			return false;
		}
	}
}
