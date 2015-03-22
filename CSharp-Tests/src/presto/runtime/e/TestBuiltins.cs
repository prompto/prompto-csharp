using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
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
		public void testDateDayOfMonth()
		{
			CheckOutput("builtins/dateDayOfMonth.e");
		}

		[Test]
		public void testDateDayOfYear()
		{
			CheckOutput("builtins/dateDayOfYear.e");
		}

		[Test]
		public void testDateMonth()
		{
			CheckOutput("builtins/dateMonth.e");
		}

		[Test]
		public void testDateTimeDayOfMonth()
		{
			CheckOutput("builtins/dateTimeDayOfMonth.e");
		}

		[Test]
		public void testDateTimeDayOfYear()
		{
			CheckOutput("builtins/dateTimeDayOfYear.e");
		}

		[Test]
		public void testDateTimeHour()
		{
			CheckOutput("builtins/dateTimeHour.e");
		}

		[Test]
		public void testDateTimeMinute()
		{
			CheckOutput("builtins/dateTimeMinute.e");
		}

		[Test]
		public void testDateTimeMonth()
		{
			CheckOutput("builtins/dateTimeMonth.e");
		}

		[Test]
		public void testDateTimeSecond()
		{
			CheckOutput("builtins/dateTimeSecond.e");
		}

		[Test]
		public void testDateTimeTZOffset()
		{
			CheckOutput("builtins/dateTimeTZOffset.e");
		}

		[Test]
		public void testDateTimeYear()
		{
			CheckOutput("builtins/dateTimeYear.e");
		}

		[Test]
		public void testDateYear()
		{
			CheckOutput("builtins/dateYear.e");
		}

		[Test]
		public void testDictLength()
		{
			CheckOutput("builtins/dictLength.e");
		}

		[Test]
		public void testEnumName()
		{
			CheckOutput("builtins/enumName.e");
		}

		[Test]
		public void testEnumSymbols()
		{
			CheckOutput("builtins/enumSymbols.e");
		}

		[Test]
		public void testEnumValue()
		{
			CheckOutput("builtins/enumValue.e");
		}

		[Test]
		public void testListLength()
		{
			CheckOutput("builtins/listLength.e");
		}

		[Test]
		public void testSetLength()
		{
			CheckOutput("builtins/setLength.e");
		}

		[Test]
		public void testTextLength()
		{
			CheckOutput("builtins/textLength.e");
		}

		[Test]
		public void testTimeHour()
		{
			CheckOutput("builtins/timeHour.e");
		}

		[Test]
		public void testTimeMinute()
		{
			CheckOutput("builtins/timeMinute.e");
		}

		[Test]
		public void testTimeSecond()
		{
			CheckOutput("builtins/timeSecond.e");
		}

		[Test]
		public void testTupleLength()
		{
			CheckOutput("builtins/tupleLength.e");
		}

	}
}

