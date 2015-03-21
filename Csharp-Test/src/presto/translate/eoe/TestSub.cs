using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestSub : BaseEParserTest
	{

		[Test]
		public void testSubDate()
		{
			compareResourceEOE("sub/subDate.e");
		}

		[Test]
		public void testSubDateTime()
		{
			compareResourceEOE("sub/subDateTime.e");
		}

		[Test]
		public void testSubDecimal()
		{
			compareResourceEOE("sub/subDecimal.e");
		}

		[Test]
		public void testSubInteger()
		{
			compareResourceEOE("sub/subInteger.e");
		}

		[Test]
		public void testSubPeriod()
		{
			compareResourceEOE("sub/subPeriod.e");
		}

		[Test]
		public void testSubTime()
		{
			compareResourceEOE("sub/subTime.e");
		}

	}
}

