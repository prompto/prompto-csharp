using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestLazy : BaseOParserTest
	{

		[Test]
		public void testCyclic()
		{
			compareResourceOMO("lazy/cyclic.poc");
		}

		[Test]
		public void testDict()
		{
			compareResourceOMO("lazy/dict.poc");
		}

		[Test]
		public void testList()
		{
			compareResourceOMO("lazy/list.poc");
		}

		[Test]
		public void testSet()
		{
			compareResourceOMO("lazy/set.poc");
		}

		[Test]
		public void testTransient()
		{
			compareResourceOMO("lazy/transient.poc");
		}

	}
}

