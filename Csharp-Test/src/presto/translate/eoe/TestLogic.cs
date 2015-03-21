using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestLogic : BaseEParserTest
	{

		[Test]
		public void testAndBoolean()
		{
			compareResourceEOE("logic/andBoolean.e");
		}

		[Test]
		public void testNotBoolean()
		{
			compareResourceEOE("logic/notBoolean.e");
		}

		[Test]
		public void testOrBoolean()
		{
			compareResourceEOE("logic/orBoolean.e");
		}

	}
}

