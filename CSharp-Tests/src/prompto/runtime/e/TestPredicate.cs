using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestPredicate : BaseEParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testContainsItem()
		{
			CheckOutput("predicate/containsItem.pec");
		}

		[Test]
		public void testEquals()
		{
			CheckOutput("predicate/equals.pec");
		}

		[Test]
		public void testEqualsError()
		{
			CheckOutput("predicate/equalsError.pec");
		}

		[Test]
		public void testGreater()
		{
			CheckOutput("predicate/greater.pec");
		}

		[Test]
		public void testHasItem()
		{
			CheckOutput("predicate/hasItem.pec");
		}

		[Test]
		public void testInList()
		{
			CheckOutput("predicate/inList.pec");
		}

		[Test]
		public void testLesser()
		{
			CheckOutput("predicate/lesser.pec");
		}

		[Test]
		public void testNotEquals()
		{
			CheckOutput("predicate/notEquals.pec");
		}

		[Test]
		public void testPartial()
		{
			CheckOutput("predicate/partial.pec");
		}

		[Test]
		public void testRoughly()
		{
			CheckOutput("predicate/roughly.pec");
		}

	}
}

