using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestSubtract : BaseEParserTest
	{

		[Test]
		public void testSubDate()
		{
			compareResourceEOE("subtract/subDate.pec");
		}

		[Test]
		public void testSubDateTime()
		{
			compareResourceEOE("subtract/subDateTime.pec");
		}

		[Test]
		public void testSubDecimal()
		{
			compareResourceEOE("subtract/subDecimal.pec");
		}

		[Test]
		public void testSubDecimalEnum()
		{
			compareResourceEOE("subtract/subDecimalEnum.pec");
		}

		[Test]
		public void testSubInteger()
		{
			compareResourceEOE("subtract/subInteger.pec");
		}

		[Test]
		public void testSubIntegerEnum()
		{
			compareResourceEOE("subtract/subIntegerEnum.pec");
		}

		[Test]
		public void testSubList()
		{
			compareResourceEOE("subtract/subList.pec");
		}

		[Test]
		public void testSubPeriod()
		{
			compareResourceEOE("subtract/subPeriod.pec");
		}

		[Test]
		public void testSubSet()
		{
			compareResourceEOE("subtract/subSet.pec");
		}

		[Test]
		public void testSubTime()
		{
			compareResourceEOE("subtract/subTime.pec");
		}

	}
}

