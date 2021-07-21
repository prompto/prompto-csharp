using System.Collections.Generic;
using System;
using prompto.runtime;
using prompto.error;
using prompto.value;
using DateTime = prompto.value.DateTimeValue;
using prompto.parser;
using prompto.grammar;
using prompto.type;
using prompto.utils;
using prompto.statement;
using prompto.declaration;

namespace prompto.expression
{

	public class MemberSelector : SelectorExpression
	{

		protected String name;

		public MemberSelector(String name)
		{
			this.name = name;
		}

		public MemberSelector(IExpression parent, String name)
			: base(parent)
		{
			this.name = name;
		}

		public String getName()
		{
			return name;
		}


		public override void ToDialect(CodeWriter writer)
		{
			if (writer.getDialect() == Dialect.E)
				ToEDialect(writer);
			else
				ToOMDialect(writer);
		}

		void ToEDialect(CodeWriter writer)
		{
			try
			{
				IType type = check(writer.getContext());
				if (type is MethodType)
					writer.append("Method: ");
			} catch (SyntaxError ignored)
			{
				// gracefully skip exceptions
			}
			parentAndMemberToDialect(writer);
		}

		void ToOMDialect(CodeWriter writer)
		{
			parentAndMemberToDialect(writer);
		}

		protected void parentAndMemberToDialect(CodeWriter writer)
		{
			try
			{
				resolveParent(writer.getContext());
			}
			catch (SyntaxError)
			{
				// ignore
			}
			if (writer.getDialect() == Dialect.E)
				parentToEDialect(writer);
			else
				parentToOMDialect(writer);
			writer.append(".");
			writer.append(name);
		}

		void parentToEDialect(CodeWriter writer)
		{
			if (parent is UnresolvedCall)
			{
				writer.append('(');
				parent.ToDialect(writer);
				writer.append(')');
			}
			else
				parent.ParentToDialect(writer);
		}

		void parentToOMDialect(CodeWriter writer)
		{
			// if parent is: (method()), then supposedly this emanates from a translation from E dialect
			if (parent is ParenthesisExpression && ((ParenthesisExpression)parent).getExpression() is UnresolvedCall)
				((ParenthesisExpression)parent).getExpression().ToDialect(writer);
			else
				parent.ToDialect(writer);
		}

		public override String ToString()
		{
			return parent.ToString() + "." + name;
		}


		
		public override IType check(Context context)
		{
			IType parentType = checkParent(context);
			return parentType.checkMember(context, name);
		}

		
		public override IValue interpret(Context context)
		{
			// resolve parent to keep clarity
			IExpression parent = resolveParent(context);
			IValue instance = parent.interpret(context);
			if (instance == null || instance == NullValue.Instance)
				throw new NullReferenceError();
			else
				return instance.GetMemberValue(context, name, true);
		}

        public override IValue interpretReference(Context context)
        {
			// resolve parent to keep clarity
			IExpression parent = resolveParent(context);
			IValue instance = parent.interpret(context);
			if (instance == null || instance == NullValue.Instance)
				throw new NullReferenceError();
			else if (instance is IInstance) {
				CategoryDeclaration category = ((IInstance)instance).getDeclaration();
				MethodDeclarationMap methods = category.getMemberMethods(context, name);
				IMethodDeclaration method = methods.GetFirst(); // TODO check prototype
				return new ClosureValue(context.newInstanceContext((IInstance)instance, true), new MethodType(method));
			} else
				throw new SyntaxError("Should never get here!");
		}

		private IExpression resolveParent(Context context)
		{
			if (parent is UnresolvedIdentifier)
			{
				((UnresolvedIdentifier)parent).checkMember(context);
				return ((UnresolvedIdentifier)parent).getResolved();
			}
			else
				return parent;
		}
	}

}
