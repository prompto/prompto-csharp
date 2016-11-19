using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestSub : BaseOParserTest
	{

		[Test]
		public void testSubDate()
		{
			compareResourceOMO("sub/subDate.poc");
		}

		[Test]
		public void testSubDateTime()
		{
			compareResourceOMO("sub/subDateTime.poc");
		}

		[Test]
		public void testSubDecimal()
		{
			compareResourceOMO("sub/subDecimal.poc");
		}

		[Test]
		public void testSubInteger()
		{
			compareResourceOMO("sub/subInteger.poc");
		}

		[Test]
		public void testSubPeriod()
		{
			compareResourceOMO("sub/subPeriod.poc");
		}

		[Test]
		public void testSubTime()
		{
			compareResourceOMO("sub/subTime.poc");
		}

	}
}

