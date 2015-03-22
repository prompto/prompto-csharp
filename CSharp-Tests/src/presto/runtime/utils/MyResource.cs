using System;
using presto.runtime;
using presto.value;

namespace presto.runtime.utils
{

    public class MyResource : IResource
    {

        public static String content;

        public String path { get; set; }
 
        public bool isReadable()
        {
            return true;
        }

        public bool isWritable()
        {
            return true;
        }

        public void close()
        {
        }

        public String readFully()
        {
            return content;
        }

        public void writeFully(String data)
        {
            content = data;
        }
    }
}
