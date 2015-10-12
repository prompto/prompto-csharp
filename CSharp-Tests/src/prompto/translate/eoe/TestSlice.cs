using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestSlice : BaseEParserTest
	{

		[Test]
		public void testSliceList()
		{
			compareResourceEOE("slice/sliceList.pec");
		}

		[Test]
		public void testSliceRange()
		{
			compareResourceEOE("slice/sliceRange.pec");
		}

		[Test]
		public void testSliceText()
		{
			compareResourceEOE("slice/sliceText.pec");
		}

	}
}

