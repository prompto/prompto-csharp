using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestResource : BaseEParserTest
	{

		[Test]
		public void testBadRead()
		{
			compareResourceESE("resource/badRead.pec");
		}

		[Test]
		public void testBadResource()
		{
			compareResourceESE("resource/badResource.pec");
		}

		[Test]
		public void testBadWrite()
		{
			compareResourceESE("resource/badWrite.pec");
		}

		[Test]
		public void testReadResource()
		{
			compareResourceESE("resource/readResource.pec");
		}

		[Test]
		public void testReadWithResource()
		{
			compareResourceESE("resource/readWithResource.pec");
		}

		[Test]
		public void testWriteResource()
		{
			compareResourceESE("resource/writeResource.pec");
		}

		[Test]
		public void testWriteWithResource()
		{
			compareResourceESE("resource/writeWithResource.pec");
		}

	}
}

