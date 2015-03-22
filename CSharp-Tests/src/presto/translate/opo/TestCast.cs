using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestCast : BaseOParserTest
	{

		[Test]
		public void testAutoDowncast()
		{
			compareResourceOPO("cast/autoDowncast.o");
		}

		[Test]
		public void testCastChild()
		{
			compareResourceOPO("cast/castChild.o");
		}

		[Test]
		public void testIsAChild()
		{
			compareResourceOPO("cast/isAChild.o");
		}

		[Test]
		public void testIsAText()
		{
			compareResourceOPO("cast/isAText.o");
		}

	}
}

