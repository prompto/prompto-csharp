using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestBuiltins : BaseEParserTest
	{

		[Test]
		public void testBooleanText()
		{
			compareResourceEME("builtins/booleanText.pec");
		}

		[Test]
		public void testCategoryText()
		{
			compareResourceEME("builtins/categoryText.pec");
		}

		[Test]
		public void testCharCodePoint()
		{
			compareResourceEME("builtins/charCodePoint.pec");
		}

		[Test]
		public void testCharText()
		{
			compareResourceEME("builtins/charText.pec");
		}

		[Test]
		public void testCursorToList()
		{
			compareResourceEME("builtins/cursorToList.pec");
		}

		[Test]
		public void testDateDayOfMonth()
		{
			compareResourceEME("builtins/dateDayOfMonth.pec");
		}

		[Test]
		public void testDateDayOfYear()
		{
			compareResourceEME("builtins/dateDayOfYear.pec");
		}

		[Test]
		public void testDateMonth()
		{
			compareResourceEME("builtins/dateMonth.pec");
		}

		[Test]
		public void testDateText()
		{
			compareResourceEME("builtins/dateText.pec");
		}

		[Test]
		public void testDateTimeDate()
		{
			compareResourceEME("builtins/dateTimeDate.pec");
		}

		[Test]
		public void testDateTimeDayOfMonth()
		{
			compareResourceEME("builtins/dateTimeDayOfMonth.pec");
		}

		[Test]
		public void testDateTimeDayOfYear()
		{
			compareResourceEME("builtins/dateTimeDayOfYear.pec");
		}

		[Test]
		public void testDateTimeHour()
		{
			compareResourceEME("builtins/dateTimeHour.pec");
		}

		[Test]
		public void testDateTimeMilli()
		{
			compareResourceEME("builtins/dateTimeMilli.pec");
		}

		[Test]
		public void testDateTimeMinute()
		{
			compareResourceEME("builtins/dateTimeMinute.pec");
		}

		[Test]
		public void testDateTimeMonth()
		{
			compareResourceEME("builtins/dateTimeMonth.pec");
		}

		[Test]
		public void testDateTimeSecond()
		{
			compareResourceEME("builtins/dateTimeSecond.pec");
		}

		[Test]
		public void testDateTimeTZName()
		{
			compareResourceEME("builtins/dateTimeTZName.pec");
		}

		[Test]
		public void testDateTimeTZOffset()
		{
			compareResourceEME("builtins/dateTimeTZOffset.pec");
		}

		[Test]
		public void testDateTimeText()
		{
			compareResourceEME("builtins/dateTimeText.pec");
		}

		[Test]
		public void testDateTimeTime()
		{
			compareResourceEME("builtins/dateTimeTime.pec");
		}

		[Test]
		public void testDateTimeYear()
		{
			compareResourceEME("builtins/dateTimeYear.pec");
		}

		[Test]
		public void testDateYear()
		{
			compareResourceEME("builtins/dateYear.pec");
		}

		[Test]
		public void testDecimalText()
		{
			compareResourceEME("builtins/decimalText.pec");
		}

		[Test]
		public void testDictCount()
		{
			compareResourceEME("builtins/dictCount.pec");
		}

		[Test]
		public void testDictKeys()
		{
			compareResourceEME("builtins/dictKeys.pec");
		}

		[Test]
		public void testDictText()
		{
			compareResourceEME("builtins/dictText.pec");
		}

		[Test]
		public void testDictValues()
		{
			compareResourceEME("builtins/dictValues.pec");
		}

		[Test]
		public void testDocumentText()
		{
			compareResourceEME("builtins/documentText.pec");
		}

		[Test]
		public void testEnumName()
		{
			compareResourceEME("builtins/enumName.pec");
		}

		[Test]
		public void testEnumSymbols()
		{
			compareResourceEME("builtins/enumSymbols.pec");
		}

		[Test]
		public void testEnumValue()
		{
			compareResourceEME("builtins/enumValue.pec");
		}

		[Test]
		public void testIntegerFormat()
		{
			compareResourceEME("builtins/integerFormat.pec");
		}

		[Test]
		public void testIntegerText()
		{
			compareResourceEME("builtins/integerText.pec");
		}

		[Test]
		public void testListCount()
		{
			compareResourceEME("builtins/listCount.pec");
		}

		[Test]
		public void testListText()
		{
			compareResourceEME("builtins/listText.pec");
		}

		[Test]
		public void testPeriodText()
		{
			compareResourceEME("builtins/periodText.pec");
		}

		[Test]
		public void testSetCount()
		{
			compareResourceEME("builtins/setCount.pec");
		}

		[Test]
		public void testSetText()
		{
			compareResourceEME("builtins/setText.pec");
		}

		[Test]
		public void testTextCapitalize()
		{
			compareResourceEME("builtins/textCapitalize.pec");
		}

		[Test]
		public void testTextCount()
		{
			compareResourceEME("builtins/textCount.pec");
		}

		[Test]
		public void testTextEndsWith()
		{
			compareResourceEME("builtins/textEndsWith.pec");
		}

		[Test]
		public void testTextLowercase()
		{
			compareResourceEME("builtins/textLowercase.pec");
		}

		[Test]
		public void testTextReplace()
		{
			compareResourceEME("builtins/textReplace.pec");
		}

		[Test]
		public void testTextReplaceAll()
		{
			compareResourceEME("builtins/textReplaceAll.pec");
		}

		[Test]
		public void testTextSplit()
		{
			compareResourceEME("builtins/textSplit.pec");
		}

		[Test]
		public void testTextStartsWith()
		{
			compareResourceEME("builtins/textStartsWith.pec");
		}

		[Test]
		public void testTextText()
		{
			compareResourceEME("builtins/textText.pec");
		}

		[Test]
		public void testTextTrim()
		{
			compareResourceEME("builtins/textTrim.pec");
		}

		[Test]
		public void testTextUppercase()
		{
			compareResourceEME("builtins/textUppercase.pec");
		}

		[Test]
		public void testTimeHour()
		{
			compareResourceEME("builtins/timeHour.pec");
		}

		[Test]
		public void testTimeMilli()
		{
			compareResourceEME("builtins/timeMilli.pec");
		}

		[Test]
		public void testTimeMinute()
		{
			compareResourceEME("builtins/timeMinute.pec");
		}

		[Test]
		public void testTimeSecond()
		{
			compareResourceEME("builtins/timeSecond.pec");
		}

		[Test]
		public void testTimeText()
		{
			compareResourceEME("builtins/timeText.pec");
		}

		[Test]
		public void testTupleCount()
		{
			compareResourceEME("builtins/tupleCount.pec");
		}

		[Test]
		public void testTupleText()
		{
			compareResourceEME("builtins/tupleText.pec");
		}

		[Test]
		public void testUuidText()
		{
			compareResourceEME("builtins/uuidText.pec");
		}

	}
}

