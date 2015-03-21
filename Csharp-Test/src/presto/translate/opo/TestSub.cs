using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestSub : BaseOParserTest
	{

		[Test]
		public void testSubDate()
		{
			compareResourceOPO("sub/subDate.o");
		}

		[Test]
		public void testSubDateTime()
		{
			compareResourceOPO("sub/subDateTime.o");
		}

		[Test]
		public void testSubDecimal()
		{
			compareResourceOPO("sub/subDecimal.o");
		}

		[Test]
		public void testSubInteger()
		{
			compareResourceOPO("sub/subInteger.o");
		}

		[Test]
		public void testSubPeriod()
		{
			compareResourceOPO("sub/subPeriod.o");
		}

		[Test]
		public void testSubTime()
		{
			compareResourceOPO("sub/subTime.o");
		}

	}
}

