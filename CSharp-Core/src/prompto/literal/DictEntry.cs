using System;
using System.Text;
using prompto.value;
using prompto.runtime;
using prompto.expression;
using prompto.utils;

namespace prompto.literal
{


	public class DictEntry : Entry<DictKey>
	{

		public DictEntry(DictKey key, IExpression value)
			: base(key, value) 
		{
		}

	}

}
