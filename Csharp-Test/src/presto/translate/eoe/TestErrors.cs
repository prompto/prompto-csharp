using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestErrors : BaseEParserTest
	{

		[Test]
		public void testDivideByZero()
		{
			compareResourceEOE("errors/divideByZero.e");
		}

		[Test]
		public void testIndexOutOfRange_listItem()
		{
			compareResourceEOE("errors/indexOutOfRange-listItem.e");
		}

		[Test]
		public void testIndexOutOfRange_sliceList()
		{
			compareResourceEOE("errors/indexOutOfRange-sliceList.e");
		}

		[Test]
		public void testIndexOutOfRange_sliceRange()
		{
			compareResourceEOE("errors/indexOutOfRange-sliceRange.e");
		}

		[Test]
		public void testIndexOutOfRange_sliceText()
		{
			compareResourceEOE("errors/indexOutOfRange-sliceText.e");
		}

		[Test]
		public void testNullDict()
		{
			compareResourceEOE("errors/nullDict.e");
		}

		[Test]
		public void testNullItem()
		{
			compareResourceEOE("errors/nullItem.e");
		}

		[Test]
		public void testNullKey()
		{
			compareResourceEOE("errors/nullKey.e");
		}

		[Test]
		public void testNullMember()
		{
			compareResourceEOE("errors/nullMember.e");
		}

		[Test]
		public void testNullMethod()
		{
			compareResourceEOE("errors/nullMethod.e");
		}

		[Test]
		public void testUserException()
		{
			compareResourceEOE("errors/userException.e");
		}

	}
}

