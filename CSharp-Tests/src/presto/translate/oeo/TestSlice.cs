using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestSlice : BaseOParserTest
	{

		[Test]
		public void testSliceList()
		{
			compareResourceOEO("slice/sliceList.o");
		}

		[Test]
		public void testSliceRange()
		{
			compareResourceOEO("slice/sliceRange.o");
		}

		[Test]
		public void testSliceText()
		{
			compareResourceOEO("slice/sliceText.o");
		}

	}
}

