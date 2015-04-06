using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
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

