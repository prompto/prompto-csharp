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
			IValue root = parent.interpret(context);
			if(!root.IsMutable())
				throw new NotMutableError();
            IValue value = expression.interpret(context);
			root.SetMember(context, name, value);
        }

		public IValue interpret(Context context)
        {
			IValue root = parent.interpret(context);
            return root.GetMember(context, name);
        }

    }
}