using System;
using presto.parser;
using System.Text;
using presto.runtime;
using presto.error;
using presto.grammar;
using presto.type;
using presto.value;
using presto.utils;


namespace presto.declaration
{

    public abstract class BaseMethodDeclaration : BaseDeclaration, IMethodDeclaration
    {

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
            this.returnType = returnType != null ? returnType : VoidType.Instance;
        }

        public IType getReturnType()
        {
            return this.returnType;
        }

        public String getSignature(Dialect dialect)
        {
            StringBuilder sb = new StringBuilder(getName());
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
            return getName() + ":(" + arguments.ToString() + ')';
        }

        public String getProto(Context context)
        {
            StringBuilder sb = new StringBuilder();
            foreach (IArgument arg in arguments)
            {
                if (sb.Length > 0)
                    sb.Append('/');
                sb.Append(arg.getProto(context));
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
        public IType GetType(Context context)
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

        public bool isAssignableTo(Context context, ArgumentAssignmentList assignments, bool checkInstance)
        {
            try
            {
                Context local = context.newLocalContext();
                registerArguments(local);
                ArgumentAssignmentList assignmentsList = new ArgumentAssignmentList(assignments);
                foreach (IArgument argument in arguments)
                {
                    ArgumentAssignment assignment = assignmentsList.find(argument.getName());
					if (assignment == null)
					{
						if(argument.DefaultValue!=null)
							assignment = new ArgumentAssignment(argument, argument.DefaultValue);
					}
                    if (assignment == null) // missing argument
                        return false;
                    assignmentsList.Remove(assignment);
                    if (!isAssignableTo(local, argument, assignment, checkInstance))
                        return false;
                }
                return assignmentsList.Count == 0;
            }
            catch (SyntaxError )
            {
                return false;
            }
        }

        bool isAssignableTo(Context context, IArgument argument, ArgumentAssignment assignment, bool checkInstance)
        {
            return computeSpecificity(context, argument, assignment, checkInstance) != Specificity.INCOMPATIBLE;
        }

        public Specificity? computeSpecificity(Context context, IArgument argument, ArgumentAssignment assignment, bool checkIntance)
        {
            try
            {
                IType required = argument.GetType(context);
                IType actual = assignment.getExpression().check(context);
                // retrieve actual runtime type
                if (checkIntance && actual is CategoryType)
                {
                    Object value = assignment.getExpression().interpret((Context)context.getCallingContext());
                    if (value is IInstance)
                        actual = ((IInstance)value).getType();
                }
                if (actual.Equals(required))
                    return Specificity.EXACT;
                if (actual.isAssignableTo(context, required))
                    return Specificity.INHERITED;
                actual = assignment.resolve(context, this, checkIntance).check(context);
                if (actual.isAssignableTo(context, required))
                    return Specificity.RESOLVED;
            }
            catch (PrestoError )
            {
            }
            return Specificity.INCOMPATIBLE;
        }

		public virtual IValue interpret(Context context)
        {
            throw new InternalError("Should never get there!");
        }

        public virtual bool isEligibleAsMain()
        {
            return false;
        }
    }
}


