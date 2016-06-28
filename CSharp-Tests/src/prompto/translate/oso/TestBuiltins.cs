using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestBuiltins : BaseOParserTest
	{

		[Test]
		public void testDateDayOfMonth()
		{
			compareResourceOSO("builtins/dateDayOfMonth.poc");
		}

		[Test]
		public void testDateDayOfYear()
		{
			compareResourceOSO("builtins/dateDayOfYear.poc");
		}

		[Test]
		public void testDateMonth()
		{
			compareResourceOSO("builtins/dateMonth.poc");
		}

		[Test]
		public void testDateTimeDayOfMonth()
		{
			compareResourceOSO("builtins/dateTimeDayOfMonth.poc");
		}

		[Test]
		public void testDateTimeDayOfYear()
		{
			compareResourceOSO("builtins/dateTimeDayOfYear.poc");
		}

		[Test]
		public void testDateTimeHour()
		{
			compareResourceOSO("builtins/dateTimeHour.poc");
		}

		[Test]
		public void testDateTimeMinute()
		{
			compareResourceOSO("builtins/dateTimeMinute.poc");
		}

		[Test]
		public void testDateTimeMonth()
		{
			compareResourceOSO("builtins/dateTimeMonth.poc");
		}

		[Test]
		public void testDateTimeSecond()
		{
			compareResourceOSO("builtins/dateTimeSecond.poc");
		}

		[Test]
		public void testDateTimeTZName()
		{
			compareResourceOSO("builtins/dateTimeTZName.poc");
		}

		[Test]
		public void testDateTimeTZOffset()
		{
			compareResourceOSO("builtins/dateTimeTZOffset.poc");
		}

		[Test]
		public void testDateTimeYear()
		{
			compareResourceOSO("builtins/dateTimeYear.poc");
		}

		[Test]
		public void testDateYear()
		{
			compareResourceOSO("builtins/dateYear.poc");
		}

		[Test]
		public void testDictCount()
		{
			compareResourceOSO("builtins/dictCount.poc");
		}

		[Test]
		public void testEnumName()
		{
			compareResourceOSO("builtins/enumName.poc");
		}

		[Test]
		public void testEnumSymbols()
		{
			compareResourceOSO("builtins/enumSymbols.poc");
		}

		[Test]
		public void testEnumValue()
		{
			compareResourceOSO("builtins/enumValue.poc");
		}

		[Test]
		public void testListCount()
		{
			compareResourceOSO("builtins/listCount.poc");
		}

		[Test]
		public void testSetCount()
		{
			compareResourceOSO("builtins/setCount.poc");
		}

		[Test]
		public void testTextCount()
		{
			compareResourceOSO("builtins/textCount.poc");
		}

		[Test]
		public void testTimeHour()
		{
			compareResourceOSO("builtins/timeHour.poc");
		}

		[Test]
		public void testTimeMinute()
		{
			compareResourceOSO("builtins/timeMinute.poc");
		}

		[Test]
		public void testTimeSecond()
		{
			compareResourceOSO("builtins/timeSecond.poc");
		}

		[Test]
		public void testTupleCount()
		{
			compareResourceOSO("builtins/tupleCount.poc");
		}

	}
}

