using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestErrors : BaseOParserTest
	{

		[Test]
		public void testDivideByZero()
		{
			compareResourceOMO("errors/divideByZero.poc");
		}

		[Test]
		public void testIndexOutOfRange_listItem()
		{
			compareResourceOMO("errors/indexOutOfRange-listItem.poc");
		}

		[Test]
		public void testIndexOutOfRange_sliceList()
		{
			compareResourceOMO("errors/indexOutOfRange-sliceList.poc");
		}

		[Test]
		public void testIndexOutOfRange_sliceRange()
		{
			compareResourceOMO("errors/indexOutOfRange-sliceRange.poc");
		}

		[Test]
		public void testIndexOutOfRange_sliceText()
		{
			compareResourceOMO("errors/indexOutOfRange-sliceText.poc");
		}

		[Test]
		public void testMemberInCatch()
		{
			compareResourceOMO("errors/memberInCatch.poc");
		}

		[Test]
		public void testNullDict()
		{
			compareResourceOMO("errors/nullDict.poc");
		}

		[Test]
		public void testNullItem()
		{
			compareResourceOMO("errors/nullItem.poc");
		}

		[Test]
		public void testNullKey()
		{
			compareResourceOMO("errors/nullKey.poc");
		}

		[Test]
		public void testNullMember()
		{
			compareResourceOMO("errors/nullMember.poc");
		}

		[Test]
		public void testNullMethod()
		{
			compareResourceOMO("errors/nullMethod.poc");
		}

		[Test]
		public void testUserException()
		{
			compareResourceOMO("errors/userException.poc");
		}

	}
}

