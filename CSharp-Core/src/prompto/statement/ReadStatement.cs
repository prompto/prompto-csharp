


using System;
using prompto.expression;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;
using prompto.parser;
using prompto.grammar;

namespace prompto.statement
{
	public class ReadStatement : ReadAllExpression, IStatement
	{

		ThenWith thenWith;


		public ReadStatement(IExpression resource, ThenWith thenWith)
			: base(resource)
		{
			this.thenWith = thenWith;
		}

		public bool CanReturn
        {
			get {
				return false;
			}
        }
	
        public bool IsSimple
        {
			get
			{
				return false;
			}
		}

        public override IType check(Context context)
		{
			base.check(context);
			return thenWith.check(context, TextType.Instance);
		}

		public override IValue interpret(Context context)
		{
			IValue result = base.interpret(context);
			return thenWith.interpret(context, result);
		}

		public override void ToDialect(CodeWriter writer)
		{
			base.ToDialect(writer);
			thenWith.ToDialect(writer, TextType.Instance);
		}
	}
}
