using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestSubtract : BaseOParserTest
	{

		[Test]
		public void testSubDate()
		{
			compareResourceOEO("subtract/subDate.poc");
		}

		[Test]
		public void testSubDateTime()
		{
			compareResourceOEO("subtract/subDateTime.poc");
		}

		[Test]
		public void testSubDecimal()
		{
			compareResourceOEO("subtract/subDecimal.poc");
		}

		[Test]
		public void testSubInteger()
		{
			compareResourceOEO("subtract/subInteger.poc");
		}

		[Test]
		public void testSubList()
		{
			compareResourceOEO("subtract/subList.poc");
		}

		[Test]
		public void testSubPeriod()
		{
			compareResourceOEO("subtract/subPeriod.poc");
		}

		[Test]
		public void testSubSet()
		{
			compareResourceOEO("subtract/subSet.poc");
		}

		[Test]
		public void testSubTime()
		{
			compareResourceOEO("subtract/subTime.poc");
		}

	}
}

