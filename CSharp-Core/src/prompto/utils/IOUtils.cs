using System;
using System.IO;

namespace prompto.utils
{
	public abstract class IOUtils
	{
		public static byte[] ReadStreamFully(Stream stream) {
			MemoryStream data = new MemoryStream ();
			byte[] buffer = new byte[4096];
			for (;;) {
				int read = stream.Read(buffer, 0, buffer.Length);
				if (read <= 0)
					break;
				data.Write (buffer, 0, read);
			}
			data.Flush ();
			return data.ToArray ();
		}
	}
}

