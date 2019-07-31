using System;
using prompto.parser;
using System.Text;
using prompto.runtime;
using prompto.error;
using prompto.grammar;
using prompto.type;
using prompto.value;
using prompto.utils;
using prompto.argument;


namespace prompto.declaration
{

    public abstract class BaseMethodDeclaration : BaseDeclaration, IMethodDeclaration
    {

		CategoryDeclaration memberOf;
		IMethodDeclaration closureOf;
		protected ArgumentList arguments;
		protected IType returnType;

        public BaseMethodDeclaration(String name, ArgumentList arguments)
            : base(name)
        {
            this.arguments = arguments != null ? arguments : new ArgumentList();
            this.returnType = null;
        }

        public BaseMethodDeclaration(String name, ArgumentList arguments, IType returnType)
            : base(name)
        {
            this.arguments = arguments != null ? arguments : new ArgumentList();
            this.returnType = returnType;
        }

		public void setMemberOf(CategoryDeclaration declaration) {
			this.memberOf = declaration;
		}

		public CategoryDeclaration getMemberOf() {
			return memberOf;
		}

		public new IMethodDeclaration ClosureOf
		{
			set
			{
				this.closureOf = value;
			}
			get
			{
				return closureOf;
			}
		}


		public IType getReturnType()
        {
            return this.returnType;
        }

        public String getSignature(Dialect dialect)
        {
			StringBuilder sb = new StringBuilder(GetName());
            sb.Append('(');
            foreach (IArgument arg in arguments)
            {
                sb.Append(arg.getSignature(dialect));
                sb.Append(", ");
            }
            if (arguments.Count > 0)
                sb.Length = sb.Length - 2; // strip ", "
            sb.Append(')');
            return ToString();
        }

        override
        public String ToString()
        {
			return GetName() + ":(" + arguments.ToString() + ')';
        }

        public String getProto()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IArgument arg in arguments)
            {
                if (sb.Length > 0)
                    sb.Append('/');
                sb.Append(arg.getProto());
            }
            return sb.ToString();
        }

        public ArgumentList getArguments()
        {
            return arguments;
        }

        override
        public void register(Context context)
        {
            context.registerDeclaration(this);
        }

        public void registerArguments(Context context)
        {
            if (arguments != null)
                arguments.register(context);
        }

        override
        public IType GetIType(Context context)
        {
            try
            {
                return check(context);
            }
            catch (SyntaxError e)
            {
                throw new Exception(null, e);
            }
        }

        public bool isAssignableTo(Context context, ArgumentAssignmentList assignments, bool useInstance)
        {
            try
            {
                Context local = context.newLocalContext();
                registerArguments(local);
                ArgumentAssignmentList assignmentsList = new ArgumentAssignmentList(assignments);
                foreach (IArgument argument in arguments)
                {
                    ArgumentAssignment assignment = assignmentsList.find(argument.GetName());
					if (assignment == null)
					{
						if(argument.DefaultValue!=null)
							assignment = new ArgumentAssignment(argument, argument.DefaultValue);
					}
                    if (assignment == null) // missing argument
                        return false;
                    assignmentsList.Remove(assignment);
					if (!isAssignableTo(local, argument, assignment, useInstance))
                        return false;
                }
                return assignmentsList.Count == 0;
            }
            catch (SyntaxError )
            {
                return false;
            }
        }

		bool isAssignableTo(Context context, IArgument argument, ArgumentAssignment assignment, bool useInstance)
        {
			return computeSpecificity(context, argument, assignment, useInstance) != Specificity.INCOMPATIBLE;
        }

		public Specificity? computeSpecificity(Context context, IArgument argument, ArgumentAssignment assignment, bool useInstance)
        {
            try
            {
                IType required = argument.GetIType(context);
                IType actual = assignment.getExpression().check(context);
                // retrieve actual runtime type
				if (useInstance && actual is CategoryType)
                {
                    Object value = assignment.getExpression().interpret((Context)context.getCallingContext());
                    if (value is IInstance)
                        actual = ((IInstance)value).getType();
                }
                if (actual.Equals(required))
                    return Specificity.EXACT;
                if (required.isAssignableFrom(context, actual))
                    return Specificity.INHERITED;
				actual = assignment.resolve(context, this, useInstance).check(context);
                if (required.isAssignableFrom(context, actual))
                    return Specificity.RESOLVED;
            }
            catch (PromptoError )
            {
            }
            return Specificity.INCOMPATIBLE;
        }

		public virtual IType checkChild(Context context)
		{
			if(arguments!=null)
				arguments.check(context);
			return returnType;
		}

		public virtual IValue interpret(Context context)
        {
            throw new InternalError("Should never get there!");
        }

        public virtual bool isAbstract()
        {
            return false;
        }

		public virtual bool isTemplate()
		{
			return false;
		}

		public virtual bool isEligibleAsMain()
		{
			return false;
		}
    }
}


