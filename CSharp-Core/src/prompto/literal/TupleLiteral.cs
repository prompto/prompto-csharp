using prompto.runtime;
using System;
using prompto.value;
using prompto.type;
using prompto.expression;
using prompto.utils;
using System.Collections.Generic;


namespace prompto.literal
{

    public class TupleLiteral : Literal<TupleValue>
    {

		ExpressionList expressions = null;

		public TupleLiteral()
            : base("()", new TupleValue())
        {
        }

		public TupleLiteral(ExpressionList expressions)
			: base("(" + expressions.ToString() + ")", new TupleValue())
        {
			this.expressions = expressions;
        }
 
		public ExpressionList Expressions {
			get { return expressions; }
		}


		public override void ToDialect(CodeWriter writer) {
			if (expressions != null) {
				writer.append ('(');
				expressions.toDialect (writer);
				writer.append (')');
			} else
				value.ToDialect (writer);
		}


        override
        public IType check(Context context)
        {
            return TupleType.Instance;
        }

        override
		public IValue interpret(Context context)
        {
			if(expressions!=null) {
				List<IValue> list = new List<IValue>();
				foreach(IExpression exp in expressions) 
					list.Add(exp.interpret(context));
				return new TupleValue(list);
			} else 
            	return value;
        }
    }
}
