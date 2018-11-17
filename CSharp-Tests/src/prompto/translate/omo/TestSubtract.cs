using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestSubtract : BaseOParserTest
	{

		[Test]
		public void testSubDate()
		{
			compareResourceOMO("subtract/subDate.poc");
		}

		[Test]
		public void testSubDateTime()
		{
			compareResourceOMO("subtract/subDateTime.poc");
		}

		[Test]
		public void testSubDecimal()
		{
			compareResourceOMO("subtract/subDecimal.poc");
		}

		[Test]
		public void testSubInteger()
		{
			compareResourceOMO("subtract/subInteger.poc");
		}

		[Test]
		public void testSubList()
		{
			compareResourceOMO("subtract/subList.poc");
		}

		[Test]
		public void testSubPeriod()
		{
			compareResourceOMO("subtract/subPeriod.poc");
		}

		[Test]
		public void testSubSet()
		{
			compareResourceOMO("subtract/subSet.poc");
		}

		[Test]
		public void testSubTime()
		{
			compareResourceOMO("subtract/subTime.poc");
		}

	}
}

