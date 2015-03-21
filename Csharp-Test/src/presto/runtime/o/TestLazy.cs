using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestLazy : BaseOParserTest
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
			CheckOutput("lazy/cyclic.o");
		}

		[Test]
		public void testDict()
		{
			CheckOutput("lazy/dict.o");
		}

		[Test]
		public void testList()
		{
			CheckOutput("lazy/list.o");
		}

		[Test]
		public void testSet()
		{
			CheckOutput("lazy/set.o");
		}

		[Test]
		public void testTransient()
		{
			CheckOutput("lazy/transient.o");
		}

	}
}

