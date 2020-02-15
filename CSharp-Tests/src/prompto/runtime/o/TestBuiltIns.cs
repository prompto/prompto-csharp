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
		public void testDictCount()
		{
			CheckOutput("builtins/dictCount.poc");
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
		public void testIntegerFormat()
		{
			CheckOutput("builtins/integerFormat.poc");
		}

		[Test]
		public void testListCount()
		{
			CheckOutput("builtins/listCount.poc");
		}

		[Test]
		public void testListJoin()
		{
			CheckOutput("builtins/listJoin.poc");
		}

		[Test]
		public void testSetCount()
		{
			CheckOutput("builtins/setCount.poc");
		}

		[Test]
		public void testSetJoin()
		{
			CheckOutput("builtins/setJoin.poc");
		}

		[Test]
		public void testTextCapitalize()
		{
			CheckOutput("builtins/textCapitalize.poc");
		}

		[Test]
		public void testTextCount()
		{
			CheckOutput("builtins/textCount.poc");
		}

		[Test]
		public void testTextIndexOf()
		{
			CheckOutput("builtins/textIndexOf.poc");
		}

		[Test]
		public void testTextLowercase()
		{
			CheckOutput("builtins/textLowercase.poc");
		}

		[Test]
		public void testTextReplace()
		{
			CheckOutput("builtins/textReplace.poc");
		}

		[Test]
		public void testTextReplaceAll()
		{
			CheckOutput("builtins/textReplaceAll.poc");
		}

		[Test]
		public void testTextSplit()
		{
			CheckOutput("builtins/textSplit.poc");
		}

		[Test]
		public void testTextTrim()
		{
			CheckOutput("builtins/textTrim.poc");
		}

		[Test]
		public void testTextUppercase()
		{
			CheckOutput("builtins/textUppercase.poc");
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
		public void testTupleCount()
		{
			CheckOutput("builtins/tupleCount.poc");
		}

		[Test]
		public void testTupleJoin()
		{
			CheckOutput("builtins/tupleJoin.poc");
		}

	}
}

