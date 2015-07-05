// generated: 2015-07-05T23:01:01.182
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestCast : BaseOParserTest
	{

		[Test]
		public void testAutoDowncast()
		{
			compareResourceOSO("cast/autoDowncast.poc");
		}

		[Test]
		public void testCastChild()
		{
			compareResourceOSO("cast/castChild.poc");
		}

		[Test]
		public void testIsAChild()
		{
			compareResourceOSO("cast/isAChild.poc");
		}

		[Test]
		public void testIsAText()
		{
			compareResourceOSO("cast/isAText.poc");
		}

	}
}

