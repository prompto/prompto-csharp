using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestBuiltins : BaseEParserTest
	{

		[Test]
		public void testDateDayOfMonth()
		{
			compareResourceEPE("builtins/dateDayOfMonth.e");
		}

		[Test]
		public void testDateDayOfYear()
		{
			compareResourceEPE("builtins/dateDayOfYear.e");
		}

		[Test]
		public void testDateMonth()
		{
			compareResourceEPE("builtins/dateMonth.e");
		}

		[Test]
		public void testDateTimeDayOfMonth()
		{
			compareResourceEPE("builtins/dateTimeDayOfMonth.e");
		}

		[Test]
		public void testDateTimeDayOfYear()
		{
			compareResourceEPE("builtins/dateTimeDayOfYear.e");
		}

		[Test]
		public void testDateTimeHour()
		{
			compareResourceEPE("builtins/dateTimeHour.e");
		}

		[Test]
		public void testDateTimeMinute()
		{
			compareResourceEPE("builtins/dateTimeMinute.e");
		}

		[Test]
		public void testDateTimeMonth()
		{
			compareResourceEPE("builtins/dateTimeMonth.e");
		}

		[Test]
		public void testDateTimeSecond()
		{
			compareResourceEPE("builtins/dateTimeSecond.e");
		}

		[Test]
		public void testDateTimeTZName()
		{
			compareResourceEPE("builtins/dateTimeTZName.e");
		}

		[Test]
		public void testDateTimeTZOffset()
		{
			compareResourceEPE("builtins/dateTimeTZOffset.e");
		}

		[Test]
		public void testDateTimeYear()
		{
			compareResourceEPE("builtins/dateTimeYear.e");
		}

		[Test]
		public void testDateYear()
		{
			compareResourceEPE("builtins/dateYear.e");
		}

		[Test]
		public void testDictLength()
		{
			compareResourceEPE("builtins/dictLength.e");
		}

		[Test]
		public void testEnumName()
		{
			compareResourceEPE("builtins/enumName.e");
		}

		[Test]
		public void testEnumSymbols()
		{
			compareResourceEPE("builtins/enumSymbols.e");
		}

		[Test]
		public void testEnumValue()
		{
			compareResourceEPE("builtins/enumValue.e");
		}

		[Test]
		public void testListLength()
		{
			compareResourceEPE("builtins/listLength.e");
		}

		[Test]
		public void testSetLength()
		{
			compareResourceEPE("builtins/setLength.e");
		}

		[Test]
		public void testTextLength()
		{
			compareResourceEPE("builtins/textLength.e");
		}

		[Test]
		public void testTimeHour()
		{
			compareResourceEPE("builtins/timeHour.e");
		}

		[Test]
		public void testTimeMinute()
		{
			compareResourceEPE("builtins/timeMinute.e");
		}

		[Test]
		public void testTimeSecond()
		{
			compareResourceEPE("builtins/timeSecond.e");
		}

		[Test]
		public void testTupleLength()
		{
			compareResourceEPE("builtins/tupleLength.e");
		}

	}
}

