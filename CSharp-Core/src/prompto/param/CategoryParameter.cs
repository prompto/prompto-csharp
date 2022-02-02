using System;
using prompto.parser;
using prompto.runtime;
using prompto.utils;
using prompto.error;
using prompto.type;
using prompto.value;
using prompto.expression;
using prompto.declaration;
using System.Linq;

namespace prompto.param
{

    public class CategoryParameter : BaseParameter, ITypedParameter
    {

        protected IType type;
        protected IType resolved;

        public CategoryParameter(IType type, string name)
            : base(name)
        {
            this.type = type;
            this.resolved = null;
        }

		public CategoryParameter(IType type, string name, IExpression defaultValue)
			: base(name)
		{
			this.type = type;
			DefaultValue = defaultValue;
		}


		public IType getType()
        {
            return type;
        }

        
        public override string getSignature(Dialect dialect)
        {
            return getProto();
        }

        
        public override string getProto()
        {
            return type.GetTypeName();
        }


        public override bool setMutable(bool mutable)
        {
            if (type is CategoryType)
                ((CategoryType)type).Mutable = mutable;
            return base.setMutable(mutable);
        }

        public override IValue checkValue(Context context, IExpression expression) 
        {
            bool isArrow = expression is ContextualExpression && ((ContextualExpression)expression).Expression is ArrowExpression;
            if (isArrow)
                return checkArrowValue(context, (ContextualExpression)expression);
            else
                return checkSimpleValue(context, expression);
	    }

        private IValue checkSimpleValue(Context context, IExpression expression)
        {
            resolve(context);
            if (resolved is MethodType)
			    return expression.interpretReference(context);
            else
                return base.checkValue(context, expression);
        }

        private IValue checkArrowValue(Context context, ContextualExpression expression)
        {
            IMethodDeclaration decl = getAbstractMethodDeclaration(context);
            return new ArrowValue(decl, expression.Calling, (ArrowExpression)expression.Expression); // TODO check
        }

        private IMethodDeclaration getAbstractMethodDeclaration(Context context)
        {
            MethodDeclarationMap methods = context.getRegisteredDeclaration<MethodDeclarationMap>(type.GetTypeName());
		    if(methods!=null)
                return methods.Values.FirstOrDefault(decl => decl.isAbstract());
            else
			    return null;
	    }

        public override void ToDialect(CodeWriter writer)
        {
			if(this.mutable)
				writer.append("mutable ");
			switch(writer.getDialect()) {
			case Dialect.E:
				ToEDialect(writer);
				break;
			case Dialect.O:
				ToODialect(writer);
				break;
			case Dialect.M:
				ToMDialect(writer);
				break;
			}
			if(DefaultValue!=null) {
				writer.append(" = ");
				DefaultValue.ToDialect(writer);
			}
		}

		protected virtual void ToEDialect(CodeWriter writer) {
			bool anonymous = "any"==type.GetTypeName();
			type.ToDialect(writer, true);
			if(anonymous) {
				writer.append(' ');
				writer.append(name);
			}
			if(!anonymous) {
				writer.append(' ');
				writer.append(name);
			}
		}

		protected virtual void ToODialect(CodeWriter writer) {
			type.ToDialect(writer, true);
			writer.append(' ');
			writer.append(name);
		}

		protected virtual void ToMDialect(CodeWriter writer) {
			writer.append(name);
			writer.append(':');
			type.ToDialect(writer, true);
		}

        
        override public bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is CategoryParameter))
                return false;
            CategoryParameter other = (CategoryParameter)obj;
            return ObjectUtils.AreEqual(this.getType(), other.getType())
				&& ObjectUtils.AreEqual(this.GetName(), other.GetName());
        }

        
        override public void register(Context context)
        {
			Context actual = context.contextForValue(name);
            if (actual == context)
                throw new SyntaxError("Duplicate argument: \"" + name + "\"");
            resolve(context);
            if(resolved == type)
                context.registerValue(this);
            else
            {
                CategoryParameter param = new CategoryParameter(resolved, name);
                param.setMutable(mutable);
                context.registerValue(param);
            }
			if (DefaultValue != null) {
				IValue value = DefaultValue.interpret (context);
				context.setValue (name, value);
			}
        }


        public override void check(Context context)
        {
            resolve(context);
            resolved.checkExists(context);
        }


        private void resolve(Context context)
        {
            if (resolved == null)
                resolved = type.Resolve(context).AsMutable(context, mutable);
        }

        public override IType GetIType(Context context)
        {
            return type;
        }

    }
}
