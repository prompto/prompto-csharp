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

        ArgumentAssignmentList assignments;
        EnumeratedCategoryType type;
		IInstance instance;

        public CategorySymbol(String name, ArgumentAssignmentList assignments)
            : base(name)
        {
            this.assignments = assignments;
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

        public void setAssignments(ArgumentAssignmentList assignments)
        {
            this.assignments = assignments;
        }

        public ArgumentAssignmentList getAssignments()
        {
            return assignments;
        }

        
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (assignments != null)
                sb.Append(assignments.ToString());
            if (sb.Length == 0)
				sb.Append(type.GetTypeName());
            return sb.ToString();
        }


		public override void ToDialect(CodeWriter writer) {
			writer.append(symbol);
			writer.append(" ");
			assignments.ToDialect(writer);
		}

        
        public override IType check(Context context)
        {
			EnumeratedCategoryDeclaration cd = context.getRegisteredDeclaration<EnumeratedCategoryDeclaration>(type.GetTypeName());
            if (cd == null)
				throw new SyntaxError("Unknown category " + type.GetTypeName());
            if (assignments != null)
            {
				context = context.newLocalContext ();
                foreach (ArgumentAssignment assignment in assignments)
                {
					if (!cd.hasAttribute(context, assignment.GetName()))
						throw new SyntaxError("\"" + assignment.GetName() + "\" is not an attribute of " + type.GetTypeName());
                    assignment.check(context);
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
				if (assignments != null)
				{
					context = context.newLocalContext();
					foreach (ArgumentAssignment assignment in assignments)
					{
						IValue val = assignment.getExpression().interpret(context);
						_instance.SetMember(context, assignment.GetName(), val);
					}
				}
				_instance.SetMember(context, "name", new Text(this.GetName()));
				_instance.setMutable(false);
				instance = _instance;
			}
			return instance;
        }


		public override IValue GetMember(Context context, String name, bool autoCreate) 
		{
			makeInstance(context);
			return instance.GetMember(context, name, autoCreate);
		}
	


    }
}
