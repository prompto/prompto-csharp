using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestLazy : BaseOParserTest
	{

		[Test]
		public void testCyclic()
		{
			compareResourceOEO("lazy/cyclic.o");
		}

		[Test]
		public void testDict()
		{
			compareResourceOEO("lazy/dict.o");
		}

		[Test]
		public void testList()
		{
			compareResourceOEO("lazy/list.o");
		}

		[Test]
		public void testSet()
		{
			compareResourceOEO("lazy/set.o");
		}

		[Test]
		public void testTransient()
		{
			compareResourceOEO("lazy/transient.o");
		}

	}
}

