using System;
using prompto.intrinsic;

namespace prompto.value
{

    public interface IResource
    {

        bool isReadable();
        bool isWritable();
        Binary readBinary();
        String readFully();
        void writeFully(String data);
		String readLine();
		void writeLine(String data);
        void close();

    }

}
