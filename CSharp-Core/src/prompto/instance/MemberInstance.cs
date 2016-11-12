using System;
using prompto.runtime;
using prompto.error;
using prompto.parser;
using prompto.value;
using prompto.expression;
using prompto.utils;
using prompto.type;


namespace prompto.instance {

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

		public void ToDialect(CodeWriter writer, IExpression expression) {
			parent.ToDialect(writer, null);
			writer.append(".");
			writer.append(name);
		}

		public IType checkAssignValue(Context context, IType valueType)
        {
			IType type = parent.checkAssignMember(context, name, valueType);
			type.checkAssignableFrom(context, valueType);
			return type;
		}

		public IType checkAssignMember(Context context, String memberName, IType valueType)
        {
			return parent.checkAssignMember(context, name, valueType);
        }

		public IType checkAssignItem(Context context, IType itemType, IType valueType)
        {
			return AnyType.Instance; // TODO
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
            return root.GetMember(context, name, true);
        }

    }
}
