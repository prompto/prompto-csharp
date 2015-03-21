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
			compareResourceEOE("cast/autoDowncast.e");
		}

		[Test]
		public void testCastChild()
		{
			compareResourceEOE("cast/castChild.e");
		}

		[Test]
		public void testIsAChild()
		{
			compareResourceEOE("cast/isAChild.e");
		}

		[Test]
		public void testIsAText()
		{
			compareResourceEOE("cast/isAText.e");
		}

	}
}

