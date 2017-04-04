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
				base.ToDialect (writer);
		}

		public ICollection<IMethodDeclaration> getCandidates (Context context, bool checkInstance)
		{
			if (parent == null)
				return getGlobalCandidates (context);
			else
				return getMemberCandidates (context, checkInstance);
		}

		private ICollection<IMethodDeclaration> getGlobalCandidates(Context context)
		{
			List<IMethodDeclaration> methods = new List<IMethodDeclaration>();
			// if called from a member method, could be a member method called without this/self
			if (context.getParentContext() is InstanceContext)
			{
				IType type = ((InstanceContext)context.getParentContext()).getInstanceType();
				ConcreteCategoryDeclaration cd = context.getRegisteredDeclaration<ConcreteCategoryDeclaration>(type.GetTypeName());
				if (cd != null)
				{
					MethodDeclarationMap members = cd.getMemberMethods(context, name);
					if (members != null)
						methods.AddRange(members.Values);
				}
			}
			MethodDeclarationMap globals = context.getRegisteredDeclaration<MethodDeclarationMap>(name);
			if (globals != null)
				methods.AddRange(globals.Values);
			return methods;
		}

		private ICollection<IMethodDeclaration> getMemberCandidates (Context context, bool checkInstance)
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
				context = context.newInstanceContext((CategoryType)type);
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
			IType type = value is TypeValue ? ((TypeValue)value).GetValue() : null;
			if(type is CategoryType)
				value = context.loadSingleton(context, (CategoryType)type);
			if (value is ConcreteInstance)
			{
				context = context.newInstanceContext((ConcreteInstance)value);
				return context.newChildContext();
			} 
			else if(value is NativeInstance) 
			{
				context = context.newInstanceContext((NativeInstance)value);
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
				return new UnresolvedIdentifier (name);
			else
				return new MemberSelector (parent, name);
		}

	}

}
