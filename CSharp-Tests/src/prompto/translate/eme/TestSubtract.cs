using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestSubtract : BaseEParserTest
	{

		[Test]
		public void testSubDate()
		{
			compareResourceEME("subtract/subDate.pec");
		}

		[Test]
		public void testSubDateTime()
		{
			compareResourceEME("subtract/subDateTime.pec");
		}

		[Test]
		public void testSubDecimal()
		{
			compareResourceEME("subtract/subDecimal.pec");
		}

		[Test]
		public void testSubDecimalEnum()
		{
			compareResourceEME("subtract/subDecimalEnum.pec");
		}

		[Test]
		public void testSubInteger()
		{
			compareResourceEME("subtract/subInteger.pec");
		}

		[Test]
		public void testSubIntegerEnum()
		{
			compareResourceEME("subtract/subIntegerEnum.pec");
		}

		[Test]
		public void testSubList()
		{
			compareResourceEME("subtract/subList.pec");
		}

		[Test]
		public void testSubPeriod()
		{
			compareResourceEME("subtract/subPeriod.pec");
		}

		[Test]
		public void testSubSet()
		{
			compareResourceEME("subtract/subSet.pec");
		}

		[Test]
		public void testSubTime()
		{
			compareResourceEME("subtract/subTime.pec");
		}

	}
}

