using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestLazy : BaseEParserTest
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
		public void testCyclic()
		{
			CheckOutput("lazy/cyclic.pec");
		}

		[Test]
		public void testDict()
		{
			CheckOutput("lazy/dict.pec");
		}

		[Test]
		public void testList()
		{
			CheckOutput("lazy/list.pec");
		}

		[Test]
		public void testSet()
		{
			CheckOutput("lazy/set.pec");
		}

		[Test]
		public void testTransient()
		{
			CheckOutput("lazy/transient.pec");
		}

	}
}

