using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestArrow : BaseOParserTest
	{

		[Test]
		public void testSort1()
		{
			compareResourceOMO("arrow/sort1.poc");
		}

		[Test]
		public void testSort1_desc()
		{
			compareResourceOMO("arrow/sort1_desc.poc");
		}

		[Test]
		public void testSort2()
		{
			compareResourceOMO("arrow/sort2.poc");
		}

		[Test]
		public void testSort2_desc()
		{
			compareResourceOMO("arrow/sort2_desc.poc");
		}

	}
}

