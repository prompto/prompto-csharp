using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestSub : BaseEParserTest
	{

		[Test]
		public void testSubDate()
		{
			compareResourceESE("sub/subDate.pec");
		}

		[Test]
		public void testSubDateTime()
		{
			compareResourceESE("sub/subDateTime.pec");
		}

		[Test]
		public void testSubDecimal()
		{
			compareResourceESE("sub/subDecimal.pec");
		}

		[Test]
		public void testSubInteger()
		{
			compareResourceESE("sub/subInteger.pec");
		}

		[Test]
		public void testSubPeriod()
		{
			compareResourceESE("sub/subPeriod.pec");
		}

		[Test]
		public void testSubTime()
		{
			compareResourceESE("sub/subTime.pec");
		}

	}
}

