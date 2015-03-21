using System;
namespace presto.value
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
