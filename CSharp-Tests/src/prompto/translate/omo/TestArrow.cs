using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestArrow : BaseOParserTest
	{

		[Test]
		public void testArrowArgument()
		{
			compareResourceOMO("arrow/arrowArgument.poc");
		}

		[Test]
		public void testFilterFromList()
		{
			compareResourceOMO("arrow/filterFromList.poc");
		}

		[Test]
		public void testFilterFromSet()
		{
			compareResourceOMO("arrow/filterFromSet.poc");
		}

		[Test]
		public void testSortCategory1Arg()
		{
			compareResourceOMO("arrow/sortCategory1Arg.poc");
		}

		[Test]
		public void testSortCategory2Args()
		{
			compareResourceOMO("arrow/sortCategory2Args.poc");
		}

		[Test]
		public void testSortText1Arg()
		{
			compareResourceOMO("arrow/sortText1Arg.poc");
		}

		[Test]
		public void testSortText1ArgDesc()
		{
			compareResourceOMO("arrow/sortText1ArgDesc.poc");
		}

		[Test]
		public void testSortText2Args()
		{
			compareResourceOMO("arrow/sortText2Args.poc");
		}

		[Test]
		public void testSortText2ArgsDesc()
		{
			compareResourceOMO("arrow/sortText2ArgsDesc.poc");
		}

	}
}

