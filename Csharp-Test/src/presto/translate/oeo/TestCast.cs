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
			compareResourceOEO("cast/autoDowncast.o");
		}

		[Test]
		public void testCastChild()
		{
			compareResourceOEO("cast/castChild.o");
		}

		[Test]
		public void testIsAChild()
		{
			compareResourceOEO("cast/isAChild.o");
		}

		[Test]
		public void testIsAText()
		{
			compareResourceOEO("cast/isAText.o");
		}

	}
}

