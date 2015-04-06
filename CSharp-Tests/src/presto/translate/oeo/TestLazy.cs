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
			compareResourceOEO("lazy/cyclic.poc");
		}

		[Test]
		public void testDict()
		{
			compareResourceOEO("lazy/dict.poc");
		}

		[Test]
		public void testList()
		{
			compareResourceOEO("lazy/list.poc");
		}

		[Test]
		public void testSet()
		{
			compareResourceOEO("lazy/set.poc");
		}

		[Test]
		public void testTransient()
		{
			compareResourceOEO("lazy/transient.poc");
		}

	}
}

