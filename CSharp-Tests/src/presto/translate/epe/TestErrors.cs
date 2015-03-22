using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestErrors : BaseEParserTest
	{

		[Test]
		public void testDivideByZero()
		{
			compareResourceEPE("errors/divideByZero.e");
		}

		[Test]
		public void testIndexOutOfRange_listItem()
		{
			compareResourceEPE("errors/indexOutOfRange-listItem.e");
		}

		[Test]
		public void testIndexOutOfRange_sliceList()
		{
			compareResourceEPE("errors/indexOutOfRange-sliceList.e");
		}

		[Test]
		public void testIndexOutOfRange_sliceRange()
		{
			compareResourceEPE("errors/indexOutOfRange-sliceRange.e");
		}

		[Test]
		public void testIndexOutOfRange_sliceText()
		{
			compareResourceEPE("errors/indexOutOfRange-sliceText.e");
		}

		[Test]
		public void testNullDict()
		{
			compareResourceEPE("errors/nullDict.e");
		}

		[Test]
		public void testNullItem()
		{
			compareResourceEPE("errors/nullItem.e");
		}

		[Test]
		public void testNullKey()
		{
			compareResourceEPE("errors/nullKey.e");
		}

		[Test]
		public void testNullMember()
		{
			compareResourceEPE("errors/nullMember.e");
		}

		[Test]
		public void testNullMethod()
		{
			compareResourceEPE("errors/nullMethod.e");
		}

		[Test]
		public void testUserException()
		{
			compareResourceEPE("errors/userException.e");
		}

	}
}

