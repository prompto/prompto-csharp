using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestErrors : BaseEParserTest
	{

		[Test]
		public void testDivideByZero()
		{
			compareResourceEME("errors/divideByZero.pec");
		}

		[Test]
		public void testIndexOutOfRange_listItem()
		{
			compareResourceEME("errors/indexOutOfRange-listItem.pec");
		}

		[Test]
		public void testIndexOutOfRange_sliceList()
		{
			compareResourceEME("errors/indexOutOfRange-sliceList.pec");
		}

		[Test]
		public void testIndexOutOfRange_sliceRange()
		{
			compareResourceEME("errors/indexOutOfRange-sliceRange.pec");
		}

		[Test]
		public void testIndexOutOfRange_sliceText()
		{
			compareResourceEME("errors/indexOutOfRange-sliceText.pec");
		}

		[Test]
		public void testMemberInCatch()
		{
			compareResourceEME("errors/memberInCatch.pec");
		}

		[Test]
		public void testNullDict()
		{
			compareResourceEME("errors/nullDict.pec");
		}

		[Test]
		public void testNullItem()
		{
			compareResourceEME("errors/nullItem.pec");
		}

		[Test]
		public void testNullKey()
		{
			compareResourceEME("errors/nullKey.pec");
		}

		[Test]
		public void testNullMember()
		{
			compareResourceEME("errors/nullMember.pec");
		}

		[Test]
		public void testNullMethod()
		{
			compareResourceEME("errors/nullMethod.pec");
		}

		[Test]
		public void testUnexpected()
		{
			compareResourceEME("errors/unexpected.pec");
		}

		[Test]
		public void testUserException()
		{
			compareResourceEME("errors/userException.pec");
		}

	}
}

