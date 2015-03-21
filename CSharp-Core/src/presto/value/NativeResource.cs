using presto.grammar;
using System;
using presto.declaration;

namespace presto.value
{

    public class NativeResource : NativeInstance, IResource
    {

        public NativeResource(NativeResourceDeclaration declaration)
            : base(declaration)
        {
        }

        public bool isReadable()
        {
            return ((IResource)instance).isReadable();
        }

        public bool isWritable()
        {
            return ((IResource)instance).isWritable();
        }

        public String readFully()
        {
            return ((IResource)instance).readFully();
        }

        public void writeFully(String data)
        {
            ((IResource)instance).writeFully(data);
        }

        public void close()
        {
            ((IResource)instance).close();
        }

    }

}
