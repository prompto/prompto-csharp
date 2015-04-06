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
			CheckOutput("lazy/cyclic.poc");
		}

		[Test]
		public void testDict()
		{
			CheckOutput("lazy/dict.poc");
		}

		[Test]
		public void testList()
		{
			CheckOutput("lazy/list.poc");
		}

		[Test]
		public void testSet()
		{
			CheckOutput("lazy/set.poc");
		}

		[Test]
		public void testTransient()
		{
			CheckOutput("lazy/transient.poc");
		}

	}
}

