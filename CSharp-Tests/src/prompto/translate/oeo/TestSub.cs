// generated: 2015-07-05T23:01:01.431
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestSub : BaseOParserTest
	{

		[Test]
		public void testSubDate()
		{
			compareResourceOEO("sub/subDate.poc");
		}

		[Test]
		public void testSubDateTime()
		{
			compareResourceOEO("sub/subDateTime.poc");
		}

		[Test]
		public void testSubDecimal()
		{
			compareResourceOEO("sub/subDecimal.poc");
		}

		[Test]
		public void testSubInteger()
		{
			compareResourceOEO("sub/subInteger.poc");
		}

		[Test]
		public void testSubPeriod()
		{
			compareResourceOEO("sub/subPeriod.poc");
		}

		[Test]
		public void testSubTime()
		{
			compareResourceOEO("sub/subTime.poc");
		}

	}
}

