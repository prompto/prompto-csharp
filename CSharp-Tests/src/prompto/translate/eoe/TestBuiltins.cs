using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestBuiltins : BaseEParserTest
	{

		[Test]
		public void testBooleanText()
		{
			compareResourceEOE("builtins/booleanText.pec");
		}

		[Test]
		public void testCategoryText()
		{
			compareResourceEOE("builtins/categoryText.pec");
		}

		[Test]
		public void testCharCodePoint()
		{
			compareResourceEOE("builtins/charCodePoint.pec");
		}

		[Test]
		public void testCharText()
		{
			compareResourceEOE("builtins/charText.pec");
		}

		[Test]
		public void testCursorToList()
		{
			compareResourceEOE("builtins/cursorToList.pec");
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
		public void testDateText()
		{
			compareResourceEOE("builtins/dateText.pec");
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
		public void testDateTimeMilli()
		{
			compareResourceEOE("builtins/dateTimeMilli.pec");
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
		public void testDateTimeText()
		{
			compareResourceEOE("builtins/dateTimeText.pec");
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
		public void testDecimalText()
		{
			compareResourceEOE("builtins/decimalText.pec");
		}

		[Test]
		public void testDictCount()
		{
			compareResourceEOE("builtins/dictCount.pec");
		}

		[Test]
		public void testDictKeys()
		{
			compareResourceEOE("builtins/dictKeys.pec");
		}

		[Test]
		public void testDictText()
		{
			compareResourceEOE("builtins/dictText.pec");
		}

		[Test]
		public void testDictValues()
		{
			compareResourceEOE("builtins/dictValues.pec");
		}

		[Test]
		public void testDocumentText()
		{
			compareResourceEOE("builtins/documentText.pec");
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
		public void testIntegerFormat()
		{
			compareResourceEOE("builtins/integerFormat.pec");
		}

		[Test]
		public void testIntegerText()
		{
			compareResourceEOE("builtins/integerText.pec");
		}

		[Test]
		public void testListCount()
		{
			compareResourceEOE("builtins/listCount.pec");
		}

		[Test]
		public void testListText()
		{
			compareResourceEOE("builtins/listText.pec");
		}

		[Test]
		public void testPeriodText()
		{
			compareResourceEOE("builtins/periodText.pec");
		}

		[Test]
		public void testSetCount()
		{
			compareResourceEOE("builtins/setCount.pec");
		}

		[Test]
		public void testSetText()
		{
			compareResourceEOE("builtins/setText.pec");
		}

		[Test]
		public void testTextCapitalize()
		{
			compareResourceEOE("builtins/textCapitalize.pec");
		}

		[Test]
		public void testTextCount()
		{
			compareResourceEOE("builtins/textCount.pec");
		}

		[Test]
		public void testTextEndsWith()
		{
			compareResourceEOE("builtins/textEndsWith.pec");
		}

		[Test]
		public void testTextLowercase()
		{
			compareResourceEOE("builtins/textLowercase.pec");
		}

		[Test]
		public void testTextReplace()
		{
			compareResourceEOE("builtins/textReplace.pec");
		}

		[Test]
		public void testTextReplaceAll()
		{
			compareResourceEOE("builtins/textReplaceAll.pec");
		}

		[Test]
		public void testTextSplit()
		{
			compareResourceEOE("builtins/textSplit.pec");
		}

		[Test]
		public void testTextStartsWith()
		{
			compareResourceEOE("builtins/textStartsWith.pec");
		}

		[Test]
		public void testTextText()
		{
			compareResourceEOE("builtins/textText.pec");
		}

		[Test]
		public void testTextTrim()
		{
			compareResourceEOE("builtins/textTrim.pec");
		}

		[Test]
		public void testTextUppercase()
		{
			compareResourceEOE("builtins/textUppercase.pec");
		}

		[Test]
		public void testTimeHour()
		{
			compareResourceEOE("builtins/timeHour.pec");
		}

		[Test]
		public void testTimeMilli()
		{
			compareResourceEOE("builtins/timeMilli.pec");
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
		public void testTimeText()
		{
			compareResourceEOE("builtins/timeText.pec");
		}

		[Test]
		public void testTupleCount()
		{
			compareResourceEOE("builtins/tupleCount.pec");
		}

		[Test]
		public void testTupleText()
		{
			compareResourceEOE("builtins/tupleText.pec");
		}

		[Test]
		public void testUuidText()
		{
			compareResourceEOE("builtins/uuidText.pec");
		}

	}
}

