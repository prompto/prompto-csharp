using prompto.runtime;
using prompto.utils;
using System;
using prompto.parser;
using prompto.param;

namespace prompto.param
{

	public class ParameterList : ObjectList<IParameter>
	{

		public ParameterList()
		{
		}

		public ParameterList(params IParameter[] arguments)
		{
			foreach (IParameter param in arguments)
				this.Add(param);
		}

		public void register(Context context)
		{
			foreach (IParameter param in this)
				param.register(context);
		}

		public void check(Context context)
		{
			foreach (IParameter param in this)
				param.check(context);
		}

		public IParameter find(String name)
		{
			foreach (IParameter param in this)
			{
				if (name.Equals(param.GetName()))
					return param;
			}
			return null;
		}
		public void ToDialect(CodeWriter writer)
		{
			if (this.Count == 0)
				return;
			switch (writer.getDialect())
			{
				case Dialect.E:
					ToEDialect(writer);
					break;
				case Dialect.O:
					ToODialect(writer);
					break;
				case Dialect.M:
					toPDialect(writer);
					break;
			}
		}

		private void ToEDialect(CodeWriter writer)
		{
			IParameter last = this.Count > 0 ? this[this.Count - 1] : null;
			writer.append("receiving ");
			foreach (IParameter param in this)
			{
				if (param == last)
					break;
				param.ToDialect(writer);
				writer.append(", ");
			}
			if (this.Count > 1)
			{
				writer.trimLast(2);
				writer.append(" and ");
			}
			last.ToDialect(writer);
			writer.append(" ");
		}

		private void ToODialect(CodeWriter writer)
		{
			foreach (IParameter param in this)
			{
				param.ToDialect(writer);
				writer.append(", ");
			}
			writer.trimLast(2);
		}

		private void toPDialect(CodeWriter writer)
		{
			foreach (IParameter param in this)
			{
				param.ToDialect(writer);
				writer.append(", ");
			}
			writer.trimLast(2);
		}
	}

}