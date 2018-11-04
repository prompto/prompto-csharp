using System;

namespace prompto.statement
{
	public abstract class SimpleStatement : BaseStatement
	{
		public override bool IsSimple
		{
			get
			{
				return true;
			}
		}
	}
}

