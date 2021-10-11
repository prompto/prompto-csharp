using System;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;


namespace prompto.csharp
{

    public class CSharpStatement
    {

        CSharpExpression expression;
        bool isReturn;

        public CSharpStatement(CSharpExpression expression, bool isReturn)
        {
            this.expression = expression;
            this.isReturn = isReturn;
        }

		public IType check(Context context, IType returnType)
		{
			IType type = expression.check(context);
			if (type == null)
				type = returnType;
			if (type is CSharpClassType)
				type = ((CSharpClassType)type).ConvertCSharpTypeToPromptoType(context, returnType);
			return isReturn ? type : VoidType.Instance;
		}

		public IValue interpret(Context context, IType returnType)
        {
            Object result = expression.interpret(context);
			if(result==null) 
				return isReturn ? NullValue.Instance : null;
			else {	
				IType type = expression.check(context);
				if (type is CSharpClassType)
					return ((CSharpClassType)type).ConvertCSharpValueToPromptoValue(context, result, returnType);
				else
					// TODO warning or exception?
					return VoidResult.Instance;
			}
        }

        public override String ToString()
        {
            return "" + (isReturn ? "return " : "") + expression.ToString() + ";";
        }

		public void ToDialect(CodeWriter writer) 
		{
			if(isReturn)
				writer.append("return ");
			expression.ToDialect(writer);
			writer.append(';');
		}


    }
}
