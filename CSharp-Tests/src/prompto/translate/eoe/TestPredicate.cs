using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestPredicate : BaseEParserTest
	{

		[Test]
		public void testContainsItem()
		{
			compareResourceEOE("predicate/containsItem.pec");
		}

		[Test]
		public void testEquals()
		{
			compareResourceEOE("predicate/equals.pec");
		}

		[Test]
		public void testEqualsError()
		{
			compareResourceEOE("predicate/equalsError.pec");
		}

		[Test]
		public void testGreater()
		{
			compareResourceEOE("predicate/greater.pec");
		}

		[Test]
		public void testHasItem()
		{
			compareResourceEOE("predicate/hasItem.pec");
		}

		[Test]
		public void testInList()
		{
			compareResourceEOE("predicate/inList.pec");
		}

		[Test]
		public void testLesser()
		{
			compareResourceEOE("predicate/lesser.pec");
		}

		[Test]
		public void testNotEquals()
		{
			compareResourceEOE("predicate/notEquals.pec");
		}

		[Test]
		public void testPartial()
		{
			compareResourceEOE("predicate/partial.pec");
		}

		[Test]
		public void testRoughly()
		{
			compareResourceEOE("predicate/roughly.pec");
		}

	}
}

