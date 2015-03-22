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
			compareResourceOEO("logic/andBoolean.o");
		}

		[Test]
		public void testNotBoolean()
		{
			compareResourceOEO("logic/notBoolean.o");
		}

		[Test]
		public void testOrBoolean()
		{
			compareResourceOEO("logic/orBoolean.o");
		}

	}
}

