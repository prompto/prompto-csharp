using prompto.parser;
using prompto.type;
using prompto.runtime;
using prompto.utils;
using prompto.value;
using prompto.declaration;
using prompto.error;

namespace prompto.statement
{

    public abstract class BaseStatement : Section, IStatement
    {

        public abstract IType check(Context context);
        public abstract IValue interpret(Context context);
        public abstract void ToDialect(CodeWriter writer);
        public virtual void ParentToDialect(CodeWriter writer)
        {
            ToDialect(writer);
        }
        public virtual AttributeDeclaration CheckAttribute(Context context)
        {
            throw new SyntaxError("Expected an attribute, got: " + this.ToString());
        }
        public virtual bool CanReturn { get { return false; } }
		public virtual bool IsSimple { get { return false; } }

    }

}
