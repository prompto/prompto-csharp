using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestDocuments : BaseOParserTest
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
		public void testDeepItem()
		{
			CheckOutput("documents/deepItem.poc");
		}

		[Test]
		public void testDeepMember()
		{
			CheckOutput("documents/deepMember.poc");
		}

		[Test]
		public void testInstance()
		{
			CheckOutput("documents/instance.poc");
		}

		[Test]
		public void testItem()
		{
			CheckOutput("documents/item.poc");
		}

		[Test]
		public void testLiteral()
		{
			CheckOutput("documents/literal.poc");
		}

		[Test]
		public void testMember()
		{
			CheckOutput("documents/member.poc");
		}

	}
}

