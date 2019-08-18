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
			INamed named = context.getRegistered(name);
			if (named is Variable && named.GetIType(context) is MethodType)
			{
				ISet<IMethodDeclaration> result = new HashSet<IMethodDeclaration>();
				result.Add(((MethodType)named.GetIType(context)).Method);
				return result;
			}
			else if(parent == null)
				return getGlobalCandidates (context);
			else
				return getMemberCandidates (context, checkInstance);
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
				return checkParentInstance(context);
			else
				return checkParent(context);
		}

		private IType checkParentInstance(Context context)
		{
			string name = null;
			if (parent is UnresolvedIdentifier)
				name = ((UnresolvedIdentifier)parent).getName();
			else if(parent is InstanceExpression)
				name = ((InstanceExpression)parent).getName();
			if(name!=null) {
				// don't get Singleton values
				if (char.IsLower(name[0]))
				{
					IValue value = context.getValue(name);
					if (value != null && value != NullValue.Instance)
						return value.GetIType();
				}
			}
			// TODO check result instance
			return checkParent(context);
		}

		public Context newLocalContext (Context context, IMethodDeclaration declaration)
		{
			if (parent != null)
				return newInstanceContext (context);
			else if(declaration.getMemberOf()!=null)
				return newLocalInstanceContext(context);
			else
				return context.newLocalContext ();
		}

		public Context newLocalCheckContext (Context context, IMethodDeclaration declaration)
		{
			if (parent != null)
				return newInstanceCheckContext (context);
			else if(declaration.getMemberOf()!=null)
				return newLocalInstanceContext(context);
			else
				return context.newLocalContext ();
		}

		private Context newInstanceCheckContext (Context context)
		{
			IType type = parent.check (context);
			if (type is CategoryType)
			{
				context = context.newInstanceContext((CategoryType)type, false);
				return context.newChildContext();
			}
			else
				return context.newChildContext();
		}

		private Context newLocalInstanceContext(Context context) {
			Context parent = context.getParentContext();
			if(!(parent is InstanceContext))
				throw new SyntaxError("Not in instance context !");
			context = context.newLocalContext();
			context.setParentContext(parent); // make local context child of the existing instance
			return context;
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
            if (value is TypeValue)
            {
                return context.newChildContext();
            }
            else if (value is ConcreteInstance)
			{
				context = context.newInstanceContext((ConcreteInstance)value, false);
				return context.newChildContext();
			} 
			else if(value is NativeInstance) 
			{
				context = context.newInstanceContext((NativeInstance)value, false);
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
