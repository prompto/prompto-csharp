
using System;
using prompto.expression;
using prompto.grammar;
using prompto.parser;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.statement {
	
	public class AsynchronousCall : UnresolvedCall
	{

		String resultName;
		StatementList andThen;

		public AsynchronousCall(UnresolvedCall call, String resultName, StatementList andThen)
			: base(call)
		{
            this.resultName = resultName;
			this.andThen = andThen;
		}

		public AsynchronousCall(IExpression caller, ArgumentAssignmentList assignments, String resultName, StatementList andThen)
			: base(caller, assignments)
		{
			this.resultName = resultName;
			this.andThen = andThen;
		}

		public override bool IsSimple { get { return false; } }


		public override void ToDialect(CodeWriter writer)
		{
			base.ToDialect(writer);
			writer.append(" then");
			if(resultName!=null)
				writer.append(" with ").append(resultName);
			if (writer.getDialect() == Dialect.O)
				writer.append(" {");
			else
				writer.append(":");
			writer = writer.newLine().indent();
			andThen.ToDialect(writer);
			writer = writer.dedent();
			if (writer.getDialect() == Dialect.O)
				writer.append("}");
		}

		public override IType check(Context context)
		{
			IType type = resolveAndCheck(context);
			context = context.newChildContext();
			if (resultName != null)
				context.registerValue(new Variable(resultName, type));
			andThen.check(context, VoidType.Instance);
			return VoidType.Instance;
		}

		public override IValue interpret(Context context)
		{
			IType type = resolveAndCheck(context);
			IValue result = resolved.interpret(context);
			context = context.newChildContext();
			if (resultName != null)
			{
				context.registerValue(new Variable(resultName, type));
				context.setValue(resultName, result);
			}
			andThen.interpret(context);
			return VoidResult.Instance;
		}

	}

}
