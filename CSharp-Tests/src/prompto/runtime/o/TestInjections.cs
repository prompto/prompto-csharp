// generated: 2015-07-05T23:01:01.285
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestInjections : BaseOParserTest
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
		public void testExpressionInjection()
		{
			CheckOutput("injections/expressionInjection.poc");
		}

	}
}

