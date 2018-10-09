using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestAnnotations : BaseOParserTest
	{

		[Test]
		public void testCallback()
		{
			compareResourceOMO("annotations/callback.poc");
		}

		[Test]
		public void testCategory()
		{
			compareResourceOMO("annotations/category.poc");
		}

	}
}

