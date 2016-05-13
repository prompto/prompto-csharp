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

		bool mutable = false;
		ExpressionList expressions = null;

		public TupleLiteral(bool mutable)
            : base("()", new TupleValue())
        {
			this.mutable = mutable;
        }

		public TupleLiteral(ExpressionList expressions, bool mutable)
			: base(ToTupleString(expressions), new TupleValue())
        {
			this.expressions = expressions;
			this.mutable = mutable;
        }

		private static String ToTupleString(ExpressionList expressions) {
			return "(" + expressions.ToString() + (expressions.Count==1 ? "," : "") + ")";
		}
 
		public ExpressionList Expressions {
			get { return expressions; }
		}


		public override void ToDialect(CodeWriter writer) {
			if(mutable)
				writer.append ("mutable ");
			if (expressions != null) {
				writer.append ('(');
				expressions.toDialect (writer);
				if(expressions.Count==1)
					writer.append (',');
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
				return new TupleValue(list, mutable);
			} else 
            	return value;
        }
    }
}
