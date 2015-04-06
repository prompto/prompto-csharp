using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
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

