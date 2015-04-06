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
			compareResourceEOE("lazy/cyclic.pec");
		}

		[Test]
		public void testDict()
		{
			compareResourceEOE("lazy/dict.pec");
		}

		[Test]
		public void testList()
		{
			compareResourceEOE("lazy/list.pec");
		}

		[Test]
		public void testSet()
		{
			compareResourceEOE("lazy/set.pec");
		}

		[Test]
		public void testTransient()
		{
			compareResourceEOE("lazy/transient.pec");
		}

	}
}

