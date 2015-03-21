using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestSetters : BaseEParserTest
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
			CheckOutput("setters/getter.e");
		}

		[Test]
		public void testSetter()
		{
			CheckOutput("setters/setter.e");
		}

	}
}

