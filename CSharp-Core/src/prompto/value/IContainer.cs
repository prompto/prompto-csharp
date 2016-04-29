using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using prompto.runtime;
using prompto.type;
using prompto.expression;

namespace prompto.value
{
	public interface IContainer : IIterable
    {
		bool HasItem(Context context, IValue item);
   }
}
