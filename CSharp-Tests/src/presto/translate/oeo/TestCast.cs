using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestCast : BaseOParserTest
	{

		[Test]
		public void testAutoDowncast()
		{
			compareResourceOEO("cast/autoDowncast.poc");
		}

		[Test]
		public void testCastChild()
		{
			compareResourceOEO("cast/castChild.poc");
		}

		[Test]
		public void testIsAChild()
		{
			compareResourceOEO("cast/isAChild.poc");
		}

		[Test]
		public void testIsAText()
		{
			compareResourceOEO("cast/isAText.poc");
		}

	}
}

