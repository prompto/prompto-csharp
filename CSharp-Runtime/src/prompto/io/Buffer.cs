using prompto.intrinsic;
using prompto.value;
using System;
using System.IO;
using System.Text;

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

		public Binary readBinary()
		{
			byte[] bytes = Encoding.UTF8.GetBytes(data);
			return new Binary("text/plain", bytes);
		}

		public String readFully ()
		{
			return data;
		}

		public void writeFully (String data)
		{
			this.data = data;
		}

		public void writeFully(string data, Action<string> thenWith)
		{
			thenWith.Invoke(data);
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