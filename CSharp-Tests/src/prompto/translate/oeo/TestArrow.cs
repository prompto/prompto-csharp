using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestArrow : BaseOParserTest
	{

		[Test]
		public void testArrowArgument()
		{
			compareResourceOEO("arrow/arrowArgument.poc");
		}

		[Test]
		public void testFilterFromList()
		{
			compareResourceOEO("arrow/filterFromList.poc");
		}

		[Test]
		public void testFilterFromSet()
		{
			compareResourceOEO("arrow/filterFromSet.poc");
		}

		[Test]
		public void testHasAllFromList()
		{
			compareResourceOEO("arrow/hasAllFromList.poc");
		}

		[Test]
		public void testHasAllFromSet()
		{
			compareResourceOEO("arrow/hasAllFromSet.poc");
		}

		[Test]
		public void testHasAnyFromList()
		{
			compareResourceOEO("arrow/hasAnyFromList.poc");
		}

		[Test]
		public void testHasAnyFromSet()
		{
			compareResourceOEO("arrow/hasAnyFromSet.poc");
		}

		[Test]
		public void testSortCategory1Arg()
		{
			compareResourceOEO("arrow/sortCategory1Arg.poc");
		}

		[Test]
		public void testSortCategory2Args()
		{
			compareResourceOEO("arrow/sortCategory2Args.poc");
		}

		[Test]
		public void testSortText1Arg()
		{
			compareResourceOEO("arrow/sortText1Arg.poc");
		}

		[Test]
		public void testSortText1ArgDesc()
		{
			compareResourceOEO("arrow/sortText1ArgDesc.poc");
		}

		[Test]
		public void testSortText2Args()
		{
			compareResourceOEO("arrow/sortText2Args.poc");
		}

		[Test]
		public void testSortText2ArgsDesc()
		{
			compareResourceOEO("arrow/sortText2ArgsDesc.poc");
		}

	}
}

