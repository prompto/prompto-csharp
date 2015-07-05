using prompto.error;
using System;
using System.Collections.Generic;
using prompto.grammar;
using prompto.value;
using prompto.type;
using prompto.runtime;
using prompto.declaration;

namespace prompto.value
{

    public interface IInstance : IValue
    {

        CategoryType getType();
        ICollection<String> getMemberNames();
		bool setMutable(bool set);
		ConcreteCategoryDeclaration getDeclaration();
    }

}