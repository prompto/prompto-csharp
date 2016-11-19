using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestLazy : BaseEParserTest
	{

		[Test]
		public void testCyclic()
		{
			compareResourceEME("lazy/cyclic.pec");
		}

		[Test]
		public void testDict()
		{
			compareResourceEME("lazy/dict.pec");
		}

		[Test]
		public void testList()
		{
			compareResourceEME("lazy/list.pec");
		}

		[Test]
		public void testSet()
		{
			compareResourceEME("lazy/set.pec");
		}

		[Test]
		public void testTransient()
		{
			compareResourceEME("lazy/transient.pec");
		}

	}
}

