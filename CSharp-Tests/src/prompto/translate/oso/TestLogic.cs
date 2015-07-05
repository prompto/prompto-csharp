// generated: 2015-07-05T23:01:01.314
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestLogic : BaseOParserTest
	{

		[Test]
		public void testAndBoolean()
		{
			compareResourceOSO("logic/andBoolean.poc");
		}

		[Test]
		public void testNotBoolean()
		{
			compareResourceOSO("logic/notBoolean.poc");
		}

		[Test]
		public void testOrBoolean()
		{
			compareResourceOSO("logic/orBoolean.poc");
		}

	}
}

