using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestArrow : BaseOParserTest
	{

		[Test]
		public void testSort1()
		{
			compareResourceOEO("arrow/sort1.poc");
		}

		[Test]
		public void testSort1_desc()
		{
			compareResourceOEO("arrow/sort1_desc.poc");
		}

		[Test]
		public void testSort2()
		{
			compareResourceOEO("arrow/sort2.poc");
		}

		[Test]
		public void testSort2_desc()
		{
			compareResourceOEO("arrow/sort2_desc.poc");
		}

	}
}

