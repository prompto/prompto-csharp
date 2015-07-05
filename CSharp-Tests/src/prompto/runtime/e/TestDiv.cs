// generated: 2015-07-05T23:01:01.222
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestDiv : BaseEParserTest
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
		public void testDivDecimal()
		{
			CheckOutput("div/divDecimal.pec");
		}

		[Test]
		public void testDivInteger()
		{
			CheckOutput("div/divInteger.pec");
		}

		[Test]
		public void testIdivInteger()
		{
			CheckOutput("div/idivInteger.pec");
		}

		[Test]
		public void testModInteger()
		{
			CheckOutput("div/modInteger.pec");
		}

	}
}

