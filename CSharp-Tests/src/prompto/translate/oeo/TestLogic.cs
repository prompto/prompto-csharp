// generated: 2015-07-05T23:01:01.313
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestLogic : BaseOParserTest
	{

		[Test]
		public void testAndBoolean()
		{
			compareResourceOEO("logic/andBoolean.poc");
		}

		[Test]
		public void testNotBoolean()
		{
			compareResourceOEO("logic/notBoolean.poc");
		}

		[Test]
		public void testOrBoolean()
		{
			compareResourceOEO("logic/orBoolean.poc");
		}

	}
}

