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
			compareResourceEOE("logic/andBoolean.pec");
		}

		[Test]
		public void testNotBoolean()
		{
			compareResourceEOE("logic/notBoolean.pec");
		}

		[Test]
		public void testOrBoolean()
		{
			compareResourceEOE("logic/orBoolean.pec");
		}

	}
}

