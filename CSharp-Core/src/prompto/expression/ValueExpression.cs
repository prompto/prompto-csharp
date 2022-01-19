using System;
using prompto.type;
using prompto.runtime;
using prompto.value;
using prompto.utils;
using prompto.declaration;
using prompto.error;

namespace prompto.expression
{

	public class ValueExpression : BaseValue, IExpression
    {

		IValue value;

		public ValueExpression(IType type, IValue value)
			: base(type)
        {
            this.value = value;
        }

        public IType check(Context context)
        {
            return type;
        }

        public virtual IType checkReference(Context context)
        {
            return check(context);
        }

        public AttributeDeclaration CheckAttribute(Context context)
        {
            throw new SyntaxError("Expected an attribute, found: " + this.ToString());
        }

        public IValue interpret(Context context)
        {
            return value;
        }

        public virtual IValue interpretReference(Context context)
        {
            return interpret(context);
        }

        public override String ToString()
        {
            return type.ToString(value);
        }

        public void ToDialect(CodeWriter writer)
        {
			writer.append(value.ToString()); // value has no dialect
        }

        public void ParentToDialect(CodeWriter writer)
        {
            ToDialect(writer);
        }
    }

}
