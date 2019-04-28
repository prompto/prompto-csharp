using System;
using prompto.runtime;
using prompto.error;
using prompto.parser;
using prompto.type;
using prompto.value;
using prompto.utils;
using prompto.grammar;
using System.Collections.Generic;

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
				IType type = source.check(writer.getContext());
				IType itemType = ((ContainerType)type).GetItemType();
				CodeWriter local = contextualizeWriter(writer, itemType);
				local.append(" with ");
				IExpression keyExp = key;
				if (keyExp is UnresolvedIdentifier) try
				{
					keyExp = ((UnresolvedIdentifier)keyExp).resolve(writer.getContext(), false);
				}
				catch (SyntaxError /*e*/)
				{
					// TODO add warning 
				}
				if(keyExp is ArrowExpression) {
					foreach(String arg in ((ArrowExpression)keyExp).Arguments)
					{
						Variable param = new Variable(arg, itemType);
						local.getContext().registerValue(param);
					}
					keyExp.ToDialect(local);
				} else if (keyExp is InstanceExpression)
					((InstanceExpression)keyExp).ToDialect(local, false);
				else
					keyExp.ToDialect(local);
				writer.append(" as key");
			}
		}

		CodeWriter contextualizeWriter(CodeWriter writer, IType itemType)
		{
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
				IType type = source.check(writer.getContext());
				IType itemType = ((ContainerType)type).GetItemType();
				writer = contextualizeWriter(writer, itemType);
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
			IValue values = source.interpret(context);
			if (values == null)
				throw new NullReferenceError();
			if (!(values is IContainer))
				throw new InternalError("Unexpected type:" + values.GetType().Name);
			IType itemType = ((ContainerType)type).GetItemType();
			Comparer<IValue> comparer = itemType.getComparer(context, key, descending);
			ListValue result = new ListValue(itemType);
			result.AddRange (((IContainer)values).GetEnumerable (context));
			result.Sort (comparer);
			return result;
		}

		public void setKey(IExpression key)
		{
			this.key = key;
		}

	}
}
