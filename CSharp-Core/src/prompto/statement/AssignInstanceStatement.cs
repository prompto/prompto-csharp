using prompto.runtime;
using prompto.type;
using prompto.expression;
using prompto.utils;
using prompto.value;
using prompto.instance;

namespace prompto.statement
{

    public class AssignInstanceStatement : SimpleStatement
    {

        IAssignableInstance instance;
        IExpression expression;

        public AssignInstanceStatement(IAssignableInstance instance, IExpression expression)
        {
            this.instance = instance;
            this.expression = expression;
        }

        public override string ToString()
        {
            return instance.ToString() + " = " + expression.ToString();
        }

        public override void ToDialect(CodeWriter writer)
        {
            instance.ToDialect(writer, expression);
			writer.append(" = ");
			expression.ToDialect(writer);
        }

        public IExpression getExpression()
        {
            return expression;
        }

        
        public override IType check(Context context)
        {
			IType valueType = expression.check (context);
			instance.checkAssignValue(context, valueType);
			// Code expressions need to be interpreted as part of full check
			if (valueType == CodeType.Instance)
				instance.assign(context, expression);
			return VoidType.Instance;
        }

        
        public override IValue interpret(Context context)
        {
            instance.assign(context, expression);
            return null;
        }
    }

}
