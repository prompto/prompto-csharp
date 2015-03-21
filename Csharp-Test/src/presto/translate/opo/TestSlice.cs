using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestSlice : BaseOParserTest
	{

		[Test]
		public void testSliceList()
		{
			compareResourceOPO("slice/sliceList.o");
		}

		[Test]
		public void testSliceRange()
		{
			compareResourceOPO("slice/sliceRange.o");
		}

		[Test]
		public void testSliceText()
		{
			compareResourceOPO("slice/sliceText.o");
		}

	}
}

