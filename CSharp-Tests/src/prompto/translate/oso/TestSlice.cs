// generated: 2015-07-05T23:01:01.412
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
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

