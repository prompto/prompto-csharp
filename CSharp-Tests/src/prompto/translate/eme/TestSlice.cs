using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestSlice : BaseEParserTest
	{

		[Test]
		public void testSliceList()
		{
			compareResourceEME("slice/sliceList.pec");
		}

		[Test]
		public void testSliceRange()
		{
			compareResourceEME("slice/sliceRange.pec");
		}

		[Test]
		public void testSliceText()
		{
			compareResourceEME("slice/sliceText.pec");
		}

	}
}

