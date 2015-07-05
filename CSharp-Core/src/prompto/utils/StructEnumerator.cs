using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace prompto.utils
{
    public class StructEnumerator<T> : IEnumerator<object> where T : struct
    {
        T? current = null;
        IEnumerator<T> src;

        public StructEnumerator(IEnumerator<T> src) 
        { 
            this.src = src;
        }

        public object Current { get { return current; } }
 
        public bool MoveNext()
        {
            current = null;
            bool res = src.MoveNext();
            if (res)
                current = src.Current;
            return res;
        }

        public void Reset()
        {
            src.Reset();
            current = null;
        }

        public void Dispose()
        {
            src.Dispose();
        }

    }
}
