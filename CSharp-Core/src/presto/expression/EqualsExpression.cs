using presto.runtime;
using System;
using System.Collections.Generic;
using presto.value;
using Decimal = presto.value.Decimal;
using Boolean = presto.value.Boolean;
using presto.parser;
using presto.type;
using presto.grammar;
using presto.utils;
using presto.error;

namespace presto.expression
{

    public class EqualsExpression : IExpression
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

        public void ToDialect(CodeWriter writer)
        {
			left.ToDialect (writer);
			writer.append (' ');
			EqOpToDialect (writer);
			writer.append (' ');
			right.ToDialect(writer);
        }
 		
		const string VOWELS = "AEIO"; // sufficient here

		public void EqOpToDialect(CodeWriter writer) {
			switch(oper) {
			case EqOp.IS:
				writer.append("is");
				break;
			case EqOp.IS_NOT:
				writer.append("is not");
				break;
			case EqOp.IS_A:
				writer.append("is a");
				if (VOWELS.IndexOf (right.ToString () [0]) >= 0)
					writer.append ("n");
				break;
			case EqOp.IS_NOT_A:
				writer.append ("is not a");
				if (VOWELS.IndexOf (right.ToString () [0]) >= 0)
					writer.append ("n");
				break;
			case EqOp.EQUALS:
				switch(writer.getDialect()) {
				case Dialect.E:
					writer.append('=');
					break;
				case Dialect.O:
				case Dialect.P:
					writer.append("==");
					break;
				}
				break;
			case EqOp.NOT_EQUALS:
				switch(writer.getDialect()) {
				case Dialect.E:
					writer.append("<>");
					break;
				case Dialect.O:
				case Dialect.P:
					writer.append("!=");
					break;
				}
				break;
			case EqOp.ROUGHLY:
				switch(writer.getDialect()) {
				case Dialect.E:
					writer.append("~");
					break;
				case Dialect.O:
				case Dialect.P:
					writer.append("~=");
					break;
				}
				break;
			}
		}

        public IType check(Context context)
        {
            left.check(context);
            right.check(context);
            return BooleanType.Instance; // can compare all objects
        }

		public IValue interpret(Context context)
        {
			bool equal = false;
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
			switch (oper) {
			case EqOp.IS:
				equal = lval == rval;
				break;
			case EqOp.IS_NOT:
				equal = lval != rval;
				break;
			case EqOp.IS_A:
				equal = isA(context,lval,rval);
				break;
			case EqOp.IS_NOT_A:
				equal = !isA(context,lval,rval);
				break;
			case EqOp.EQUALS:
				equal = lval.Equals (context, rval);
				break;
			case EqOp.NOT_EQUALS:
				equal = !lval.Equals (context, rval);
				break;
			case EqOp.ROUGHLY:
				equal = lval.Roughly (context, (IValue)rval);
				break;
			}
			return Boolean.ValueOf(equal);
        }

		private bool isA(Context context, IValue lval, IValue rval) {
			if(rval is TypeValue) {
				IType actual = lval.GetType(context);
				IType toCheck = ((TypeValue)rval).GetValue();
				return actual.isAssignableTo(context, toCheck);
			} else
				return false;
		}


		public Context downCast(Context context, bool setValue) {
			if(oper==EqOp.IS_A) {
				String name = readLeftName();
				if(name!=null) {
					INamed value = context.getRegisteredValue<INamed>( name);
					IType type = ((TypeExpression)right).getType();
					Context local = context.newChildContext();
					value = new LinkedVariable(type, value);
					local.registerValue(value, false);
					if(setValue)
						local.setValue(name, new LinkedValue(context, type));
					context = local;
				}
			}
			return context;
		}

		private String readLeftName() {
			if(left is InstanceExpression)
				return ((InstanceExpression)left).getName();
			else if(left is UnresolvedIdentifier)
				return ((UnresolvedIdentifier)left).getName();
			return null;
		}
			
    }

}
