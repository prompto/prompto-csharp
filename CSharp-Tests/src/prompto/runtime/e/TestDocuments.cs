using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestDocuments : BaseEParserTest
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
		public void testBlob()
		{
			CheckOutput("documents/blob.pec");
		}

		[Test]
		public void testDeepItem()
		{
			CheckOutput("documents/deepItem.pec");
		}

		[Test]
		public void testDeepMember()
		{
			CheckOutput("documents/deepMember.pec");
		}

		[Test]
		public void testInstance()
		{
			CheckOutput("documents/instance.pec");
		}

		[Test]
		public void testItem()
		{
			CheckOutput("documents/item.pec");
		}

		[Test]
		public void testLiteral()
		{
			CheckOutput("documents/literal.pec");
		}

		[Test]
		public void testMember()
		{
			CheckOutput("documents/member.pec");
		}

		[Test]
		public void testNamedItem()
		{
			CheckOutput("documents/namedItem.pec");
		}

	}
}

