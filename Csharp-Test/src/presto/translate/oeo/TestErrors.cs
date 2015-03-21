using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestErrors : BaseOParserTest
	{

		[Test]
		public void testDivideByZero()
		{
			compareResourceOEO("errors/divideByZero.o");
		}

		[Test]
		public void testIndexOutOfRange_listItem()
		{
			compareResourceOEO("errors/indexOutOfRange-listItem.o");
		}

		[Test]
		public void testIndexOutOfRange_sliceList()
		{
			compareResourceOEO("errors/indexOutOfRange-sliceList.o");
		}

		[Test]
		public void testIndexOutOfRange_sliceRange()
		{
			compareResourceOEO("errors/indexOutOfRange-sliceRange.o");
		}

		[Test]
		public void testIndexOutOfRange_sliceText()
		{
			compareResourceOEO("errors/indexOutOfRange-sliceText.o");
		}

		[Test]
		public void testNullDict()
		{
			compareResourceOEO("errors/nullDict.o");
		}

		[Test]
		public void testNullItem()
		{
			compareResourceOEO("errors/nullItem.o");
		}

		[Test]
		public void testNullKey()
		{
			compareResourceOEO("errors/nullKey.o");
		}

		[Test]
		public void testNullMember()
		{
			compareResourceOEO("errors/nullMember.o");
		}

		[Test]
		public void testNullMethod()
		{
			compareResourceOEO("errors/nullMethod.o");
		}

		[Test]
		public void testUserException()
		{
			compareResourceOEO("errors/userException.o");
		}

	}
}

