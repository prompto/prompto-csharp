using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using presto.runtime;
using presto.type;

namespace presto.value
{
    public interface IContainer : IValue
    {
		IType ItemType { get; }
        bool HasItem(Context context, IValue iValue);
        IValue GetItem(Context context, IValue item);
		IEnumerable<IValue> GetItems(Context context);
    }
}
