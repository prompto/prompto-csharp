using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestSlice : BaseEParserTest
	{

		[Test]
		public void testSliceList()
		{
			compareResourceEPE("slice/sliceList.e");
		}

		[Test]
		public void testSliceRange()
		{
			compareResourceEPE("slice/sliceRange.e");
		}

		[Test]
		public void testSliceText()
		{
			compareResourceEPE("slice/sliceText.e");
		}

	}
}

