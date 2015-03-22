using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestLazy : BaseEParserTest
	{

		[Test]
		public void testCyclic()
		{
			compareResourceEOE("lazy/cyclic.e");
		}

		[Test]
		public void testDict()
		{
			compareResourceEOE("lazy/dict.e");
		}

		[Test]
		public void testList()
		{
			compareResourceEOE("lazy/list.e");
		}

		[Test]
		public void testSet()
		{
			compareResourceEOE("lazy/set.e");
		}

		[Test]
		public void testTransient()
		{
			compareResourceEOE("lazy/transient.e");
		}

	}
}

