using System;
using NUnit.Framework;
using System.Globalization;

namespace prompto.utils
{


	[TestFixture]	
	public class DoubleTest
	{
		[Test]
		public void TestParse ()
		{
			double d = Double.Parse ("123.45", CultureInfo.InvariantCulture);
			Assert.AreEqual (d, 123.45);
		}
	}
}

