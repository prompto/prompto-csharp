using System;
using prompto.runtime;
using prompto.parser;
using prompto.type;
using prompto.expression;
using prompto.utils;
using prompto.value;


namespace prompto.literal
{

    public class RangeLiteral : BaseExpression, IExpression
    {

        IExpression first;
        IExpression last;

        public RangeLiteral(IExpression first, IExpression last)
        {
            this.first = first;
            this.last = last;
        }

		
		public override String ToString() {
			return "[" + first.ToString() + ".." + last.ToString() + "]";
		}

        public override void ToDialect(CodeWriter writer)
        {
			writer.append("[");
			first.ToDialect(writer);
			writer.append("..");
			last.ToDialect(writer);
			writer.append("]");
        }

        public IExpression getFirst()
        {
            return first;
        }

        public IExpression getLast()
        {
            return last;
        }

        public override IType check(Context context)
        {
            IType firstType = first.check(context);
            IType lastType = last.check(context);
            return firstType.checkRange(context, lastType);
        }

		public override IValue interpret(Context context)
        {
            IType type = first.check(context);
			if ("IntegerLimits".Equals(type.GetTypeName()))
                type = IntegerType.Instance;
			IValue of = first.interpret(context);
			IValue ol = last.interpret(context);
            return type.newRange(of, ol);
        }

    }

}