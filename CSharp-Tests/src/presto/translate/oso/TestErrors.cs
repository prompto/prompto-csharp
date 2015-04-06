using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
{

	[TestFixture]
	public class TestErrors : BaseOParserTest
	{

		[Test]
		public void testDivideByZero()
		{
			compareResourceOSO("errors/divideByZero.poc");
		}

		[Test]
		public void testIndexOutOfRange_listItem()
		{
			compareResourceOSO("errors/indexOutOfRange-listItem.poc");
		}

		[Test]
		public void testIndexOutOfRange_sliceList()
		{
			compareResourceOSO("errors/indexOutOfRange-sliceList.poc");
		}

		[Test]
		public void testIndexOutOfRange_sliceRange()
		{
			compareResourceOSO("errors/indexOutOfRange-sliceRange.poc");
		}

		[Test]
		public void testIndexOutOfRange_sliceText()
		{
			compareResourceOSO("errors/indexOutOfRange-sliceText.poc");
		}

		[Test]
		public void testNullDict()
		{
			compareResourceOSO("errors/nullDict.poc");
		}

		[Test]
		public void testNullItem()
		{
			compareResourceOSO("errors/nullItem.poc");
		}

		[Test]
		public void testNullKey()
		{
			compareResourceOSO("errors/nullKey.poc");
		}

		[Test]
		public void testNullMember()
		{
			compareResourceOSO("errors/nullMember.poc");
		}

		[Test]
		public void testNullMethod()
		{
			compareResourceOSO("errors/nullMethod.poc");
		}

		[Test]
		public void testUserException()
		{
			compareResourceOSO("errors/userException.poc");
		}

	}
}

