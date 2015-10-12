using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.s
{

	[TestFixture]
	public class TestAdd : BaseSParserTest
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
		public void testAddInteger()
		{
			CheckOutput("add/addInteger.psc");
		}

	}
}

