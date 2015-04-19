using presto.error;
using presto.grammar;
using System.Collections.Generic;
using System.Threading;
using System;
using presto.utils;
using System.Text;
using presto.value;
using presto.runtime;
using presto.declaration;
using presto.expression;
using presto.type;

namespace presto.value
{

	public class ConcreteInstance : BaseValue, IInstance, IMultiplyable
    {

        ConcreteCategoryDeclaration declaration;
        Dictionary<String, IValue> values = new Dictionary<String, IValue>();
		bool mutable = false;

		public ConcreteInstance(ConcreteCategoryDeclaration declaration)
			: base(new CategoryType(declaration.GetName()))
		     {
            this.declaration = declaration;
        }

		public bool setMutable(bool set)
		{
			bool result = mutable;
			mutable = set;
			return result;
		}

		public override bool IsMutable()
		{
			return mutable;
		}

			
        public ConcreteCategoryDeclaration getDeclaration()
        {
            return declaration;
        }

        public CategoryType getType()
        {
			return (CategoryType)type;
        }

        public ICollection<String> getMemberNames()
        {
            return values.Keys;
        }

        static Dictionary<String, Context> Factory()
        {
            return new Dictionary<String, Context>();
        }

        // don't call getters from getters, so register them
        ThreadLocal<Dictionary<String, Context>> activeGetters = new ThreadLocal<Dictionary<String, Context>>(Factory);

        
		public override IValue GetMember(Context context, String attrName)
        {
            Context stacked;
            activeGetters.Value.TryGetValue(attrName, out stacked);
            try
            {
                return GetMember(context, attrName, stacked == null);
            }
            finally
            {
                if (stacked == context)
                    activeGetters.Value[attrName] = null;
            }
        }

        protected IValue GetMember(Context context, String attrName, bool allowGetter)
        {
            GetterMethodDeclaration getter = allowGetter ? declaration.findGetter(context, attrName) : null;
            if (getter != null)
            {
                activeGetters.Value[attrName] = context;
                context = context.newInstanceContext(this);
                return new ContextualExpression(context, getter);
            }
            else
            {
                IValue value = null;
                values.TryGetValue(attrName, out value);
                return value;
            }
        }

        // don't call setters from setters, so register them
        ThreadLocal<Dictionary<String, Context>> activeSetters = new ThreadLocal<Dictionary<String, Context>>(Factory);

        public override void SetMember(Context context, String attrName, IValue value)
        {
			if(!mutable)
				throw new NotMutableError();
			Context stacked;
            activeSetters.Value.TryGetValue(attrName, out stacked);
            try
            {
                set(context, attrName, value, stacked == null);
            }
            finally
            {
                if (stacked == context)
                    activeSetters.Value[attrName] = null;
            }
        }

        public void set(Context context, String attrName, IValue value, bool allowSetter)
        {
            AttributeDeclaration decl = context.getRegisteredDeclaration<AttributeDeclaration>(attrName);
            SetterMethodDeclaration setter = allowSetter ? declaration.findSetter(context, attrName) : null;
            if (setter != null)
            {
                activeSetters.Value[attrName] = context;
                // use attribute name as parameter name for incoming value
                context = context.newInstanceContext(this);
				context.registerValue(new Variable(attrName, decl.getType()));
				context.setValue(attrName, value);
                value = setter.interpret(context);
            }
			value = autocast(decl, value);
            values[attrName] = value;
        }

		private IValue autocast(AttributeDeclaration decl, IValue value) {
			if(value!=null && value is presto.value.Integer && decl.getType()==DecimalType.Instance)
				value = new Decimal(((presto.value.Integer)value).DecimalValue);
			return value;
		}

		override
        public bool Equals(Object obj)
        {
            if (!(obj is ConcreteInstance))
                return false;
            return Utils.EqualDictionaries(this.values, ((ConcreteInstance)obj).values);
        }

        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (KeyValuePair<String, IValue> kvp in values)
            {
                sb.Append(kvp.Key);
                sb.Append(":");
                sb.Append(kvp.Value);
                sb.Append(", ");
            }
            sb.Length = sb.Length - 2;
            sb.Append("}");
            return sb.ToString();
        }


		public override IValue Multiply(Context context, IValue value) {
			try {
				return interpretOperator(context, value, Operator.MULTIPLY);
			} catch(SyntaxError ) {
				return base.Multiply(context, value);
			}
		}

		
		public override IValue Divide(Context context, IValue value) {
			try {
				return interpretOperator(context, value, Operator.DIVIDE);
			} catch(SyntaxError ) {
				return base.Divide(context, value);
			}
		}

		
		public override IValue IntDivide(Context context, IValue value) {
			try {
				return interpretOperator(context, value, Operator.IDIVIDE);
			} catch(SyntaxError ) {
				return base.IntDivide(context, value);
			}
		}

		
		public override IValue Modulo(Context context, IValue value) {
			try {
				return interpretOperator(context, value, Operator.MODULO);
			} catch(SyntaxError ) {
				return base.Modulo(context, value);
			}
		}

		
		public override IValue Add(Context context, IValue value) {
			try {
				return interpretOperator(context, value, Operator.PLUS);
			} catch(SyntaxError ) {
				return base.Add(context, value);
			}
		}

			
		public override IValue Subtract(Context context, IValue value) {
			try {
				return interpretOperator(context, value, Operator.MINUS);
			} catch(SyntaxError ) {
				return base.Subtract(context, value);
			}
		}


		private IValue interpretOperator(Context context, IValue value, Operator oper) {
			IMethodDeclaration decl = declaration.findOperator(context, oper, value.GetType(context));
			context = context.newInstanceContext(this);
			Context local = context.newChildContext();
			decl.registerArguments(local);
			IArgument arg = decl.getArguments()[0];
			local.setValue(arg.GetName(), value);
			return decl.interpret(local);
		}

    }

}
