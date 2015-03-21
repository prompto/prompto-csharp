using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestBuiltins : BaseOParserTest
	{

		[Test]
		public void testDateDayOfMonth()
		{
			compareResourceOPO("builtins/dateDayOfMonth.o");
		}

		[Test]
		public void testDateDayOfYear()
		{
			compareResourceOPO("builtins/dateDayOfYear.o");
		}

		[Test]
		public void testDateMonth()
		{
			compareResourceOPO("builtins/dateMonth.o");
		}

		[Test]
		public void testDateTimeDayOfMonth()
		{
			compareResourceOPO("builtins/dateTimeDayOfMonth.o");
		}

		[Test]
		public void testDateTimeDayOfYear()
		{
			compareResourceOPO("builtins/dateTimeDayOfYear.o");
		}

		[Test]
		public void testDateTimeHour()
		{
			compareResourceOPO("builtins/dateTimeHour.o");
		}

		[Test]
		public void testDateTimeMinute()
		{
			compareResourceOPO("builtins/dateTimeMinute.o");
		}

		[Test]
		public void testDateTimeMonth()
		{
			compareResourceOPO("builtins/dateTimeMonth.o");
		}

		[Test]
		public void testDateTimeSecond()
		{
			compareResourceOPO("builtins/dateTimeSecond.o");
		}

		[Test]
		public void testDateTimeTZName()
		{
			compareResourceOPO("builtins/dateTimeTZName.o");
		}

		[Test]
		public void testDateTimeTZOffset()
		{
			compareResourceOPO("builtins/dateTimeTZOffset.o");
		}

		[Test]
		public void testDateTimeYear()
		{
			compareResourceOPO("builtins/dateTimeYear.o");
		}

		[Test]
		public void testDateYear()
		{
			compareResourceOPO("builtins/dateYear.o");
		}

		[Test]
		public void testDictLength()
		{
			compareResourceOPO("builtins/dictLength.o");
		}

		[Test]
		public void testEnumName()
		{
			compareResourceOPO("builtins/enumName.o");
		}

		[Test]
		public void testEnumSymbols()
		{
			compareResourceOPO("builtins/enumSymbols.o");
		}

		[Test]
		public void testEnumValue()
		{
			compareResourceOPO("builtins/enumValue.o");
		}

		[Test]
		public void testListLength()
		{
			compareResourceOPO("builtins/listLength.o");
		}

		[Test]
		public void testSetLength()
		{
			compareResourceOPO("builtins/setLength.o");
		}

		[Test]
		public void testTextLength()
		{
			compareResourceOPO("builtins/textLength.o");
		}

		[Test]
		public void testTimeHour()
		{
			compareResourceOPO("builtins/timeHour.o");
		}

		[Test]
		public void testTimeMinute()
		{
			compareResourceOPO("builtins/timeMinute.o");
		}

		[Test]
		public void testTimeSecond()
		{
			compareResourceOPO("builtins/timeSecond.o");
		}

		[Test]
		public void testTupleLength()
		{
			compareResourceOPO("builtins/tupleLength.o");
		}

	}
}

