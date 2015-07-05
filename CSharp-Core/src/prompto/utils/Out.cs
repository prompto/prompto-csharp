using System.Threading;
using System.IO;
using System;
using System.Text;

namespace prompto.utils
{

    public class Out
    {

        static ThreadLocal<TextWriter> oldOut = new ThreadLocal<TextWriter>();
        static ThreadLocal<MemoryStream> output = new ThreadLocal<MemoryStream>();

        public static void init()
        {
            oldOut.Value = Console.Out;
            output.Value = new MemoryStream();
            Console.SetOut(new StreamWriter(output.Value));
        }

        public static String read()
        {
            TextWriter writer = Console.Out;
            writer.Flush();
            MemoryStream stream = output.Value;
            String result = Encoding.UTF8.GetString(stream.ToArray(), 0, (int)stream.Position);
            stream.SetLength(0);
            return result;
        }

        public static void reset()
        {
        }

        public static void restore()
        {
            Console.SetOut(oldOut.Value);
            oldOut.Value = null;
        }
    }

}