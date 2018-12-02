using prompto.utils;
using prompto.error;
using prompto.parser;
using prompto.runtime;
using System;
using prompto.type;
using prompto.declaration;
using prompto.grammar;
using System.Collections.Generic;

namespace prompto.argument
{

	public class MethodArgument : BaseArgument, INamedArgument
	{

		public MethodArgument(String name)
			: base(name)
		{
		}


		public override String getSignature(Dialect dialect)
		{
			return GetName();
		}


		public override void ToDialect(CodeWriter writer)
		{
			writer.append(GetName());
		}


		public override String getProto()
		{
			return GetName();
		}


		public override bool Equals(Object obj)
		{
			if (obj == this)
				return true;
			if (obj == null)
				return false;
			if (!(obj is MethodArgument))
				return false;
			MethodArgument other = (MethodArgument)obj;
			return ObjectUtils.AreEqual(this.GetName(), other.GetName());
		}


		public override void register(Context context)
		{
			INamed actual = context.getRegisteredValue<INamed>(name);
			if (actual != null)
				throw new SyntaxError("Duplicate argument: \"" + name + "\"");
			context.registerValue(this);
		}


		public override void check(Context context)
		{
			IMethodDeclaration actual = context.getRegisteredDeclaration<IMethodDeclaration>(name);
			if (actual == null)
				throw new SyntaxError("Unknown method: \"" + name + "\"");
		}


		public override IType GetIType(Context context)
		{
			IMethodDeclaration actual = getDeclaration(context);
			return new MethodType(actual);
		}

		private IMethodDeclaration getDeclaration(Context context)
		{
			MethodDeclarationMap methods = context.getRegisteredDeclaration<MethodDeclarationMap>(name);
			if (methods != null)
			{
				IEnumerator<IMethodDeclaration> values = methods.Values.GetEnumerator();
				values.MoveNext();
				return values.Current;
			}
			else
				return null;
		}

	}

}