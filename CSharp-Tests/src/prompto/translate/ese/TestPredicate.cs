using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestPredicate : BaseEParserTest
	{

		[Test]
		public void testContainsItem()
		{
			compareResourceESE("predicate/containsItem.pec");
		}

		[Test]
		public void testEquals()
		{
			compareResourceESE("predicate/equals.pec");
		}

		[Test]
		public void testGreater()
		{
			compareResourceESE("predicate/greater.pec");
		}

		[Test]
		public void testInList()
		{
			compareResourceESE("predicate/inList.pec");
		}

		[Test]
		public void testLesser()
		{
			compareResourceESE("predicate/lesser.pec");
		}

		[Test]
		public void testNotEquals()
		{
			compareResourceESE("predicate/notEquals.pec");
		}

		[Test]
		public void testPartial()
		{
			compareResourceESE("predicate/partial.pec");
		}

		[Test]
		public void testRoughly()
		{
			compareResourceESE("predicate/roughly.pec");
		}

	}
}

