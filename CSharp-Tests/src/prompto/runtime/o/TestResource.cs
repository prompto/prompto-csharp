using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestResource : BaseOParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testReadResource()
		{
			CheckOutput("resource/readResource.poc");
		}

		[Test]
		public void testReadResourceThen()
		{
			CheckOutput("resource/readResourceThen.poc");
		}

		[Test]
		public void testReadWithResource()
		{
			CheckOutput("resource/readWithResource.poc");
		}

		[Test]
		public void testWriteResource()
		{
			CheckOutput("resource/writeResource.poc");
		}

		[Test]
		public void testWriteWithResource()
		{
			CheckOutput("resource/writeWithResource.poc");
		}

	}
}

