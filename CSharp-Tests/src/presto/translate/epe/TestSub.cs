using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestSub : BaseEParserTest
	{

		[Test]
		public void testSubDate()
		{
			compareResourceEPE("sub/subDate.e");
		}

		[Test]
		public void testSubDateTime()
		{
			compareResourceEPE("sub/subDateTime.e");
		}

		[Test]
		public void testSubDecimal()
		{
			compareResourceEPE("sub/subDecimal.e");
		}

		[Test]
		public void testSubInteger()
		{
			compareResourceEPE("sub/subInteger.e");
		}

		[Test]
		public void testSubPeriod()
		{
			compareResourceEPE("sub/subPeriod.e");
		}

		[Test]
		public void testSubTime()
		{
			compareResourceEPE("sub/subTime.e");
		}

	}
}

