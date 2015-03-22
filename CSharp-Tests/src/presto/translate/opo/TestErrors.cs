using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestErrors : BaseOParserTest
	{

		[Test]
		public void testDivideByZero()
		{
			compareResourceOPO("errors/divideByZero.o");
		}

		[Test]
		public void testIndexOutOfRange_listItem()
		{
			compareResourceOPO("errors/indexOutOfRange-listItem.o");
		}

		[Test]
		public void testIndexOutOfRange_sliceList()
		{
			compareResourceOPO("errors/indexOutOfRange-sliceList.o");
		}

		[Test]
		public void testIndexOutOfRange_sliceRange()
		{
			compareResourceOPO("errors/indexOutOfRange-sliceRange.o");
		}

		[Test]
		public void testIndexOutOfRange_sliceText()
		{
			compareResourceOPO("errors/indexOutOfRange-sliceText.o");
		}

		[Test]
		public void testNullDict()
		{
			compareResourceOPO("errors/nullDict.o");
		}

		[Test]
		public void testNullItem()
		{
			compareResourceOPO("errors/nullItem.o");
		}

		[Test]
		public void testNullKey()
		{
			compareResourceOPO("errors/nullKey.o");
		}

		[Test]
		public void testNullMember()
		{
			compareResourceOPO("errors/nullMember.o");
		}

		[Test]
		public void testNullMethod()
		{
			compareResourceOPO("errors/nullMethod.o");
		}

		[Test]
		public void testUserException()
		{
			compareResourceOPO("errors/userException.o");
		}

	}
}

