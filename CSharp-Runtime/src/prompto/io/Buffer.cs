using prompto.value;
using System;
using System.IO;

namespace prompto.io
{
	public class Buffer : IResource
	{
		String data = "";
		StringReader reader = null;

		public String text
		{
			get
			{
				return data;
			}
		}

		public bool isReadable ()
		{
			return true;
		}

		public bool isWritable ()
		{
			return true;
		}

		public void close ()
		{
			if (reader != null)
			{
				reader.Close();
				reader = null;
			}
		}

		public String readFully ()
		{
			return data;
		}

		public void writeFully (String data)
		{
			this.data = data;
		}

		public String readLine()
		{
			if (reader == null)
				reader = new StringReader(data);
			return reader.ReadLine();
		}

		public void writeLine(string data)
		{
			this.data = this.data + data + '\n';
		}
	}
}