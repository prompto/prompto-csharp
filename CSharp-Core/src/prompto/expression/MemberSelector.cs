using System.Collections.Generic;
using System;
using prompto.runtime;
using prompto.error;
using prompto.value;
using DateTime = prompto.value.DateTime;
using prompto.parser;
using prompto.grammar;
using prompto.type;
using prompto.utils;
using prompto.statement;

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
			IType type = check(writer.getContext());
			if (type is MethodType)
				writer.append("Method: ");
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
				parent.ToDialect(writer);
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


		override
		public IType check(Context context)
		{
			IType parentType = checkParent(context);
			return parentType.checkMember(context, name);
		}

		override
		public IValue interpret(Context context)
		{
			// resolve parent to keep clarity
			IExpression parent = resolveParent(context);
			// special case for singletons 
			IValue value = interpretSingleton(context, parent);
			if (value != null)
				return value;
			// special case for 'static' type members (like Enum.symbols, Type.name etc...)
			value = interpretTypeMember(context, parent);
			if (value != null)
				return value;
			// finally resolve instance member
			return interpretInstanceMember(context, parent);
		}

		private IValue interpretInstanceMember(Context context, IExpression parent)
		{
			IValue instance = parent.interpret(context);
			if (instance == null || instance == NullValue.Instance)
				throw new NullReferenceError();
			else
				return instance.GetMember(context, name, true);
		}

		private IValue interpretTypeMember(Context context, IExpression parent)
		{
			if (parent is TypeExpression)
				return ((TypeExpression)parent).getMember(context, name);
			else
				return null;
		}

		private IValue interpretSingleton(Context context, IExpression parent)
		{
			if (parent is TypeExpression)
			{
				IType type = ((TypeExpression)parent).getType();
				if (type is CategoryType && !(type is EnumeratedCategoryType))
				{
					ConcreteInstance instance = context.loadSingleton(context, (CategoryType)type);
					if (instance != null)
						return instance.GetMember(context, name, false);
				}
			}
			return null;
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
