using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
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
		public void testAutoDowncastMethod()
		{
			compareResourceOEO("cast/autoDowncastMethod.poc");
		}

		[Test]
		public void testCastChild()
		{
			compareResourceOEO("cast/castChild.poc");
		}

		[Test]
		public void testCastMethod()
		{
			compareResourceOEO("cast/castMethod.poc");
		}

		[Test]
		public void testCastMissing()
		{
			compareResourceOEO("cast/castMissing.poc");
		}

		[Test]
		public void testCastNull()
		{
			compareResourceOEO("cast/castNull.poc");
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

		[Test]
		public void testNullIsNotAText()
		{
			compareResourceOEO("cast/nullIsNotAText.poc");
		}

	}
}

