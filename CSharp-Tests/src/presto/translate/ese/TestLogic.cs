using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
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

