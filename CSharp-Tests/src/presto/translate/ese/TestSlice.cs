using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestSlice : BaseEParserTest
	{

		[Test]
		public void testSliceList()
		{
			compareResourceESE("slice/sliceList.pec");
		}

		[Test]
		public void testSliceRange()
		{
			compareResourceESE("slice/sliceRange.pec");
		}

		[Test]
		public void testSliceText()
		{
			compareResourceESE("slice/sliceText.pec");
		}

	}
}

