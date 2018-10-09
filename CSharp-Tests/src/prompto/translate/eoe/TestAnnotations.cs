using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestAnnotations : BaseEParserTest
	{

		[Test]
		public void testCallback()
		{
			compareResourceEOE("annotations/callback.pec");
		}

		[Test]
		public void testCategory()
		{
			compareResourceEOE("annotations/category.pec");
		}

	}
}

