using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
{

	[TestFixture]
	public class TestLazy : BaseOParserTest
	{

		[Test]
		public void testCyclic()
		{
			compareResourceOSO("lazy/cyclic.poc");
		}

		[Test]
		public void testDict()
		{
			compareResourceOSO("lazy/dict.poc");
		}

		[Test]
		public void testList()
		{
			compareResourceOSO("lazy/list.poc");
		}

		[Test]
		public void testSet()
		{
			compareResourceOSO("lazy/set.poc");
		}

		[Test]
		public void testTransient()
		{
			compareResourceOSO("lazy/transient.poc");
		}

	}
}

