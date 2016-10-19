using prompto.value;
using System.Net;
using System;
using System.IO;

namespace prompto.internet
{
	public class Url : IResource
	{

		Uri url;
		String _encoding;
		WebClient client = null;
		StreamReader reader = null;

		public String path 
		{
			get {
				return url != null ? url.ToString () : "";
			}
			set {
				url = new Uri (value);
			}
		}

		public String encoding
		{
			set {
				this._encoding = value;
			}
			get {
				return _encoding!=null ? _encoding : "utf-8";
			}
		}

		public bool isReadable ()
		{
			return url != null;
		}

		public bool isWritable ()
		{
			return url != null;
		}

		public void close ()
		{
			if (reader != null)
			{
				reader.Close();
				reader = null;
			}
			if (client != null)
			{
				client.Dispose();
				client = null;
			}
		}

		public String readFully ()
		{
			using (WebClient client = new WebClient ()) {
				return client.DownloadString (url);
			}
		}

		public void writeFully (String data)
		{
			throw new Exception("Unsupported!");
		}

		public String readLine()
		{
			if (client == null)
				client = new WebClient();
			if (reader == null)
				reader = new StreamReader(client.OpenRead( url));
			return reader.ReadLine();
		}

		public void writeLine(String data)
		{
			throw new Exception("Unsupported!");
		}

	}
}