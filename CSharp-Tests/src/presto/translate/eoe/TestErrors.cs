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
			compareResourceEOE("errors/divideByZero.pec");
		}

		[Test]
		public void testIndexOutOfRange_listItem()
		{
			compareResourceEOE("errors/indexOutOfRange-listItem.pec");
		}

		[Test]
		public void testIndexOutOfRange_sliceList()
		{
			compareResourceEOE("errors/indexOutOfRange-sliceList.pec");
		}

		[Test]
		public void testIndexOutOfRange_sliceRange()
		{
			compareResourceEOE("errors/indexOutOfRange-sliceRange.pec");
		}

		[Test]
		public void testIndexOutOfRange_sliceText()
		{
			compareResourceEOE("errors/indexOutOfRange-sliceText.pec");
		}

		[Test]
		public void testNullDict()
		{
			compareResourceEOE("errors/nullDict.pec");
		}

		[Test]
		public void testNullItem()
		{
			compareResourceEOE("errors/nullItem.pec");
		}

		[Test]
		public void testNullKey()
		{
			compareResourceEOE("errors/nullKey.pec");
		}

		[Test]
		public void testNullMember()
		{
			compareResourceEOE("errors/nullMember.pec");
		}

		[Test]
		public void testNullMethod()
		{
			compareResourceEOE("errors/nullMethod.pec");
		}

		[Test]
		public void testUnexpected()
		{
			compareResourceEOE("errors/unexpected.pec");
		}

		[Test]
		public void testUserException()
		{
			compareResourceEOE("errors/userException.pec");
		}

	}
}

