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
			compareResourceOEO("sub/subDate.o");
		}

		[Test]
		public void testSubDateTime()
		{
			compareResourceOEO("sub/subDateTime.o");
		}

		[Test]
		public void testSubDecimal()
		{
			compareResourceOEO("sub/subDecimal.o");
		}

		[Test]
		public void testSubInteger()
		{
			compareResourceOEO("sub/subInteger.o");
		}

		[Test]
		public void testSubPeriod()
		{
			compareResourceOEO("sub/subPeriod.o");
		}

		[Test]
		public void testSubTime()
		{
			compareResourceOEO("sub/subTime.o");
		}

	}
}

