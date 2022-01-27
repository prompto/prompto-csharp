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

		public override void ToDialect (CodeWriter writer)
		{
			if (parent == null)
				writer.append (name);
			else
				base.parentAndMemberToDialect (writer);
		}

		public ISet<IMethodDeclaration> getCandidates (Context context, bool checkInstance)
		{
			IMethodDeclaration decl = getMethodInstance(context);
			if (decl != null) {
				ISet<IMethodDeclaration> methods = new HashSet<IMethodDeclaration>();
				methods.Add(decl);
				return methods;
			} else if (parent == null)
				return getGlobalCandidates (context);
			else
				return getMemberCandidates (context, checkInstance);
		}


		private IMethodDeclaration getMethodInstance(Context context)
        {
			INamed named = context.getRegistered(name);
			if (named is INamedInstance)
			{
				IType type = named.GetIType(context);
				if (type != null)
				{
					type = type.Resolve(context);
					if (type is MethodType)
						return ((MethodType)type).Method;
				}
			}
			return null;

		}


		private ISet<IMethodDeclaration> getGlobalCandidates(Context context)
		{
			ISet<IMethodDeclaration> methods = new HashSet<IMethodDeclaration>();
    		// if called from a member method, could be a member method called without this/self
			InstanceContext instance = context.getClosestInstanceContext();
			if (instance != null)
			{
				IType type = instance.getInstanceType();
				ConcreteCategoryDeclaration cd = context.getRegisteredDeclaration<ConcreteCategoryDeclaration>(type.GetTypeName());
				if (cd != null)
				{
					MethodDeclarationMap members = cd.getMemberMethods(context, name);
					if (members != null)
						methods.UnionWith(members.Values);
				}
			}
			MethodDeclarationMap globals = context.getRegisteredDeclaration<MethodDeclarationMap>(name);
			if (globals != null)
				methods.UnionWith(globals.Values);
			return methods;
		}

		private ISet<IMethodDeclaration> getMemberCandidates (Context context, bool checkInstance)
		{
			IType parentType = checkParentType(context, checkInstance);
			return parentType.getMemberMethods(context, name);
		}

		private IType checkParentType(Context context, bool checkInstance)
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
