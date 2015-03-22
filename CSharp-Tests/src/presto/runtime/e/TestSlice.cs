using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
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
			CheckOutput("slice/sliceList.e");
		}

		[Test]
		public void testSliceRange()
		{
			CheckOutput("slice/sliceRange.e");
		}

		[Test]
		public void testSliceText()
		{
			CheckOutput("slice/sliceText.e");
		}

	}
}

