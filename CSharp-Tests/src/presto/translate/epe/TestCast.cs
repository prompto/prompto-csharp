using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestCast : BaseEParserTest
	{

		[Test]
		public void testAutoDowncast()
		{
			compareResourceEPE("cast/autoDowncast.e");
		}

		[Test]
		public void testCastChild()
		{
			compareResourceEPE("cast/castChild.e");
		}

		[Test]
		public void testIsAChild()
		{
			compareResourceEPE("cast/isAChild.e");
		}

		[Test]
		public void testIsAText()
		{
			compareResourceEPE("cast/isAText.e");
		}

	}
}

