using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestErrors : BaseEParserTest
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
			CheckOutput("errors/divideByZero.e");
		}

		[Test]
		public void testIndexOutOfRange_listItem()
		{
			CheckOutput("errors/indexOutOfRange-listItem.e");
		}

		[Test]
		public void testIndexOutOfRange_sliceList()
		{
			CheckOutput("errors/indexOutOfRange-sliceList.e");
		}

		[Test]
		public void testIndexOutOfRange_sliceRange()
		{
			CheckOutput("errors/indexOutOfRange-sliceRange.e");
		}

		[Test]
		public void testIndexOutOfRange_sliceText()
		{
			CheckOutput("errors/indexOutOfRange-sliceText.e");
		}

		[Test]
		public void testNullDict()
		{
			CheckOutput("errors/nullDict.e");
		}

		[Test]
		public void testNullItem()
		{
			CheckOutput("errors/nullItem.e");
		}

		[Test]
		public void testNullKey()
		{
			CheckOutput("errors/nullKey.e");
		}

		[Test]
		public void testNullMember()
		{
			CheckOutput("errors/nullMember.e");
		}

		[Test]
		public void testNullMethod()
		{
			CheckOutput("errors/nullMethod.e");
		}

		[Test]
		public void testUserException()
		{
			CheckOutput("errors/userException.e");
		}

	}
}

