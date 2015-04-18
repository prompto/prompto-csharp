using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestErrors : BaseEParserTest
	{

		[Test]
		public void testDivideByZero()
		{
			compareResourceESE("errors/divideByZero.pec");
		}

		[Test]
		public void testIndexOutOfRange_listItem()
		{
			compareResourceESE("errors/indexOutOfRange-listItem.pec");
		}

		[Test]
		public void testIndexOutOfRange_sliceList()
		{
			compareResourceESE("errors/indexOutOfRange-sliceList.pec");
		}

		[Test]
		public void testIndexOutOfRange_sliceRange()
		{
			compareResourceESE("errors/indexOutOfRange-sliceRange.pec");
		}

		[Test]
		public void testIndexOutOfRange_sliceText()
		{
			compareResourceESE("errors/indexOutOfRange-sliceText.pec");
		}

		[Test]
		public void testNullDict()
		{
			compareResourceESE("errors/nullDict.pec");
		}

		[Test]
		public void testNullItem()
		{
			compareResourceESE("errors/nullItem.pec");
		}

		[Test]
		public void testNullKey()
		{
			compareResourceESE("errors/nullKey.pec");
		}

		[Test]
		public void testNullMember()
		{
			compareResourceESE("errors/nullMember.pec");
		}

		[Test]
		public void testNullMethod()
		{
			compareResourceESE("errors/nullMethod.pec");
		}

		[Test]
		public void testUnexpected()
		{
			compareResourceESE("errors/unexpected.pec");
		}

		[Test]
		public void testUserException()
		{
			compareResourceESE("errors/userException.pec");
		}

	}
}

