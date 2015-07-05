// generated: 2015-07-05T23:01:01.310
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
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

