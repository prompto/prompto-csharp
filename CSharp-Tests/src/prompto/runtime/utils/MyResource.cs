using System;
using prompto.runtime;
using prompto.value;
using System.Collections.Generic;
using System.IO;
using prompto.intrinsic;
using System.Text;

namespace prompto.runtime.utils
{

    public class MyResource : IResource
    {

		public static Dictionary<String,String> contents = new Dictionary<String, String>();

        StringReader reader = null;

		public String path { get; set; }

		public String content { 
			get {
				lock (contents) {
					String val;
					if (contents.TryGetValue(path, out val))
						return val;
					else
						return "";
				}
			}
			set {
				lock (contents) {
					contents [path] = value;
				}
			}
		}
 
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
			if (reader != null)
			{
				reader.Close();
				reader = null;
			}
        }

        public Binary readBinary()
        {
			byte[] data = Encoding.UTF8.GetBytes(content);
			return new Binary("text/plain", data);
        }

        public String readFully()
        {
            return content;
        }

        public void writeFully(String data)
        {
            content = data;
        }

		public void writeFully(string data, Action<string> thenWith)
		{
			writeFully(data);
			thenWith.Invoke("accepted: " + readFully());
		}


		public String readLine()
		{
			if(reader==null)
				reader = new StringReader(content);
			return reader.ReadLine();
		}

		public void writeLine(String data)
		{
			String val = content;
			if(val.Length>0)
				val += '\n';
			content = val + data;
		}

    }
}
