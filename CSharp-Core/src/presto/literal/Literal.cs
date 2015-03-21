
using presto.runtime;
using System;
using presto.parser;
using presto.value;
using presto.type;
using presto.expression;
using presto.utils;

namespace presto.literal
{

	public abstract class Literal<T> : IExpression where T : IValue
	{
	
		String text;
		protected T value;

		protected Literal (String text, T value)
		{
			this.text = text;
			this.value = value;
		}


		public override String ToString ()
		{
			return text;
		}

		public virtual void ToDialect (CodeWriter writer)
		{
			writer.append (text);
		}


		public override bool Equals (Object obj)
		{
			if (obj is Literal<T>)
				return value.Equals (((Literal<T>)obj).value);
			else
				return value.Equals (obj);
		}

		public T getValue ()
		{
			return value;
		}

		public virtual IValue interpret (Context context)
		{
			return value;
		}

		public abstract IType check (Context context);

	}

}
