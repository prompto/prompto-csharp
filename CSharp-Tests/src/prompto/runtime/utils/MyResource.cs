using System;
using prompto.runtime;
using prompto.value;
using System.Collections.Generic;

namespace prompto.runtime.utils
{

    public class MyResource : IResource
    {

		public static Dictionary<String,String> contents = new Dictionary<String, String>();

        public String path { get; set; }
		public String content { 
			get {
				lock (contents) {
					return contents [path];
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
