using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestBuiltins : BaseEParserTest
	{

		[Test]
		public void testDateDayOfMonth()
		{
			compareResourceEOE("builtins/dateDayOfMonth.e");
		}

		[Test]
		public void testDateDayOfYear()
		{
			compareResourceEOE("builtins/dateDayOfYear.e");
		}

		[Test]
		public void testDateMonth()
		{
			compareResourceEOE("builtins/dateMonth.e");
		}

		[Test]
		public void testDateTimeDayOfMonth()
		{
			compareResourceEOE("builtins/dateTimeDayOfMonth.e");
		}

		[Test]
		public void testDateTimeDayOfYear()
		{
			compareResourceEOE("builtins/dateTimeDayOfYear.e");
		}

		[Test]
		public void testDateTimeHour()
		{
			compareResourceEOE("builtins/dateTimeHour.e");
		}

		[Test]
		public void testDateTimeMinute()
		{
			compareResourceEOE("builtins/dateTimeMinute.e");
		}

		[Test]
		public void testDateTimeMonth()
		{
			compareResourceEOE("builtins/dateTimeMonth.e");
		}

		[Test]
		public void testDateTimeSecond()
		{
			compareResourceEOE("builtins/dateTimeSecond.e");
		}

		[Test]
		public void testDateTimeTZName()
		{
			compareResourceEOE("builtins/dateTimeTZName.e");
		}

		[Test]
		public void testDateTimeTZOffset()
		{
			compareResourceEOE("builtins/dateTimeTZOffset.e");
		}

		[Test]
		public void testDateTimeYear()
		{
			compareResourceEOE("builtins/dateTimeYear.e");
		}

		[Test]
		public void testDateYear()
		{
			compareResourceEOE("builtins/dateYear.e");
		}

		[Test]
		public void testDictLength()
		{
			compareResourceEOE("builtins/dictLength.e");
		}

		[Test]
		public void testEnumName()
		{
			compareResourceEOE("builtins/enumName.e");
		}

		[Test]
		public void testEnumSymbols()
		{
			compareResourceEOE("builtins/enumSymbols.e");
		}

		[Test]
		public void testEnumValue()
		{
			compareResourceEOE("builtins/enumValue.e");
		}

		[Test]
		public void testListLength()
		{
			compareResourceEOE("builtins/listLength.e");
		}

		[Test]
		public void testSetLength()
		{
			compareResourceEOE("builtins/setLength.e");
		}

		[Test]
		public void testTextLength()
		{
			compareResourceEOE("builtins/textLength.e");
		}

		[Test]
		public void testTimeHour()
		{
			compareResourceEOE("builtins/timeHour.e");
		}

		[Test]
		public void testTimeMinute()
		{
			compareResourceEOE("builtins/timeMinute.e");
		}

		[Test]
		public void testTimeSecond()
		{
			compareResourceEOE("builtins/timeSecond.e");
		}

		[Test]
		public void testTupleLength()
		{
			compareResourceEOE("builtins/tupleLength.e");
		}

	}
}

