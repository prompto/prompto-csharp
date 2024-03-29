using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestBuiltins : BaseEParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testBooleanJson()
		{
			CheckOutput("builtins/booleanJson.pec");
		}

		[Test]
		public void testBooleanText()
		{
			CheckOutput("builtins/booleanText.pec");
		}

		[Test]
		public void testCategoryCategory()
		{
			CheckOutput("builtins/categoryCategory.pec");
		}

		[Test]
		public void testCategoryJson()
		{
			CheckOutput("builtins/categoryJson.pec");
		}

		[Test]
		public void testCategoryText()
		{
			CheckOutput("builtins/categoryText.pec");
		}

		[Test]
		public void testCharCodePoint()
		{
			CheckOutput("builtins/charCodePoint.pec");
		}

		[Test]
		public void testCharJson()
		{
			CheckOutput("builtins/charJson.pec");
		}

		[Test]
		public void testCharText()
		{
			CheckOutput("builtins/charText.pec");
		}

		[Test]
		public void testCursorToList()
		{
			CheckOutput("builtins/cursorToList.pec");
		}

		[Test]
		public void testDateDayOfMonth()
		{
			CheckOutput("builtins/dateDayOfMonth.pec");
		}

		[Test]
		public void testDateDayOfYear()
		{
			CheckOutput("builtins/dateDayOfYear.pec");
		}

		[Test]
		public void testDateJson()
		{
			CheckOutput("builtins/dateJson.pec");
		}

		[Test]
		public void testDateMonth()
		{
			CheckOutput("builtins/dateMonth.pec");
		}

		[Test]
		public void testDateText()
		{
			CheckOutput("builtins/dateText.pec");
		}

		[Test]
		public void testDateTimeDate()
		{
			CheckOutput("builtins/dateTimeDate.pec");
		}

		[Test]
		public void testDateTimeDayOfMonth()
		{
			CheckOutput("builtins/dateTimeDayOfMonth.pec");
		}

		[Test]
		public void testDateTimeDayOfYear()
		{
			CheckOutput("builtins/dateTimeDayOfYear.pec");
		}

		[Test]
		public void testDateTimeHour()
		{
			CheckOutput("builtins/dateTimeHour.pec");
		}

		[Test]
		public void testDateTimeJson()
		{
			CheckOutput("builtins/dateTimeJson.pec");
		}

		[Test]
		public void testDateTimeMilli()
		{
			CheckOutput("builtins/dateTimeMilli.pec");
		}

		[Test]
		public void testDateTimeMinute()
		{
			CheckOutput("builtins/dateTimeMinute.pec");
		}

		[Test]
		public void testDateTimeMonth()
		{
			CheckOutput("builtins/dateTimeMonth.pec");
		}

		[Test]
		public void testDateTimeSecond()
		{
			CheckOutput("builtins/dateTimeSecond.pec");
		}

		[Test]
		public void testDateTimeText()
		{
			CheckOutput("builtins/dateTimeText.pec");
		}

		[Test]
		public void testDateTimeTime()
		{
			CheckOutput("builtins/dateTimeTime.pec");
		}

		[Test]
		public void testDateTimeYear()
		{
			CheckOutput("builtins/dateTimeYear.pec");
		}

		[Test]
		public void testDateYear()
		{
			CheckOutput("builtins/dateYear.pec");
		}

		[Test]
		public void testDecimalJson()
		{
			CheckOutput("builtins/decimalJson.pec");
		}

		[Test]
		public void testDecimalText()
		{
			CheckOutput("builtins/decimalText.pec");
		}

		[Test]
		public void testDictCount()
		{
			CheckOutput("builtins/dictCount.pec");
		}

		[Test]
		public void testDictJson()
		{
			CheckOutput("builtins/dictJson.pec");
		}

		[Test]
		public void testDictKeys()
		{
			CheckOutput("builtins/dictKeys.pec");
		}

		[Test]
		public void testDictText()
		{
			CheckOutput("builtins/dictText.pec");
		}

		[Test]
		public void testDictValues()
		{
			CheckOutput("builtins/dictValues.pec");
		}

		[Test]
		public void testDocumentCount()
		{
			CheckOutput("builtins/documentCount.pec");
		}

		[Test]
		public void testDocumentJson()
		{
			CheckOutput("builtins/documentJson.pec");
		}

		[Test]
		public void testDocumentKeys()
		{
			CheckOutput("builtins/documentKeys.pec");
		}

		[Test]
		public void testDocumentText()
		{
			CheckOutput("builtins/documentText.pec");
		}

		[Test]
		public void testDocumentValues()
		{
			CheckOutput("builtins/documentValues.pec");
		}

		[Test]
		public void testEnumName()
		{
			CheckOutput("builtins/enumName.pec");
		}

		[Test]
		public void testEnumSymbols()
		{
			CheckOutput("builtins/enumSymbols.pec");
		}

		[Test]
		public void testEnumValue()
		{
			CheckOutput("builtins/enumValue.pec");
		}

		[Test]
		public void testIntegerFormat()
		{
			CheckOutput("builtins/integerFormat.pec");
		}

		[Test]
		public void testIntegerJson()
		{
			CheckOutput("builtins/integerJson.pec");
		}

		[Test]
		public void testIntegerText()
		{
			CheckOutput("builtins/integerText.pec");
		}

		[Test]
		public void testIteratorToList()
		{
			CheckOutput("builtins/iteratorToList.pec");
		}

		[Test]
		public void testIteratorToSet()
		{
			CheckOutput("builtins/iteratorToSet.pec");
		}

		[Test]
		public void testListCount()
		{
			CheckOutput("builtins/listCount.pec");
		}

		[Test]
		public void testListIndexOf()
		{
			CheckOutput("builtins/listIndexOf.pec");
		}

		[Test]
		public void testListJson()
		{
			CheckOutput("builtins/listJson.pec");
		}

		[Test]
		public void testListText()
		{
			CheckOutput("builtins/listText.pec");
		}

		[Test]
		public void testListToSet()
		{
			CheckOutput("builtins/listToSet.pec");
		}

		[Test]
		public void testPeriodDays()
		{
			CheckOutput("builtins/periodDays.pec");
		}

		[Test]
		public void testPeriodHours()
		{
			CheckOutput("builtins/periodHours.pec");
		}

		[Test]
		public void testPeriodJson()
		{
			CheckOutput("builtins/periodJson.pec");
		}

		[Test]
		public void testPeriodMillis()
		{
			CheckOutput("builtins/periodMillis.pec");
		}

		[Test]
		public void testPeriodMinutes()
		{
			CheckOutput("builtins/periodMinutes.pec");
		}

		[Test]
		public void testPeriodMonths()
		{
			CheckOutput("builtins/periodMonths.pec");
		}

		[Test]
		public void testPeriodSeconds()
		{
			CheckOutput("builtins/periodSeconds.pec");
		}

		[Test]
		public void testPeriodText()
		{
			CheckOutput("builtins/periodText.pec");
		}

		[Test]
		public void testPeriodWeeks()
		{
			CheckOutput("builtins/periodWeeks.pec");
		}

		[Test]
		public void testPeriodYears()
		{
			CheckOutput("builtins/periodYears.pec");
		}

		[Test]
		public void testSetCount()
		{
			CheckOutput("builtins/setCount.pec");
		}

		[Test]
		public void testSetJson()
		{
			CheckOutput("builtins/setJson.pec");
		}

		[Test]
		public void testSetText()
		{
			CheckOutput("builtins/setText.pec");
		}

		[Test]
		public void testSetToList()
		{
			CheckOutput("builtins/setToList.pec");
		}

		[Test]
		public void testTextCapitalize()
		{
			CheckOutput("builtins/textCapitalize.pec");
		}

		[Test]
		public void testTextCount()
		{
			CheckOutput("builtins/textCount.pec");
		}

		[Test]
		public void testTextEndsWith()
		{
			CheckOutput("builtins/textEndsWith.pec");
		}

		[Test]
		public void testTextJson()
		{
			CheckOutput("builtins/textJson.pec");
		}

		[Test]
		public void testTextLowercase()
		{
			CheckOutput("builtins/textLowercase.pec");
		}

		[Test]
		public void testTextReplace()
		{
			CheckOutput("builtins/textReplace.pec");
		}

		[Test]
		public void testTextReplaceAll()
		{
			CheckOutput("builtins/textReplaceAll.pec");
		}

		[Test]
		public void testTextSplit()
		{
			CheckOutput("builtins/textSplit.pec");
		}

		[Test]
		public void testTextStartsWith()
		{
			CheckOutput("builtins/textStartsWith.pec");
		}

		[Test]
		public void testTextText()
		{
			CheckOutput("builtins/textText.pec");
		}

		[Test]
		public void testTextTrim()
		{
			CheckOutput("builtins/textTrim.pec");
		}

		[Test]
		public void testTextUppercase()
		{
			CheckOutput("builtins/textUppercase.pec");
		}

		[Test]
		public void testTimeHour()
		{
			CheckOutput("builtins/timeHour.pec");
		}

		[Test]
		public void testTimeJson()
		{
			CheckOutput("builtins/timeJson.pec");
		}

		[Test]
		public void testTimeMilli()
		{
			CheckOutput("builtins/timeMilli.pec");
		}

		[Test]
		public void testTimeMinute()
		{
			CheckOutput("builtins/timeMinute.pec");
		}

		[Test]
		public void testTimeSecond()
		{
			CheckOutput("builtins/timeSecond.pec");
		}

		[Test]
		public void testTimeText()
		{
			CheckOutput("builtins/timeText.pec");
		}

		[Test]
		public void testTupleCount()
		{
			CheckOutput("builtins/tupleCount.pec");
		}

		[Test]
		public void testTupleText()
		{
			CheckOutput("builtins/tupleText.pec");
		}

		[Test]
		public void testUuidJson()
		{
			CheckOutput("builtins/uuidJson.pec");
		}

		[Test]
		public void testUuidText()
		{
			CheckOutput("builtins/uuidText.pec");
		}

		[Test]
		public void testVersionMembers()
		{
			CheckOutput("builtins/versionMembers.pec");
		}

	}
}

