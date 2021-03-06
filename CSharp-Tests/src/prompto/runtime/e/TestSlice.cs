using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestSlice : BaseEParserTest
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
		public void testSliceList()
		{
			CheckOutput("slice/sliceList.pec");
		}

		[Test]
		public void testSliceRange()
		{
			CheckOutput("slice/sliceRange.pec");
		}

		[Test]
		public void testSliceText()
		{
			CheckOutput("slice/sliceText.pec");
		}

	}
}

