using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestArrow : BaseEParserTest
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
		public void testHasAllFromList()
		{
			CheckOutput("arrow/hasAllFromList.pec");
		}

		[Test]
		public void testHasAllFromSet()
		{
			CheckOutput("arrow/hasAllFromSet.pec");
		}

		[Test]
		public void testHasAnyFromList()
		{
			CheckOutput("arrow/hasAnyFromList.pec");
		}

		[Test]
		public void testHasAnyFromSet()
		{
			CheckOutput("arrow/hasAnyFromSet.pec");
		}

	}
}

