using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestSlice : BaseEParserTest
	{

		[Test]
		public void testSliceList()
		{
			compareResourceEOE("slice/sliceList.e");
		}

		[Test]
		public void testSliceRange()
		{
			compareResourceEOE("slice/sliceRange.e");
		}

		[Test]
		public void testSliceText()
		{
			compareResourceEOE("slice/sliceText.e");
		}

	}
}

