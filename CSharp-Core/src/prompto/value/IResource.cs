using System;
namespace prompto.value
{

    public interface IResource
    {

        bool isReadable();
        bool isWritable();
        String readFully();
        void writeFully(String data);
        void close();

    }

}
