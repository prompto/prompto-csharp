using System;
using System.Collections.Generic;
using prompto.error;
using prompto.grammar;
using prompto.runtime;
using prompto.statement;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.expression
{
	public class ArrowExpression : IExpression
	{

		public ArrowExpression(IdentifierList args, String argsSuite, String arrowSuite)
		{
			this.Arguments = args;
			this.ArgsSuite = argsSuite;
			this.ArrowSuite = arrowSuite;
		}

		public IdentifierList Arguments { get; set; }
		public String ArgsSuite { get; set; }
		public String ArrowSuite { get; set; }
		public StatementList Statements { get; set; }

		public IType check(Context context)
		{
			throw new NotSupportedException();
		}

		public IValue interpret(Context context)
		{
			return this.Statements.interpret(context);
		}

		public void ToDialect(CodeWriter writer)
		{
			ArgsToDialect(writer);
			writer.append(ArgsSuite);
			writer.append("=>");
			writer.append(ArrowSuite);
			BodyToDialect(writer);
		}

		private void ArgsToDialect(CodeWriter writer)
		{
			if (Arguments == null || Arguments.Count == 0)
				writer.append("()");
			else if (Arguments.Count == 1)
				writer.append(Arguments[0]);
			else
			{
				writer.append("(");
				Arguments.ToDialect(writer, false);
				writer.append(")");
			}

		}

		private void BodyToDialect(CodeWriter writer)
		{
			if (Statements.Count == 1 && Statements[0] is ReturnStatement)
				((ReturnStatement)Statements[0]).getExpression().ToDialect(writer);
			else
			{
				writer.append("{").newLine().indent();
				Statements.ToDialect(writer);
				writer.newLine().dedent().append("}").newLine();
			}
		}


		public IExpression Expression
		{
			set
			{
				IStatement stmt = new ReturnStatement(value);
				Statements = new StatementList(stmt);
			}
		}

		public Comparer<IValue> getComparer(Context context, IType itemType, bool descending)
		{
			switch(Arguments.Count) {
			case 1:
				return new ArrowComparer1Arg(context, itemType, descending, this);
			case 2:
				return new ArrowComparer2Args(context, itemType, descending, this);
			default:
				throw new SyntaxError("Expecting 1 or 2 parameters only!"); 			
			}
		}

		internal abstract class ArrowComparer : Comparer<IValue>
		{
			protected Context context;
			protected IType itemType;
			protected bool descending;
			protected ArrowExpression arrow;

			internal ArrowComparer(Context context, IType itemType, bool descending, ArrowExpression arrow)
			{
				this.context = context;
				this.itemType = itemType;
				this.descending = descending;
				this.arrow = arrow;
			}

			protected Context registerArrowArgs(Context context)
			{
				foreach (String argument in arrow.Arguments)
					context.registerValue(new Variable(argument, itemType));
				return context;
			}
		}

		internal class ArrowComparer1Arg : ArrowComparer
		{
			internal ArrowComparer1Arg(Context context, IType itemType, bool descending, ArrowExpression arrow)
				: base(context, itemType, descending, arrow)
			{
			}

			public override int Compare(IValue o1, IValue o2)
			{
				Context local = registerArrowArgs(context.newLocalContext());
				local.setValue(arrow.Arguments[0], o1);
				IValue key1 = arrow.Statements.interpret(local);
				local.setValue(arrow.Arguments[0], o2);
				IValue key2 = arrow.Statements.interpret(local);
				int result = key1.CompareTo(context, key2);
				return descending? -result : result;
			}
		}

		internal class ArrowComparer2Args : ArrowComparer
		{
			internal ArrowComparer2Args(Context context, IType itemType, bool descending, ArrowExpression arrow)
				: base(context, itemType, descending, arrow)
			{
			}

			public override int Compare(IValue o1, IValue o2)
			{
				Context local = registerArrowArgs(context.newLocalContext());
				local.setValue(arrow.Arguments[0], o1);
				local.setValue(arrow.Arguments[1], o2);
				IValue value = arrow.Statements.interpret(local);
				if(!(value is Integer))
					throw new SyntaxError("Expecting an Integer as result of key body!");
				long result = ((Integer)value).IntegerValue;
				return (int)(descending? -result : result);
			}
		}
	}

}
