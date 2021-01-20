using prompto.grammar;
using System;
using prompto.declaration;
using prompto.intrinsic;

namespace prompto.value
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

        public Binary readBinary()
        {
            return ((IResource)instance).readBinary();
        }

        public String readFully()
        {
            return ((IResource)instance).readFully();
        }

        public void writeFully(String data)
        {
            ((IResource)instance).writeFully(data);
        }

        public void writeFully(String data, Action<String> thenWith)
        {
            ((IResource)instance).writeFully(data, thenWith);
        }

        public String readLine()
		{
			return ((IResource)instance).readLine();
		}

		public void writeLine(String data)
		{
			((IResource)instance).writeLine(data);
		}

		public void close()
        {
            ((IResource)instance).close();
        }

    }

}
