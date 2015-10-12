using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
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

