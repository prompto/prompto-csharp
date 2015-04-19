using System;
using System.Collections.Generic;
using presto.runtime;
using presto.error;
using presto.parser;
using presto.declaration;
using presto.type;
using presto.value;
using presto.grammar;
using presto.utils;


namespace presto.expression
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

		public Dictionary<String, IMethodDeclaration>.ValueCollection getCandidates (Context context)
		{
			if (parent == null)
				return getGlobalCandidates (context);
			else
				return getCategoryCandidates (context);
		}

		private Dictionary<String,IMethodDeclaration>.ValueCollection getGlobalCandidates (Context context)
		{
			MethodDeclarationMap actual = context.getRegisteredDeclaration<MethodDeclarationMap> (name);
			if (actual == null)
				throw new SyntaxError ("Unknown method: \"" + name + "\"");
			return actual.Values;
		}

		private Dictionary<String, IMethodDeclaration>.ValueCollection getCategoryCandidates (Context context)
		{
			IType parentType = checkParent(context);
			if(!(parentType is CategoryType))
				throw new SyntaxError(parent.ToString() + " is not a category");
			ConcreteCategoryDeclaration cd = context.getRegisteredDeclaration<ConcreteCategoryDeclaration>(parentType.GetName());
			if(cd==null)
				throw new SyntaxError("Unknown category:" + parentType.GetName());
			return cd.findMemberMethods(context, name);
		}

		public Context newLocalContext (Context context)
		{
			if (parent == null)
				return context.newLocalContext ();
			else
				return newInstanceContext (context);
		}

		public Context newLocalCheckContext (Context context)
		{
			if (parent == null)
				return context.newLocalContext ();
			else
				return newInstanceCheckContext (context);
		}

		private Context newInstanceCheckContext (Context context)
		{
			IType type = parent.check (context);
			if (!(type is CategoryType))
				throw new SyntaxError ("Not an instance !");
			context = context.newInstanceContext (type);
			return context.newChildContext ();
		}

		private Context newInstanceContext (Context context)
		{
			Object value = parent.interpret(context);
			if(value==null || value==NullValue.Instance)
				throw new NullReferenceError();
			if(value is TypeValue && ((TypeValue)value).GetValue() is CategoryType)
				value = context.loadSingleton((CategoryType)((TypeValue)value).GetValue());
			if(!(value is ConcreteInstance))
				throw new InvalidDataError("Not an instance !");
			context = context.newInstanceContext((ConcreteInstance)value);
			return context.newChildContext();
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
