using System;
using System.Collections.Generic;
using prompto.runtime;
using prompto.error;
using prompto.parser;
using prompto.declaration;
using prompto.type;
using prompto.value;
using prompto.grammar;
using prompto.utils;


namespace prompto.expression
{

	public class MethodSelector : MemberSelector
	{

		public MethodSelector (String name)
			: base (name)
		{
		}


		public MethodSelector (IExpression parent, String name)
			: base (parent, name)
		{
		}

		public override string ToString ()
		{
			return parent==null ? name : base.ToString();
		}

		public override void ToDialect(CodeWriter writer)
		{
			ToDialect(writer, true);
		}


        public void ToDialect(CodeWriter writer, bool asRef)
		{
			if(asRef && writer.getDialect() == Dialect.E)
                writer.append("Method: ");
            if (parent == null)
				writer.append (name);
			else
				base.parentAndMemberToDialect (writer);
		}

	
		public IType checkParentType(Context context, bool checkInstance)
		{
			if (checkInstance)
				return interpretParentInstance(context);
			else
				return checkParent(context);
		}

		private IType interpretParentInstance(Context context)
		{
			IValue value = parent.interpret(context);
			if (value == null || value == NullValue.Instance)
				throw new NullReferenceError();
			IType type = value.GetIType();
			if (parent is SuperExpression)
				return ((CategoryType)type).getSuperType(context);
			else
				return type;
		}

		public Context newLocalContext (Context context, IMethodDeclaration declaration)
		{
			if (parent != null)
				return newInstanceContext (context);
			else if(declaration.getMemberOf()!=null)
				return newLocalInstanceContext(context, declaration);
			else
				return context.newLocalContext ();
		}

		public Context newLocalCheckContext (Context context, IMethodDeclaration declaration)
		{
			if (parent != null)
				return newInstanceCheckContext (context);
			else if(declaration.getMemberOf()!=null)
				return newLocalInstanceContext(context, declaration);
			else
				return context.newLocalContext ();
		}

		private Context newInstanceCheckContext (Context context)
		{
			IType type = parent.check (context);
			// if calling singleton method, parent is the singleton type 
			if (type is TypeType) { 
				IDeclaration decl = context.getRegisteredDeclaration<IDeclaration>(((TypeType) type).GetIType().GetTypeName());
				if(decl is SingletonCategoryDeclaration)
					type = decl.GetIType(context);
			}
			if (type is CategoryType)
			{
				context = context.newInstanceContext((CategoryType)type, false);
				return context.newChildContext();
			}
			else
				return context.newChildContext();
		}

		private Context newLocalInstanceContext(Context context, IMethodDeclaration declaration) {
			InstanceContext instance = context.getClosestInstanceContext();
			if (instance != null)
			{
				CategoryType required = (CategoryType)declaration.getMemberOf().GetIType(context);
				IType actual = instance.getInstanceType();
				if (!required.isAssignableFrom(context, actual))
					instance = null;
			}
			if (instance == null)
			{
				CategoryType declaring = (CategoryType)declaration.getMemberOf().GetIType(context);
				instance = context.newInstanceContext(declaring, false);
			}
			return instance.newChildContext();
		}

		private Context newInstanceContext (Context context)
		{
			IValue value = parent.interpret(context);
			if(value==null || value==NullValue.Instance)
				throw new NullReferenceError();
            if (value is TypeValue) {
                IType type = ((TypeValue)value).GetValue();
                if (type is CategoryType) {
                    IDeclaration decl = ((CategoryType)type).getDeclaration(context);
                    if (decl is SingletonCategoryDeclaration) {
                        value = context.loadSingleton((CategoryType)type);
                    }
                }
            }
			if (value is CategorySymbol)
				value = ((CategorySymbol)value).interpret(context);
            if (value is TypeValue)
            {
                return context.newChildContext();
            }
            else if (value is IInstance)
			{
				context = context.newInstanceContext((IInstance)value, false);
				return context.newChildContext();
			} 
			else
			{
				context = context.newBuiltInContext(value);
				return context.newChildContext();
			}
		}

		public IExpression toInstanceExpression ()
		{
			if (parent == null)
				return new UnresolvedIdentifier (name, Dialect.O); // not a method call
			else
				return new MemberSelector (parent, name);
		}

	}

}
