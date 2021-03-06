using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestSlice : BaseOParserTest
	{

		[Test]
		public void testSliceList()
		{
			compareResourceOEO("slice/sliceList.poc");
		}

		[Test]
		public void testSliceRange()
		{
			compareResourceOEO("slice/sliceRange.poc");
		}

		[Test]
		public void testSliceText()
		{
			compareResourceOEO("slice/sliceText.poc");
		}

	}
}

