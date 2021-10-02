using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestErrors : BaseOParserTest
	{

		[Test]
		public void testDivideByZero()
		{
			compareResourceOEO("errors/divideByZero.poc");
		}

		[Test]
		public void testIndexOutOfRange_listItem()
		{
			compareResourceOEO("errors/indexOutOfRange-listItem.poc");
		}

		[Test]
		public void testIndexOutOfRange_sliceList()
		{
			compareResourceOEO("errors/indexOutOfRange-sliceList.poc");
		}

		[Test]
		public void testIndexOutOfRange_sliceRange()
		{
			compareResourceOEO("errors/indexOutOfRange-sliceRange.poc");
		}

		[Test]
		public void testIndexOutOfRange_sliceText()
		{
			compareResourceOEO("errors/indexOutOfRange-sliceText.poc");
		}

		[Test]
		public void testMemberInCatch()
		{
			compareResourceOEO("errors/memberInCatch.poc");
		}

		[Test]
		public void testNullDict()
		{
			compareResourceOEO("errors/nullDict.poc");
		}

		[Test]
		public void testNullItem()
		{
			compareResourceOEO("errors/nullItem.poc");
		}

		[Test]
		public void testNullKey()
		{
			compareResourceOEO("errors/nullKey.poc");
		}

		[Test]
		public void testNullMember()
		{
			compareResourceOEO("errors/nullMember.poc");
		}

		[Test]
		public void testNullMethod()
		{
			compareResourceOEO("errors/nullMethod.poc");
		}

		[Test]
		public void testUserException()
		{
			compareResourceOEO("errors/userException.poc");
		}

	}
}

