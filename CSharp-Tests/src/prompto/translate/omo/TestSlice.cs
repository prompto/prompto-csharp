using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestSlice : BaseOParserTest
	{

		[Test]
		public void testSliceList()
		{
			compareResourceOMO("slice/sliceList.poc");
		}

		[Test]
		public void testSliceRange()
		{
			compareResourceOMO("slice/sliceRange.poc");
		}

		[Test]
		public void testSliceText()
		{
			compareResourceOMO("slice/sliceText.poc");
		}

	}
}

