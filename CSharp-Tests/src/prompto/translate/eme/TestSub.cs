using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestSub : BaseEParserTest
	{

		[Test]
		public void testSubDate()
		{
			compareResourceEME("sub/subDate.pec");
		}

		[Test]
		public void testSubDateTime()
		{
			compareResourceEME("sub/subDateTime.pec");
		}

		[Test]
		public void testSubDecimal()
		{
			compareResourceEME("sub/subDecimal.pec");
		}

		[Test]
		public void testSubDecimalEnum()
		{
			compareResourceEME("sub/subDecimalEnum.pec");
		}

		[Test]
		public void testSubInteger()
		{
			compareResourceEME("sub/subInteger.pec");
		}

		[Test]
		public void testSubIntegerEnum()
		{
			compareResourceEME("sub/subIntegerEnum.pec");
		}

		[Test]
		public void testSubPeriod()
		{
			compareResourceEME("sub/subPeriod.pec");
		}

		[Test]
		public void testSubTime()
		{
			compareResourceEME("sub/subTime.pec");
		}

	}
}

