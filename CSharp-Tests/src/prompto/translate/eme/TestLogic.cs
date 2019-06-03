using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestLogic : BaseEParserTest
	{

		[Test]
		public void testAndBoolean()
		{
			compareResourceEME("logic/andBoolean.pec");
		}

		[Test]
		public void testNotBoolean()
		{
			compareResourceEME("logic/notBoolean.pec");
		}

		[Test]
		public void testOrBoolean()
		{
			compareResourceEME("logic/orBoolean.pec");
		}

		[Test]
		public void testRightSkipped()
		{
			compareResourceEME("logic/rightSkipped.pec");
		}

	}
}

