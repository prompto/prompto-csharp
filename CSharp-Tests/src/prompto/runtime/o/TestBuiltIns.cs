using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestBuiltins : BaseOParserTest
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
		public void testDateDayOfMonth()
		{
			CheckOutput("builtins/dateDayOfMonth.poc");
		}

		[Test]
		public void testDateDayOfYear()
		{
			CheckOutput("builtins/dateDayOfYear.poc");
		}

		[Test]
		public void testDateMonth()
		{
			CheckOutput("builtins/dateMonth.poc");
		}

		[Test]
		public void testDateTimeDayOfMonth()
		{
			CheckOutput("builtins/dateTimeDayOfMonth.poc");
		}

		[Test]
		public void testDateTimeDayOfYear()
		{
			CheckOutput("builtins/dateTimeDayOfYear.poc");
		}

		[Test]
		public void testDateTimeHour()
		{
			CheckOutput("builtins/dateTimeHour.poc");
		}

		[Test]
		public void testDateTimeMinute()
		{
			CheckOutput("builtins/dateTimeMinute.poc");
		}

		[Test]
		public void testDateTimeMonth()
		{
			CheckOutput("builtins/dateTimeMonth.poc");
		}

		[Test]
		public void testDateTimeSecond()
		{
			CheckOutput("builtins/dateTimeSecond.poc");
		}

		[Test]
		public void testDateTimeTZOffset()
		{
			CheckOutput("builtins/dateTimeTZOffset.poc");
		}

		[Test]
		public void testDateTimeYear()
		{
			CheckOutput("builtins/dateTimeYear.poc");
		}

		[Test]
		public void testDateYear()
		{
			CheckOutput("builtins/dateYear.poc");
		}

		[Test]
		public void testDictLength()
		{
			CheckOutput("builtins/dictLength.poc");
		}

		[Test]
		public void testEnumName()
		{
			CheckOutput("builtins/enumName.poc");
		}

		[Test]
		public void testEnumSymbols()
		{
			CheckOutput("builtins/enumSymbols.poc");
		}

		[Test]
		public void testEnumValue()
		{
			CheckOutput("builtins/enumValue.poc");
		}

		[Test]
		public void testListLength()
		{
			CheckOutput("builtins/listLength.poc");
		}

		[Test]
		public void testSetLength()
		{
			CheckOutput("builtins/setLength.poc");
		}

		[Test]
		public void testTextLength()
		{
			CheckOutput("builtins/textLength.poc");
		}

		[Test]
		public void testTimeHour()
		{
			CheckOutput("builtins/timeHour.poc");
		}

		[Test]
		public void testTimeMinute()
		{
			CheckOutput("builtins/timeMinute.poc");
		}

		[Test]
		public void testTimeSecond()
		{
			CheckOutput("builtins/timeSecond.poc");
		}

		[Test]
		public void testTupleLength()
		{
			CheckOutput("builtins/tupleLength.poc");
		}

	}
}

