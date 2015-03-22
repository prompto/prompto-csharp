using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestLazy : BaseOParserTest
	{

		[Test]
		public void testCyclic()
		{
			compareResourceOPO("lazy/cyclic.o");
		}

		[Test]
		public void testDict()
		{
			compareResourceOPO("lazy/dict.o");
		}

		[Test]
		public void testList()
		{
			compareResourceOPO("lazy/list.o");
		}

		[Test]
		public void testSet()
		{
			compareResourceOPO("lazy/set.o");
		}

		[Test]
		public void testTransient()
		{
			compareResourceOPO("lazy/transient.o");
		}

	}
}

