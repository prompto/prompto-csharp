using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestCast : BaseEParserTest
	{

		[Test]
		public void testAutoDowncast()
		{
			compareResourceEOE("cast/autoDowncast.pec");
		}

		[Test]
		public void testCastChild()
		{
			compareResourceEOE("cast/castChild.pec");
		}

		[Test]
		public void testIsAChild()
		{
			compareResourceEOE("cast/isAChild.pec");
		}

		[Test]
		public void testIsAText()
		{
			compareResourceEOE("cast/isAText.pec");
		}

	}
}
