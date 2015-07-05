using prompto.runtime;
using System;
using System.Collections.Generic;
using prompto.value;
using Decimal = prompto.value.Decimal;
using Boolean = prompto.value.Boolean;
using prompto.parser;
using prompto.type;
using prompto.grammar;
using prompto.utils;
using prompto.error;
using prompto.declaration;

namespace prompto.expression
{

	public class EqualsExpression : IExpression, IAssertion
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
			writer.append(operatorToString (writer.getDialect()));
			right.ToDialect(writer);
        }
 		
		const string VOWELS = "AEIO"; // sufficient here

		public String operatorToString(Dialect dialect) {
			switch(oper) {
			case EqOp.IS:
				return " is ";
			case EqOp.IS_NOT:
				return " is not ";
			case EqOp.IS_A:
				return " is a" + (VOWELS.IndexOf (right.ToString () [0]) >= 0 ? "n " : " ");
			case EqOp.IS_NOT_A:
				return " is not a" + (VOWELS.IndexOf (right.ToString () [0]) >= 0 ? "n " : " ");
			case EqOp.EQUALS:
				switch(dialect) {
				case Dialect.E:
					return " = ";
				case Dialect.O:
				case Dialect.S:
					return " == ";
				default:
					throw new Exception ("Unimplemented!");
				}
			case EqOp.NOT_EQUALS:
				switch(dialect) {
				case Dialect.E:
					return " <> ";
				case Dialect.O:
				case Dialect.S:
					return " != ";
				default:
					throw new Exception ("Unimplemented!");
				}
			case EqOp.ROUGHLY:
				switch(dialect) {
				case Dialect.E:
					return " ~ ";
				case Dialect.O:
				case Dialect.S:
					return " ~= ";
				default:
					throw new Exception ("Unimplemented!");
				}
			default:
				throw new Exception ("Unimplemented!");
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
			IValue lval = left.interpret (context);
			if(lval==null)
				lval = NullValue.Instance;
			IValue rval = right.interpret (context);
			if(rval==null)
				rval = NullValue.Instance;
			return interpret (context, lval, rval);
		}

		public IValue interpret(Context context, IValue lval, IValue rval)
		{
			bool equal = false;
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

		public bool interpretAssert(Context context, TestMethodDeclaration test) {
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
			IValue result = interpret(context, lval, rval);
			if(result==Boolean.TRUE) 
				return true;
			CodeWriter writer = new CodeWriter(test.Dialect, context);
			this.ToDialect(writer);
			String expected = writer.ToString();
			String actual = lval.ToString() + operatorToString(test.Dialect) + rval.ToString();
			test.printFailure(context, expected, actual);
			return false;
		}

			
    }

}
