using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
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

