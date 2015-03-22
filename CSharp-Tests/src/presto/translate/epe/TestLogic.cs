using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestLogic : BaseEParserTest
	{

		[Test]
		public void testAndBoolean()
		{
			compareResourceEPE("logic/andBoolean.e");
		}

		[Test]
		public void testNotBoolean()
		{
			compareResourceEPE("logic/notBoolean.e");
		}

		[Test]
		public void testOrBoolean()
		{
			compareResourceEPE("logic/orBoolean.e");
		}

	}
}

