using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestBuiltins : BaseEParserTest
	{

		[Test]
		public void testCharCodePoint()
		{
			compareResourceESE("builtins/charCodePoint.pec");
		}

		[Test]
		public void testDateDayOfMonth()
		{
			compareResourceESE("builtins/dateDayOfMonth.pec");
		}

		[Test]
		public void testDateDayOfYear()
		{
			compareResourceESE("builtins/dateDayOfYear.pec");
		}

		[Test]
		public void testDateMonth()
		{
			compareResourceESE("builtins/dateMonth.pec");
		}

		[Test]
		public void testDateTimeDayOfMonth()
		{
			compareResourceESE("builtins/dateTimeDayOfMonth.pec");
		}

		[Test]
		public void testDateTimeDayOfYear()
		{
			compareResourceESE("builtins/dateTimeDayOfYear.pec");
		}

		[Test]
		public void testDateTimeHour()
		{
			compareResourceESE("builtins/dateTimeHour.pec");
		}

		[Test]
		public void testDateTimeMilli()
		{
			compareResourceESE("builtins/dateTimeMilli.pec");
		}

		[Test]
		public void testDateTimeMinute()
		{
			compareResourceESE("builtins/dateTimeMinute.pec");
		}

		[Test]
		public void testDateTimeMonth()
		{
			compareResourceESE("builtins/dateTimeMonth.pec");
		}

		[Test]
		public void testDateTimeSecond()
		{
			compareResourceESE("builtins/dateTimeSecond.pec");
		}

		[Test]
		public void testDateTimeTZName()
		{
			compareResourceESE("builtins/dateTimeTZName.pec");
		}

		[Test]
		public void testDateTimeTZOffset()
		{
			compareResourceESE("builtins/dateTimeTZOffset.pec");
		}

		[Test]
		public void testDateTimeYear()
		{
			compareResourceESE("builtins/dateTimeYear.pec");
		}

		[Test]
		public void testDateYear()
		{
			compareResourceESE("builtins/dateYear.pec");
		}

		[Test]
		public void testDictCount()
		{
			compareResourceESE("builtins/dictCount.pec");
		}

		[Test]
		public void testEnumName()
		{
			compareResourceESE("builtins/enumName.pec");
		}

		[Test]
		public void testEnumSymbols()
		{
			compareResourceESE("builtins/enumSymbols.pec");
		}

		[Test]
		public void testEnumValue()
		{
			compareResourceESE("builtins/enumValue.pec");
		}

		[Test]
		public void testListCount()
		{
			compareResourceESE("builtins/listCount.pec");
		}

		[Test]
		public void testSetCount()
		{
			compareResourceESE("builtins/setCount.pec");
		}

		[Test]
		public void testTextCount()
		{
			compareResourceESE("builtins/textCount.pec");
		}

		[Test]
		public void testTimeHour()
		{
			compareResourceESE("builtins/timeHour.pec");
		}

		[Test]
		public void testTimeMilli()
		{
			compareResourceESE("builtins/timeMilli.pec");
		}

		[Test]
		public void testTimeMinute()
		{
			compareResourceESE("builtins/timeMinute.pec");
		}

		[Test]
		public void testTimeSecond()
		{
			compareResourceESE("builtins/timeSecond.pec");
		}

		[Test]
		public void testTupleCount()
		{
			compareResourceESE("builtins/tupleCount.pec");
		}

	}
}

