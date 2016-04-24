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
		public void testCastMissing()
		{
			compareResourceOSO("cast/castMissing.poc");
		}

		[Test]
		public void testCastNull()
		{
			compareResourceOSO("cast/castNull.poc");
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

