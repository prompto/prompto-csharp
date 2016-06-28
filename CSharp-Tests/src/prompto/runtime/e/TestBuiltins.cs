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
		public void testCharCodePoint()
		{
			CheckOutput("builtins/charCodePoint.pec");
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
		public void testDateTimeTZOffset()
		{
			CheckOutput("builtins/dateTimeTZOffset.pec");
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
		public void testDictCount()
		{
			CheckOutput("builtins/dictCount.pec");
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
		public void testListCount()
		{
			CheckOutput("builtins/listCount.pec");
		}

		[Test]
		public void testSetCount()
		{
			CheckOutput("builtins/setCount.pec");
		}

		[Test]
		public void testTextCount()
		{
			CheckOutput("builtins/textCount.pec");
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
		public void testTupleCount()
		{
			CheckOutput("builtins/tupleCount.pec");
		}

	}
}

