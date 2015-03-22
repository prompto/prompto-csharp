using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestLazy : BaseEParserTest
	{

		[Test]
		public void testCyclic()
		{
			compareResourceEPE("lazy/cyclic.e");
		}

		[Test]
		public void testDict()
		{
			compareResourceEPE("lazy/dict.e");
		}

		[Test]
		public void testList()
		{
			compareResourceEPE("lazy/list.e");
		}

		[Test]
		public void testSet()
		{
			compareResourceEPE("lazy/set.e");
		}

		[Test]
		public void testTransient()
		{
			compareResourceEPE("lazy/transient.e");
		}

	}
}

