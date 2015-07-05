// generated: 2015-07-05T23:01:01.432
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestSub : BaseOParserTest
	{

		[Test]
		public void testSubDate()
		{
			compareResourceOSO("sub/subDate.poc");
		}

		[Test]
		public void testSubDateTime()
		{
			compareResourceOSO("sub/subDateTime.poc");
		}

		[Test]
		public void testSubDecimal()
		{
			compareResourceOSO("sub/subDecimal.poc");
		}

		[Test]
		public void testSubInteger()
		{
			compareResourceOSO("sub/subInteger.poc");
		}

		[Test]
		public void testSubPeriod()
		{
			compareResourceOSO("sub/subPeriod.poc");
		}

		[Test]
		public void testSubTime()
		{
			compareResourceOSO("sub/subTime.poc");
		}

	}
}

