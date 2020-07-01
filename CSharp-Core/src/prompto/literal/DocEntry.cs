using System;
using System.Text;
using prompto.value;
using prompto.runtime;
using prompto.expression;
using prompto.utils;

namespace prompto.literal
{


	public class DocEntry : Entry<DocKey>
	{

		public DocEntry(DocKey key, IExpression value)
			: base(key, value) 
		{
		}

	}

}
