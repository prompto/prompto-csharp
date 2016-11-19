using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestPredicate : BaseEParserTest
	{

		[Test]
		public void testContainsItem()
		{
			compareResourceEME("predicate/containsItem.pec");
		}

		[Test]
		public void testEquals()
		{
			compareResourceEME("predicate/equals.pec");
		}

		[Test]
		public void testGreater()
		{
			compareResourceEME("predicate/greater.pec");
		}

		[Test]
		public void testInList()
		{
			compareResourceEME("predicate/inList.pec");
		}

		[Test]
		public void testLesser()
		{
			compareResourceEME("predicate/lesser.pec");
		}

		[Test]
		public void testNotEquals()
		{
			compareResourceEME("predicate/notEquals.pec");
		}

		[Test]
		public void testPartial()
		{
			compareResourceEME("predicate/partial.pec");
		}

		[Test]
		public void testRoughly()
		{
			compareResourceEME("predicate/roughly.pec");
		}

	}
}

