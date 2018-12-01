using System;
using System.Threading;

namespace prompto.utils
{
	public abstract class Utils
	{
		public static void Sleep(Int64 millis)
		{
			while (millis > Int32.MaxValue)
			{
				Thread.Sleep(Int32.MaxValue);
				millis -= Int32.MaxValue;
			}
			Thread.Sleep((Int32)millis);
		}
	}
}
