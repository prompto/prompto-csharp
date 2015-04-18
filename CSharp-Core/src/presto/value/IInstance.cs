using presto.error;
using System;
using System.Collections.Generic;
using presto.grammar;
using presto.value;
using presto.type;
using presto.runtime;

namespace presto.value
{

    public interface IInstance : IValue
    {

        CategoryType getType();
        ICollection<String> getMemberNames();
		bool setMutable(bool set);

    }

}