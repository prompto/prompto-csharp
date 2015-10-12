using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
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
			CheckOutput("errors/divideByZero.pec");
		}

		[Test]
		public void testIndexOutOfRange_listItem()
		{
			CheckOutput("errors/indexOutOfRange-listItem.pec");
		}

		[Test]
		public void testIndexOutOfRange_sliceList()
		{
			CheckOutput("errors/indexOutOfRange-sliceList.pec");
		}

		[Test]
		public void testIndexOutOfRange_sliceRange()
		{
			CheckOutput("errors/indexOutOfRange-sliceRange.pec");
		}

		[Test]
		public void testIndexOutOfRange_sliceText()
		{
			CheckOutput("errors/indexOutOfRange-sliceText.pec");
		}

		[Test]
		public void testNullDict()
		{
			CheckOutput("errors/nullDict.pec");
		}

		[Test]
		public void testNullItem()
		{
			CheckOutput("errors/nullItem.pec");
		}

		[Test]
		public void testNullKey()
		{
			CheckOutput("errors/nullKey.pec");
		}

		[Test]
		public void testNullMember()
		{
			CheckOutput("errors/nullMember.pec");
		}

		[Test]
		public void testNullMethod()
		{
			CheckOutput("errors/nullMethod.pec");
		}

		[Test]
		public void testUserException()
		{
			CheckOutput("errors/userException.pec");
		}

	}
}

