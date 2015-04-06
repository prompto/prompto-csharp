using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestBuiltins : BaseEParserTest
	{

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
		public void testDictLength()
		{
			compareResourceEOE("builtins/dictLength.pec");
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
		public void testListLength()
		{
			compareResourceEOE("builtins/listLength.pec");
		}

		[Test]
		public void testSetLength()
		{
			compareResourceEOE("builtins/setLength.pec");
		}

		[Test]
		public void testTextLength()
		{
			compareResourceEOE("builtins/textLength.pec");
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
		public void testTupleLength()
		{
			compareResourceEOE("builtins/tupleLength.pec");
		}

	}
}

