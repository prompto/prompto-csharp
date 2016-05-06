using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestResource : BaseOParserTest
	{

		[Test]
		public void testReadResource()
		{
			compareResourceOEO("resource/readResource.poc");
		}

		[Test]
		public void testReadWithResource()
		{
			compareResourceOEO("resource/readWithResource.poc");
		}

		[Test]
		public void testWriteResource()
		{
			compareResourceOEO("resource/writeResource.poc");
		}

		[Test]
		public void testWriteWithResource()
		{
			compareResourceOEO("resource/writeWithResource.poc");
		}

	}
}

