using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestBuiltins : BaseEParserTest
	{

		[Test]
		public void testCharCodePoint()
		{
			compareResourceEOE("builtins/charCodePoint.pec");
		}

		[Test]
		public void testDateDayOfMonth()
		{
			compareResourceEOE("builtins/dateDayOfMonth.pec");
		}

		[Test]
		public void testDateDayOfYear()
		{
			compareResourceEOE("builtins/dateDayOfYear.pec");
		}

		[Test]
		public void testDateMonth()
		{
			compareResourceEOE("builtins/dateMonth.pec");
		}

		[Test]
		public void testDateTimeDayOfMonth()
		{
			compareResourceEOE("builtins/dateTimeDayOfMonth.pec");
		}

		[Test]
		public void testDateTimeDayOfYear()
		{
			compareResourceEOE("builtins/dateTimeDayOfYear.pec");
		}

		[Test]
		public void testDateTimeHour()
		{
			compareResourceEOE("builtins/dateTimeHour.pec");
		}

		[Test]
		public void testDateTimeMinute()
		{
			compareResourceEOE("builtins/dateTimeMinute.pec");
		}

		[Test]
		public void testDateTimeMonth()
		{
			compareResourceEOE("builtins/dateTimeMonth.pec");
		}

		[Test]
		public void testDateTimeSecond()
		{
			compareResourceEOE("builtins/dateTimeSecond.pec");
		}

		[Test]
		public void testDateTimeTZName()
		{
			compareResourceEOE("builtins/dateTimeTZName.pec");
		}

		[Test]
		public void testDateTimeTZOffset()
		{
			compareResourceEOE("builtins/dateTimeTZOffset.pec");
		}

		[Test]
		public void testDateTimeYear()
		{
			compareResourceEOE("builtins/dateTimeYear.pec");
		}

		[Test]
		public void testDateYear()
		{
			compareResourceEOE("builtins/dateYear.pec");
		}

		[Test]
		public void testDictCount()
		{
			compareResourceEOE("builtins/dictCount.pec");
		}

		[Test]
		public void testEnumName()
		{
			compareResourceEOE("builtins/enumName.pec");
		}

		[Test]
		public void testEnumSymbols()
		{
			compareResourceEOE("builtins/enumSymbols.pec");
		}

		[Test]
		public void testEnumValue()
		{
			compareResourceEOE("builtins/enumValue.pec");
		}

		[Test]
		public void testListCount()
		{
			compareResourceEOE("builtins/listCount.pec");
		}

		[Test]
		public void testSetCount()
		{
			compareResourceEOE("builtins/setCount.pec");
		}

		[Test]
		public void testTextCount()
		{
			compareResourceEOE("builtins/textCount.pec");
		}

		[Test]
		public void testTimeHour()
		{
			compareResourceEOE("builtins/timeHour.pec");
		}

		[Test]
		public void testTimeMinute()
		{
			compareResourceEOE("builtins/timeMinute.pec");
		}

		[Test]
		public void testTimeSecond()
		{
			compareResourceEOE("builtins/timeSecond.pec");
		}

		[Test]
		public void testTupleCount()
		{
			compareResourceEOE("builtins/tupleCount.pec");
		}

	}
}

