using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestLogic : BaseOParserTest
	{

		[Test]
		public void testAndBoolean()
		{
			compareResourceOPO("logic/andBoolean.o");
		}

		[Test]
		public void testNotBoolean()
		{
			compareResourceOPO("logic/notBoolean.o");
		}

		[Test]
		public void testOrBoolean()
		{
			compareResourceOPO("logic/orBoolean.o");
		}

	}
}

