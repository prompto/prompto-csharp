using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestInfer : BaseEParserTest
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
		public void testInferDict()
		{
			CheckOutput("infer/inferDict.pec");
		}

		[Test]
		public void testInferList()
		{
			CheckOutput("infer/inferList.pec");
		}

		[Test]
		public void testInferSet()
		{
			CheckOutput("infer/inferSet.pec");
		}

	}
}

