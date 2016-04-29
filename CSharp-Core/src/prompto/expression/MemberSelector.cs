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

namespace prompto.expression
{

	public class MemberSelector : SelectorExpression
	{

		protected String name;

		public MemberSelector (String name)
		{
			this.name = name;
		}

		public MemberSelector (IExpression parent, String name)
			: base (parent)
		{
			this.name = name;
		}

		public String getName ()
		{
			return name;
		}


		public override void ToDialect (CodeWriter writer)
		{
			try {
				resolveParent(writer.getContext());
			} catch(SyntaxError ) {
				// ignore
			}
			parent.ToDialect (writer);
			writer.append (".");
			writer.append (name);
		}

		public override String ToString() {
			return parent.ToString() + "." + name;
		}


		override
        public IType check (Context context)
		{
			IType parentType = checkParent(context);
			return parentType.checkMember (context, name);
		}

		override
		public IValue interpret (Context context)
		{       // resolve parent to keep clarity
			IExpression parent = resolveParent(context);
			// special case for Symbol which evaluates as value
			IValue value = interpretSymbol(context, parent);
			if(value!=null)
				return value;
			// special case for singletons 
			value = interpretSingleton(context, parent);
			if(value!=null)
				return value;
			// special case for 'static' type members (like Enum.symbols, Type.name etc...)
			value = interpretTypeMember(context, parent);
			if(value!=null)
				return value;
			// finally resolve instance member
			return interpretInstanceMember(context, parent);
		}

		private IValue interpretInstanceMember(Context context, IExpression parent) {
			IValue instance = parent.interpret(context);
			if (instance == null || instance == NullValue.Instance)
				throw new NullReferenceError();
			else
				return instance.GetMember(context, name, true);
		}

		private IValue interpretTypeMember(Context context, IExpression parent) {
			if(parent is TypeExpression)
				return ((TypeExpression)parent).getMember(context, name);
			else
				return null;
		}

		private IValue interpretSingleton(Context context, IExpression parent) {
			if(parent is TypeExpression && ((TypeExpression)parent).getType() is CategoryType) {
				ConcreteInstance instance = context.loadSingleton((CategoryType)((TypeExpression)parent).getType());
				if(instance!=null)
					return instance.GetMember(context, name, false); 
			}
			return null;
		}

		private IValue interpretSymbol(Context context, IExpression parent) {
			if (parent is SymbolExpression)
			{
				if ("name".Equals(name))
					return new Text(((SymbolExpression)parent).getName());
				else if("value".Equals(name))
					return parent.interpret(context);
			}
			return null;
		}

		private IExpression resolveParent(Context context) {
			if(parent is UnresolvedIdentifier) {
				((UnresolvedIdentifier) parent).checkMember(context);
				return ((UnresolvedIdentifier) parent).getResolved();
			} else
				return parent;
		}
	}

}
