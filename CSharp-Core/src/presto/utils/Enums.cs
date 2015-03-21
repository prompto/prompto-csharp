using System;
using presto.grammar;

namespace presto.utils
{
	public abstract class Enums
	{
		public static String OperatorToString(Operator oper)
		{
			switch (oper) {
			case Operator.PLUS:
				return "+";
			case Operator.MINUS:
				return "-";
			case Operator.MULTIPLY:
				return "*";
			case Operator.DIVIDE:
				return "/";
			case Operator.IDIVIDE:
				return "\\";
			case Operator.MODULO:
				return "%";
			default:
				throw new Exception ("Unknown operator:" + oper.ToString ());
			}
		}
	}
}

