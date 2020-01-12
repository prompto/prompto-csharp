using prompto.error;
using prompto.runtime;
using System.Text;
using System;
using prompto.value;
using prompto.type;
using prompto.expression;
using prompto.declaration;
using prompto.utils;
using prompto.grammar;

namespace prompto.expression
{

    public class CategorySymbol : Symbol, IExpression
    {

        ArgumentList arguments;
        EnumeratedCategoryType type;
		IInstance instance;

        public CategorySymbol(String name, ArgumentList arguments)
            : base(name)
        {
            this.arguments = arguments;
        }

		public override void SetIType(IType type)
        {
			this.type = (EnumeratedCategoryType)type;
        }

        
		public override IType GetIType()
		{
			return type;
		}

		public override IType GetIType(Context context)
        {
            return type;
        }

        public void setArguments(ArgumentList arguments)
        {
            this.arguments = arguments;
        }

        public ArgumentList getArguments()
        {
            return arguments;
        }

        
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (arguments != null)
                sb.Append(arguments.ToString());
            if (sb.Length == 0)
				sb.Append(type.GetTypeName());
            return sb.ToString();
        }


		public override void ToDialect(CodeWriter writer) {
			writer.append(symbol);
			writer.append(" ");
			arguments.ToDialect(writer);
		}

        
        public override IType check(Context context)
        {
			EnumeratedCategoryDeclaration cd = context.getRegisteredDeclaration<EnumeratedCategoryDeclaration>(type.GetTypeName());
            if (cd == null)
				throw new SyntaxError("Unknown category " + type.GetTypeName());
            if (arguments != null)
            {
				context = context.newLocalContext ();
                foreach (Argument argument in arguments)
                {
					if (!cd.hasAttribute(context, argument.GetName()))
						throw new SyntaxError("\"" + argument.GetName() + "\" is not an attribute of " + type.GetTypeName());
                    argument.check(context);
                }
            }
            return type;
        }


		public override IValue interpret(Context context)
		{
			return makeInstance(context);
		}


		private IInstance makeInstance(Context context)
		{
			if (instance == null) 
			{
				IInstance _instance = type.newInstance(context);
				_instance.setMutable(true);
				if (arguments != null)
				{
					context = context.newLocalContext();
					foreach (Argument argument in arguments)
					{
						IValue val = argument.getExpression().interpret(context);
						_instance.SetMemberValue(context, argument.GetName(), val);
					}
				}
				_instance.SetMemberValue(context, "name", new Text(this.GetName()));
				_instance.setMutable(false);
				instance = _instance;
			}
			return instance;
        }


		public override IValue GetMemberValue(Context context, String name, bool autoCreate) 
		{
			makeInstance(context);
			return instance.GetMemberValue(context, name, autoCreate);
		}
	


    }
}
