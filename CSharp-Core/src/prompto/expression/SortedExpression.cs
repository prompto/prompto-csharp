using System;
using prompto.runtime;
using prompto.error;
using prompto.parser;
using prompto.type;
using prompto.value;
using prompto.utils;
using prompto.grammar;


namespace prompto.expression
{

	public class SortedExpression : IExpression
	{

		IExpression source;
		bool descending;
		IExpression key;

		public SortedExpression(IExpression source, bool descending)
		{
			this.source = source;
			this.descending = descending;
		}

		public SortedExpression(IExpression source, bool descending, IExpression key)
		{
			this.source = source;
			this.descending = descending;
			this.key = key;
		}

		public void ToDialect(CodeWriter writer)
		{
			switch (writer.getDialect())
			{
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
		}

		private void ToEDialect(CodeWriter writer)
		{
			writer.append("sorted ");
			if (descending)
				writer.append("descending ");
			source.ToDialect(writer);
			if (key != null)
			{
				writer = contextualizeWriter(writer);
				writer.append(" with ");
				IExpression keyExp = key;
				if (keyExp is UnresolvedIdentifier) try
					{
						keyExp = ((UnresolvedIdentifier)keyExp).resolve(writer.getContext(), false);
					}
					catch (SyntaxError /*e*/)
					{
						// TODO add warning 
					}
				if (keyExp is InstanceExpression)
					((InstanceExpression)keyExp).ToDialect(writer, false);
				else
					keyExp.ToDialect(writer);
				writer.append(" as key");
			}
		}

		CodeWriter contextualizeWriter(CodeWriter writer)
		{
			IType type = source.check(writer.getContext());
			IType itemType = ((ContainerType)type).GetItemType();
			if (itemType is CategoryType)
				return writer.newInstanceWriter((CategoryType)itemType);
			else if (itemType is DocumentType)
				return writer.newDocumentWriter();
			else
				return writer;
		}

		private void ToODialect(CodeWriter writer)
		{
			writer.append("sorted ");
			if (descending)
				writer.append("desc ");
			writer.append('(');
			source.ToDialect(writer);
			if (key != null)
			{
				writer = contextualizeWriter(writer);
				writer.append(", key = ");
				key.ToDialect(writer);
			}
			writer.append(")");
		}

		private void ToMDialect(CodeWriter writer)
		{
			ToODialect(writer);
		}

		public IType check(Context context)
		{
			IType type = source.check(context);
			if (!(type is ListType || type is SetType))
				throw new SyntaxError("Unsupported type: " + type);
			return type;
		}

		public IValue interpret(Context context)
		{
			IType type = source.check(context);
			if (!(type is ListType || type is SetType))
				throw new SyntaxError("Unsupported type: " + type);
			IValue o = source.interpret(context);
			if (o == null)
				throw new NullReferenceError();
			if (!(o is IContainer))
				throw new InternalError("Unexpected type:" + o.GetType().Name);
			IType itemType = ((ContainerType)type).GetItemType();
			if (itemType is CategoryType)
				return ((CategoryType)itemType).sort(context, (IContainer)o, key, descending);
			else if (itemType is DocumentType)
				return ((DocumentType)itemType).sort(context, (IContainer)o, key, descending);
			else
				return itemType.sort(context, (IContainer)o, descending);
		}

		public void setKey(IExpression key)
		{
			this.key = key;
		}

	}
}
