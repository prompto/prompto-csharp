using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestBuiltins : BaseOParserTest
	{

		[Test]
		public void testDateDayOfMonth()
		{
			compareResourceOMO("builtins/dateDayOfMonth.poc");
		}

		[Test]
		public void testDateDayOfYear()
		{
			compareResourceOMO("builtins/dateDayOfYear.poc");
		}

		[Test]
		public void testDateMonth()
		{
			compareResourceOMO("builtins/dateMonth.poc");
		}

		[Test]
		public void testDateTimeDayOfMonth()
		{
			compareResourceOMO("builtins/dateTimeDayOfMonth.poc");
		}

		[Test]
		public void testDateTimeDayOfYear()
		{
			compareResourceOMO("builtins/dateTimeDayOfYear.poc");
		}

		[Test]
		public void testDateTimeHour()
		{
			compareResourceOMO("builtins/dateTimeHour.poc");
		}

		[Test]
		public void testDateTimeMinute()
		{
			compareResourceOMO("builtins/dateTimeMinute.poc");
		}

		[Test]
		public void testDateTimeMonth()
		{
			compareResourceOMO("builtins/dateTimeMonth.poc");
		}

		[Test]
		public void testDateTimeSecond()
		{
			compareResourceOMO("builtins/dateTimeSecond.poc");
		}

		[Test]
		public void testDateTimeTZName()
		{
			compareResourceOMO("builtins/dateTimeTZName.poc");
		}

		[Test]
		public void testDateTimeTZOffset()
		{
			compareResourceOMO("builtins/dateTimeTZOffset.poc");
		}

		[Test]
		public void testDateTimeYear()
		{
			compareResourceOMO("builtins/dateTimeYear.poc");
		}

		[Test]
		public void testDateYear()
		{
			compareResourceOMO("builtins/dateYear.poc");
		}

		[Test]
		public void testDictCount()
		{
			compareResourceOMO("builtins/dictCount.poc");
		}

		[Test]
		public void testDictSwap()
		{
			compareResourceOMO("builtins/dictSwap.poc");
		}

		[Test]
		public void testDocumentCount()
		{
			compareResourceOMO("builtins/documentCount.poc");
		}

		[Test]
		public void testEnumName()
		{
			compareResourceOMO("builtins/enumName.poc");
		}

		[Test]
		public void testEnumSymbols()
		{
			compareResourceOMO("builtins/enumSymbols.poc");
		}

		[Test]
		public void testEnumValue()
		{
			compareResourceOMO("builtins/enumValue.poc");
		}

		[Test]
		public void testIntegerFormat()
		{
			compareResourceOMO("builtins/integerFormat.poc");
		}

		[Test]
		public void testListCount()
		{
			compareResourceOMO("builtins/listCount.poc");
		}

		[Test]
		public void testListIndexOf()
		{
			compareResourceOMO("builtins/listIndexOf.poc");
		}

		[Test]
		public void testListJoin()
		{
			compareResourceOMO("builtins/listJoin.poc");
		}

		[Test]
		public void testPeriodDays()
		{
			compareResourceOMO("builtins/periodDays.poc");
		}

		[Test]
		public void testPeriodHours()
		{
			compareResourceOMO("builtins/periodHours.poc");
		}

		[Test]
		public void testPeriodMillis()
		{
			compareResourceOMO("builtins/periodMillis.poc");
		}

		[Test]
		public void testPeriodMinutes()
		{
			compareResourceOMO("builtins/periodMinutes.poc");
		}

		[Test]
		public void testPeriodMonths()
		{
			compareResourceOMO("builtins/periodMonths.poc");
		}

		[Test]
		public void testPeriodSeconds()
		{
			compareResourceOMO("builtins/periodSeconds.poc");
		}

		[Test]
		public void testPeriodWeeks()
		{
			compareResourceOMO("builtins/periodWeeks.poc");
		}

		[Test]
		public void testPeriodYears()
		{
			compareResourceOMO("builtins/periodYears.poc");
		}

		[Test]
		public void testSetCount()
		{
			compareResourceOMO("builtins/setCount.poc");
		}

		[Test]
		public void testSetJoin()
		{
			compareResourceOMO("builtins/setJoin.poc");
		}

		[Test]
		public void testTextCapitalize()
		{
			compareResourceOMO("builtins/textCapitalize.poc");
		}

		[Test]
		public void testTextCount()
		{
			compareResourceOMO("builtins/textCount.poc");
		}

		[Test]
		public void testTextIndexOf()
		{
			compareResourceOMO("builtins/textIndexOf.poc");
		}

		[Test]
		public void testTextLowercase()
		{
			compareResourceOMO("builtins/textLowercase.poc");
		}

		[Test]
		public void testTextReplace()
		{
			compareResourceOMO("builtins/textReplace.poc");
		}

		[Test]
		public void testTextReplaceAll()
		{
			compareResourceOMO("builtins/textReplaceAll.poc");
		}

		[Test]
		public void testTextSplit()
		{
			compareResourceOMO("builtins/textSplit.poc");
		}

		[Test]
		public void testTextTrim()
		{
			compareResourceOMO("builtins/textTrim.poc");
		}

		[Test]
		public void testTextUppercase()
		{
			compareResourceOMO("builtins/textUppercase.poc");
		}

		[Test]
		public void testTimeHour()
		{
			compareResourceOMO("builtins/timeHour.poc");
		}

		[Test]
		public void testTimeMinute()
		{
			compareResourceOMO("builtins/timeMinute.poc");
		}

		[Test]
		public void testTimeSecond()
		{
			compareResourceOMO("builtins/timeSecond.poc");
		}

		[Test]
		public void testTupleCount()
		{
			compareResourceOMO("builtins/tupleCount.poc");
		}

		[Test]
		public void testTupleJoin()
		{
			compareResourceOMO("builtins/tupleJoin.poc");
		}

	}
}

