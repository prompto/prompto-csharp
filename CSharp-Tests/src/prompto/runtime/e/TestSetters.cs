using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
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
			CheckOutput("setters/getter.pec");
		}

		[Test]
		public void testGetterCall()
		{
			CheckOutput("setters/getterCall.pec");
		}

		[Test]
		public void testSetter()
		{
			CheckOutput("setters/setter.pec");
		}

	}
}

