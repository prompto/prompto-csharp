using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
{

	[TestFixture]
	public class TestSlice : BaseOParserTest
	{

		[Test]
		public void testSliceList()
		{
			compareResourceOSO("slice/sliceList.poc");
		}

		[Test]
		public void testSliceRange()
		{
			compareResourceOSO("slice/sliceRange.poc");
		}

		[Test]
		public void testSliceText()
		{
			compareResourceOSO("slice/sliceText.poc");
		}

	}
}

