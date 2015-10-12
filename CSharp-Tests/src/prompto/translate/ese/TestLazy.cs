using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestLazy : BaseEParserTest
	{

		[Test]
		public void testCyclic()
		{
			compareResourceESE("lazy/cyclic.pec");
		}

		[Test]
		public void testDict()
		{
			compareResourceESE("lazy/dict.pec");
		}

		[Test]
		public void testList()
		{
			compareResourceESE("lazy/list.pec");
		}

		[Test]
		public void testSet()
		{
			compareResourceESE("lazy/set.pec");
		}

		[Test]
		public void testTransient()
		{
			compareResourceESE("lazy/transient.pec");
		}

	}
}

