using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestBuiltins : BaseOParserTest
	{

		[Test]
		public void testDateDayOfMonth()
		{
			compareResourceOEO("builtins/dateDayOfMonth.o");
		}

		[Test]
		public void testDateDayOfYear()
		{
			compareResourceOEO("builtins/dateDayOfYear.o");
		}

		[Test]
		public void testDateMonth()
		{
			compareResourceOEO("builtins/dateMonth.o");
		}

		[Test]
		public void testDateTimeDayOfMonth()
		{
			compareResourceOEO("builtins/dateTimeDayOfMonth.o");
		}

		[Test]
		public void testDateTimeDayOfYear()
		{
			compareResourceOEO("builtins/dateTimeDayOfYear.o");
		}

		[Test]
		public void testDateTimeHour()
		{
			compareResourceOEO("builtins/dateTimeHour.o");
		}

		[Test]
		public void testDateTimeMinute()
		{
			compareResourceOEO("builtins/dateTimeMinute.o");
		}

		[Test]
		public void testDateTimeMonth()
		{
			compareResourceOEO("builtins/dateTimeMonth.o");
		}

		[Test]
		public void testDateTimeSecond()
		{
			compareResourceOEO("builtins/dateTimeSecond.o");
		}

		[Test]
		public void testDateTimeTZName()
		{
			compareResourceOEO("builtins/dateTimeTZName.o");
		}

		[Test]
		public void testDateTimeTZOffset()
		{
			compareResourceOEO("builtins/dateTimeTZOffset.o");
		}

		[Test]
		public void testDateTimeYear()
		{
			compareResourceOEO("builtins/dateTimeYear.o");
		}

		[Test]
		public void testDateYear()
		{
			compareResourceOEO("builtins/dateYear.o");
		}

		[Test]
		public void testDictLength()
		{
			compareResourceOEO("builtins/dictLength.o");
		}

		[Test]
		public void testEnumName()
		{
			compareResourceOEO("builtins/enumName.o");
		}

		[Test]
		public void testEnumSymbols()
		{
			compareResourceOEO("builtins/enumSymbols.o");
		}

		[Test]
		public void testEnumValue()
		{
			compareResourceOEO("builtins/enumValue.o");
		}

		[Test]
		public void testListLength()
		{
			compareResourceOEO("builtins/listLength.o");
		}

		[Test]
		public void testSetLength()
		{
			compareResourceOEO("builtins/setLength.o");
		}

		[Test]
		public void testTextLength()
		{
			compareResourceOEO("builtins/textLength.o");
		}

		[Test]
		public void testTimeHour()
		{
			compareResourceOEO("builtins/timeHour.o");
		}

		[Test]
		public void testTimeMinute()
		{
			compareResourceOEO("builtins/timeMinute.o");
		}

		[Test]
		public void testTimeSecond()
		{
			compareResourceOEO("builtins/timeSecond.o");
		}

		[Test]
		public void testTupleLength()
		{
			compareResourceOEO("builtins/tupleLength.o");
		}

	}
}

