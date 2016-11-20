using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestBuiltins : BaseEParserTest
	{

		[Test]
		public void testCharCodePoint()
		{
			compareResourceEME("builtins/charCodePoint.pec");
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
		public void testDictValues()
		{
			compareResourceEME("builtins/dictValues.pec");
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
		public void testListCount()
		{
			compareResourceEME("builtins/listCount.pec");
		}

		[Test]
		public void testSetCount()
		{
			compareResourceEME("builtins/setCount.pec");
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
		public void testTextLowercase()
		{
			compareResourceEME("builtins/textLowercase.pec");
		}

		[Test]
		public void testTextSplit()
		{
			compareResourceEME("builtins/textSplit.pec");
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
		public void testTupleCount()
		{
			compareResourceEME("builtins/tupleCount.pec");
		}

	}
}

