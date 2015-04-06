using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestSlice : BaseOParserTest
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
			CheckOutput("slice/sliceList.poc");
		}

		[Test]
		public void testSliceRange()
		{
			CheckOutput("slice/sliceRange.poc");
		}

		[Test]
		public void testSliceText()
		{
			CheckOutput("slice/sliceText.poc");
		}

	}
}

