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
using prompto.store;

namespace prompto.expression
{

	public class EqualsExpression : BaseExpression, IPredicateExpression, IAssertion
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

        public override void ToDialect(CodeWriter writer)
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
				case Dialect.M:
					return " == ";
				default:
					throw new Exception ("Unimplemented!");
				}
			case EqOp.NOT_EQUALS:
				switch(dialect) {
				case Dialect.E:
					return " <> ";
				case Dialect.O:
				case Dialect.M:
					return " != ";
				default:
					throw new Exception ("Unimplemented!");
				}
			case EqOp.ROUGHLY:
				switch(dialect) {
				case Dialect.E:
					return " ~ ";
				case Dialect.O:
				case Dialect.M:
					return " ~= ";
				default:
					throw new Exception ("Unimplemented!");
				}
			case EqOp.CONTAINS:
				return " contains ";
			case EqOp.NOT_CONTAINS:
				return " not contains ";
			default:
				throw new Exception ("Unimplemented!");
			}
		}

        public override IType check(Context context)
        {
            IType lt = left.check(context);
            IType rt = right.check(context);
			if (oper == EqOp.CONTAINS || oper == EqOp.NOT_CONTAINS)
			{
				if (lt is ContainerType)
					lt = ((ContainerType)lt).GetItemType();
				if (rt is ContainerType)
					rt = ((ContainerType)rt).GetItemType();
				if (!(lt is TextType) || !(rt is TextType || rt is CharacterType))
					throw new SyntaxError("'contains' is only supported for textual values!");
			}
			return BooleanType.Instance; // can compare all objects
		}

		public override IValue interpret(Context context)
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
			case EqOp.CONTAINS:
				equal = lval.Contains (context, rval);
				break;
			case EqOp.NOT_CONTAINS:
				equal = !lval.Contains (context, rval);
				break;
			case EqOp.ROUGHLY:
				equal = lval.Roughly (context, (IValue)rval);
				break;
			}
			return Boolean.ValueOf(equal);
        }

		private bool isA(Context context, IValue lval, IValue rval) {
			if(rval is TypeValue) {
				IType actual = lval.GetIType();
				if (actual == NullType.Instance)
					return false;
				IType toCheck = ((TypeValue)rval).GetValue();
				return toCheck.isAssignableFrom(context, actual);
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
			test.printAssertionFailed(context, expected, actual);
			return false;
		}

		public void interpretQuery(Context context, IQueryBuilder builder)
		{
			IValue value = null;
			String name = readFieldName(left);
			if (name != null)
				value = right.interpret(context);
			else {
				name = readFieldName(right);
				if (name != null)
					value = left.interpret(context);
				else
					throw new SyntaxError("Unable to interpret predicate");
			}
			if (value is IInstance)
				value = ((IInstance)value).GetMember(context, "dbId", false);
			AttributeDeclaration decl = context.findAttribute(name);
			AttributeInfo info = decl == null ? null : decl.getAttributeInfo();
			Object data = value == null ? null : value.GetStorableData();
			MatchOp match = GetMatchOp();
			builder.Verify<Object>(info, match, data);
			if (oper == EqOp.NOT_EQUALS)
				builder.Not();
		}

		private MatchOp GetMatchOp()
		{
			switch (oper) {
				case EqOp.EQUALS:
				case EqOp.NOT_EQUALS:
					return MatchOp.EQUALS;
				case EqOp.ROUGHLY:
					return MatchOp.ROUGHLY;
				case EqOp.CONTAINS:
				case EqOp.NOT_CONTAINS:
					return MatchOp.CONTAINS;
				default:
					throw new NotSupportedException();
			}
		}

		private String readFieldName(IExpression exp)
		{
			if (exp is UnresolvedIdentifier
			|| exp is InstanceExpression
			|| exp is MemberSelector)
				return exp.ToString();
			else
				return null;
		}

	}

}
