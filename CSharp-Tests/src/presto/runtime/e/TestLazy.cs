using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
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
			CheckOutput("lazy/cyclic.e");
		}

		[Test]
		public void testDict()
		{
			CheckOutput("lazy/dict.e");
		}

		[Test]
		public void testList()
		{
			CheckOutput("lazy/list.e");
		}

		[Test]
		public void testSet()
		{
			CheckOutput("lazy/set.e");
		}

		[Test]
		public void testTransient()
		{
			CheckOutput("lazy/transient.e");
		}

	}
}

