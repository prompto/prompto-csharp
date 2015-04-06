using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
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

