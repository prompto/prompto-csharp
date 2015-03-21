using System.Collections.Generic;
using presto.type;
namespace presto.grammar {

    public class TypeList : List<IType>
    {

        public TypeList()
        {
        }

        public TypeList(IType type)
        {
            this.Add(type);
        }

        /* for unified grammar */
        public void add(IType type)
        {
            this.Add(type);
        }
   
    }
}
