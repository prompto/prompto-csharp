// generated: 2015-07-05T23:01:01.311
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestLogic : BaseEParserTest
	{

		[Test]
		public void testAndBoolean()
		{
			compareResourceESE("logic/andBoolean.pec");
		}

		[Test]
		public void testNotBoolean()
		{
			compareResourceESE("logic/notBoolean.pec");
		}

		[Test]
		public void testOrBoolean()
		{
			compareResourceESE("logic/orBoolean.pec");
		}

	}
}

