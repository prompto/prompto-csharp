using System;
using presto.runtime;
using presto.error;
using presto.parser;
using presto.value;
using presto.expression;
using presto.utils;


namespace presto.grammar {

    public class MemberInstance : IAssignableSelector
    {

        IAssignableInstance parent;
        String name;

        public MemberInstance(IAssignableInstance parent, String name)
        {
            this.parent = parent;
            this.name = name;
        }

        public MemberInstance(String name)
        {
            this.name = name;
        }

        public void SetParent(IAssignableInstance parent)
        {
            this.parent = parent;
        }

        public String getName()
        {
            return name;
        }

		public void ToDialect(CodeWriter writer) {
			parent.ToDialect(writer);
			writer.append(".");
			writer.append(name);
		}

        public void checkAssignValue(Context context, IExpression expression)
        {
            parent.checkAssignMember(context, name);
            expression.check(context);
        }

        public void checkAssignMember(Context context, String memberName)
        {
            parent.checkAssignMember(context, name);
        }

        public void checkAssignElement(Context context)
        {
            // TODO Auto-generated method stub

        }

        public void assign(Context context, IExpression expression)
        {
            Object doc = parent.interpret(context);
            if (doc is Document)
            {
                IValue value = (IValue)expression.interpret(context);
                ((Document)doc).SetMember(name, value);
            }
            else
                throw new InvalidDataError("Expecting a document, got:" + doc.GetType().Name);
        }

        public Object interpret(Context context)
        {
            Object doc = parent.interpret(context);
            if (doc is Document)
                return ((Document)doc).GetMember(context, name);
            else
                throw new InvalidDataError("Expecting a document, got:" + doc.GetType().Name);
        }

    }
}
