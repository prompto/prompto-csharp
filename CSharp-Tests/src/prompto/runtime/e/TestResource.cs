using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestResource : BaseEParserTest
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
			CheckOutput("resource/readResource.pec");
		}

		[Test]
		public void testReadResourceThen()
		{
			CheckOutput("resource/readResourceThen.pec");
		}

		[Test]
		public void testReadWithResource()
		{
			CheckOutput("resource/readWithResource.pec");
		}

		[Test]
		public void testWriteResource()
		{
			CheckOutput("resource/writeResource.pec");
		}

		[Test]
		public void testWriteResourceThen()
		{
			CheckOutput("resource/writeResourceThen.pec");
		}

		[Test]
		public void testWriteWithResource()
		{
			CheckOutput("resource/writeWithResource.pec");
		}

	}
}

