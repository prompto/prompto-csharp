using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Text;

namespace prompto.utils
{
    public static class PromptoExtensions
    {
        public static void add<T>(this List<T> list, T elem)
        {
            list.Add(elem);
        }

    }
}
