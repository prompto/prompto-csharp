using prompto.value;
using System.Net;
using System;

namespace prompto.internet
{
	public class Url : IResource
	{

		Uri url;
		String _encoding;

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
		}

		public String readFully ()
		{
			using (WebClient client = new WebClient ()) {
				return client.DownloadString (url);
			}
		}

		public void writeFully (String data)
		{
		}


	}
}