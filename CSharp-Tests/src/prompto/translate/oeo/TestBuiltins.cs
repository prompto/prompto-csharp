using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestBuiltins : BaseOParserTest
	{

		[Test]
		public void testDateDayOfMonth()
		{
			compareResourceOEO("builtins/dateDayOfMonth.poc");
		}

		[Test]
		public void testDateDayOfYear()
		{
			compareResourceOEO("builtins/dateDayOfYear.poc");
		}

		[Test]
		public void testDateMonth()
		{
			compareResourceOEO("builtins/dateMonth.poc");
		}

		[Test]
		public void testDateTimeDayOfMonth()
		{
			compareResourceOEO("builtins/dateTimeDayOfMonth.poc");
		}

		[Test]
		public void testDateTimeDayOfYear()
		{
			compareResourceOEO("builtins/dateTimeDayOfYear.poc");
		}

		[Test]
		public void testDateTimeHour()
		{
			compareResourceOEO("builtins/dateTimeHour.poc");
		}

		[Test]
		public void testDateTimeMinute()
		{
			compareResourceOEO("builtins/dateTimeMinute.poc");
		}

		[Test]
		public void testDateTimeMonth()
		{
			compareResourceOEO("builtins/dateTimeMonth.poc");
		}

		[Test]
		public void testDateTimeSecond()
		{
			compareResourceOEO("builtins/dateTimeSecond.poc");
		}

		[Test]
		public void testDateTimeTZName()
		{
			compareResourceOEO("builtins/dateTimeTZName.poc");
		}

		[Test]
		public void testDateTimeTZOffset()
		{
			compareResourceOEO("builtins/dateTimeTZOffset.poc");
		}

		[Test]
		public void testDateTimeYear()
		{
			compareResourceOEO("builtins/dateTimeYear.poc");
		}

		[Test]
		public void testDateYear()
		{
			compareResourceOEO("builtins/dateYear.poc");
		}

		[Test]
		public void testDictCount()
		{
			compareResourceOEO("builtins/dictCount.poc");
		}

		[Test]
		public void testEnumName()
		{
			compareResourceOEO("builtins/enumName.poc");
		}

		[Test]
		public void testEnumSymbols()
		{
			compareResourceOEO("builtins/enumSymbols.poc");
		}

		[Test]
		public void testEnumValue()
		{
			compareResourceOEO("builtins/enumValue.poc");
		}

		[Test]
		public void testListCount()
		{
			compareResourceOEO("builtins/listCount.poc");
		}

		[Test]
		public void testSetCount()
		{
			compareResourceOEO("builtins/setCount.poc");
		}

		[Test]
		public void testTextCapitalize()
		{
			compareResourceOEO("builtins/textCapitalize.poc");
		}

		[Test]
		public void testTextCount()
		{
			compareResourceOEO("builtins/textCount.poc");
		}

		[Test]
		public void testTextLowercase()
		{
			compareResourceOEO("builtins/textLowercase.poc");
		}

		[Test]
		public void testTextReplace()
		{
			compareResourceOEO("builtins/textReplace.poc");
		}

		[Test]
		public void testTextSplit()
		{
			compareResourceOEO("builtins/textSplit.poc");
		}

		[Test]
		public void testTextTrim()
		{
			compareResourceOEO("builtins/textTrim.poc");
		}

		[Test]
		public void testTextUppercase()
		{
			compareResourceOEO("builtins/textUppercase.poc");
		}

		[Test]
		public void testTimeHour()
		{
			compareResourceOEO("builtins/timeHour.poc");
		}

		[Test]
		public void testTimeMinute()
		{
			compareResourceOEO("builtins/timeMinute.poc");
		}

		[Test]
		public void testTimeSecond()
		{
			compareResourceOEO("builtins/timeSecond.poc");
		}

		[Test]
		public void testTupleCount()
		{
			compareResourceOEO("builtins/tupleCount.poc");
		}

	}
}

