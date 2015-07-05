// generated: 2015-07-05T23:01:01.300
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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

