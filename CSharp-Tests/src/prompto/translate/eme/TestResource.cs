using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestResource : BaseEParserTest
	{

		[Test]
		public void testReadResource()
		{
			compareResourceEME("resource/readResource.pec");
		}

		[Test]
		public void testReadWithResource()
		{
			compareResourceEME("resource/readWithResource.pec");
		}

		[Test]
		public void testWriteResource()
		{
			compareResourceEME("resource/writeResource.pec");
		}

		[Test]
		public void testWriteWithResource()
		{
			compareResourceEME("resource/writeWithResource.pec");
		}

	}
}

