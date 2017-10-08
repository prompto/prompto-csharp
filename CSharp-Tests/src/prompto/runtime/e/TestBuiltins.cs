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
		public void testBooleanText()
		{
			CheckOutput("builtins/booleanText.pec");
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
		public void testCharText()
		{
			CheckOutput("builtins/charText.pec");
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
		public void testDocumentText()
		{
			CheckOutput("builtins/documentText.pec");
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
		public void testIntegerText()
		{
			CheckOutput("builtins/integerText.pec");
		}

		[Test]
		public void testListCount()
		{
			CheckOutput("builtins/listCount.pec");
		}

		[Test]
		public void testListText()
		{
			CheckOutput("builtins/listText.pec");
		}

		[Test]
		public void testPeriodText()
		{
			CheckOutput("builtins/periodText.pec");
		}

		[Test]
		public void testSetCount()
		{
			CheckOutput("builtins/setCount.pec");
		}

		[Test]
		public void testSetText()
		{
			CheckOutput("builtins/setText.pec");
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
		public void testUuidText()
		{
			CheckOutput("builtins/uuidText.pec");
		}

	}
}

