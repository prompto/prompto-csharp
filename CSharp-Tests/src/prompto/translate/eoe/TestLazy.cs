// generated: 2015-07-05T23:01:01.295
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
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

