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
			CheckOutput("errors/divideByZero.o");
		}

		[Test]
		public void testIndexOutOfRange_listItem()
		{
			CheckOutput("errors/indexOutOfRange-listItem.o");
		}

		[Test]
		public void testIndexOutOfRange_sliceList()
		{
			CheckOutput("errors/indexOutOfRange-sliceList.o");
		}

		[Test]
		public void testIndexOutOfRange_sliceRange()
		{
			CheckOutput("errors/indexOutOfRange-sliceRange.o");
		}

		[Test]
		public void testIndexOutOfRange_sliceText()
		{
			CheckOutput("errors/indexOutOfRange-sliceText.o");
		}

		[Test]
		public void testNullDict()
		{
			CheckOutput("errors/nullDict.o");
		}

		[Test]
		public void testNullItem()
		{
			CheckOutput("errors/nullItem.o");
		}

		[Test]
		public void testNullKey()
		{
			CheckOutput("errors/nullKey.o");
		}

		[Test]
		public void testNullMember()
		{
			CheckOutput("errors/nullMember.o");
		}

		[Test]
		public void testNullMethod()
		{
			CheckOutput("errors/nullMethod.o");
		}

		[Test]
		public void testUserException()
		{
			CheckOutput("errors/userException.o");
		}

	}
}

