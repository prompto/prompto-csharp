// generated: 2015-07-05T23:01:01.397
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestSetters : BaseOParserTest
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
		public void testGetter()
		{
			CheckOutput("setters/getter.poc");
		}

		[Test]
		public void testSetter()
		{
			CheckOutput("setters/setter.poc");
		}

	}
}

