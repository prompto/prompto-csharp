using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestErrors : BaseOParserTest
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
		public void testDivideByZero()
		{
			CheckOutput("errors/divideByZero.poc");
		}

		[Test]
		public void testIndexOutOfRange_listItem()
		{
			CheckOutput("errors/indexOutOfRange-listItem.poc");
		}

		[Test]
		public void testIndexOutOfRange_sliceList()
		{
			CheckOutput("errors/indexOutOfRange-sliceList.poc");
		}

		[Test]
		public void testIndexOutOfRange_sliceRange()
		{
			CheckOutput("errors/indexOutOfRange-sliceRange.poc");
		}

		[Test]
		public void testIndexOutOfRange_sliceText()
		{
			CheckOutput("errors/indexOutOfRange-sliceText.poc");
		}

		[Test]
		public void testNullDict()
		{
			CheckOutput("errors/nullDict.poc");
		}

		[Test]
		public void testNullItem()
		{
			CheckOutput("errors/nullItem.poc");
		}

		[Test]
		public void testNullKey()
		{
			CheckOutput("errors/nullKey.poc");
		}

		[Test]
		public void testNullMember()
		{
			CheckOutput("errors/nullMember.poc");
		}

		[Test]
		public void testNullMethod()
		{
			CheckOutput("errors/nullMethod.poc");
		}

		[Test]
		public void testUserException()
		{
			CheckOutput("errors/userException.poc");
		}

	}
}

