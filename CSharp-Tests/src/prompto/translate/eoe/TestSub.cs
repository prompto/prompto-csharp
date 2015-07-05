// generated: 2015-07-05T23:01:01.428
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestSub : BaseEParserTest
	{

		[Test]
		public void testSubDate()
		{
			compareResourceEOE("sub/subDate.pec");
		}

		[Test]
		public void testSubDateTime()
		{
			compareResourceEOE("sub/subDateTime.pec");
		}

		[Test]
		public void testSubDecimal()
		{
			compareResourceEOE("sub/subDecimal.pec");
		}

		[Test]
		public void testSubInteger()
		{
			compareResourceEOE("sub/subInteger.pec");
		}

		[Test]
		public void testSubPeriod()
		{
			compareResourceEOE("sub/subPeriod.pec");
		}

		[Test]
		public void testSubTime()
		{
			compareResourceEOE("sub/subTime.pec");
		}

	}
}

