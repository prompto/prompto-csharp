using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
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
			CheckOutput("builtins/dateDayOfMonth.o");
		}

		[Test]
		public void testDateDayOfYear()
		{
			CheckOutput("builtins/dateDayOfYear.o");
		}

		[Test]
		public void testDateMonth()
		{
			CheckOutput("builtins/dateMonth.o");
		}

		[Test]
		public void testDateTimeDayOfMonth()
		{
			CheckOutput("builtins/dateTimeDayOfMonth.o");
		}

		[Test]
		public void testDateTimeDayOfYear()
		{
			CheckOutput("builtins/dateTimeDayOfYear.o");
		}

		[Test]
		public void testDateTimeHour()
		{
			CheckOutput("builtins/dateTimeHour.o");
		}

		[Test]
		public void testDateTimeMinute()
		{
			CheckOutput("builtins/dateTimeMinute.o");
		}

		[Test]
		public void testDateTimeMonth()
		{
			CheckOutput("builtins/dateTimeMonth.o");
		}

		[Test]
		public void testDateTimeSecond()
		{
			CheckOutput("builtins/dateTimeSecond.o");
		}

		[Test]
		public void testDateTimeTZOffset()
		{
			CheckOutput("builtins/dateTimeTZOffset.o");
		}

		[Test]
		public void testDateTimeYear()
		{
			CheckOutput("builtins/dateTimeYear.o");
		}

		[Test]
		public void testDateYear()
		{
			CheckOutput("builtins/dateYear.o");
		}

		[Test]
		public void testDictLength()
		{
			CheckOutput("builtins/dictLength.o");
		}

		[Test]
		public void testEnumName()
		{
			CheckOutput("builtins/enumName.o");
		}

		[Test]
		public void testEnumSymbols()
		{
			CheckOutput("builtins/enumSymbols.o");
		}

		[Test]
		public void testEnumValue()
		{
			CheckOutput("builtins/enumValue.o");
		}

		[Test]
		public void testListLength()
		{
			CheckOutput("builtins/listLength.o");
		}

		[Test]
		public void testSetLength()
		{
			CheckOutput("builtins/setLength.o");
		}

		[Test]
		public void testTextLength()
		{
			CheckOutput("builtins/textLength.o");
		}

		[Test]
		public void testTimeHour()
		{
			CheckOutput("builtins/timeHour.o");
		}

		[Test]
		public void testTimeMinute()
		{
			CheckOutput("builtins/timeMinute.o");
		}

		[Test]
		public void testTimeSecond()
		{
			CheckOutput("builtins/timeSecond.o");
		}

		[Test]
		public void testTupleLength()
		{
			CheckOutput("builtins/tupleLength.o");
		}

	}
}

