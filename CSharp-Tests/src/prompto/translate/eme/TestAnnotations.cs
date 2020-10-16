using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestAnnotations : BaseEParserTest
	{

		[Test]
		public void testCallback()
		{
			compareResourceEME("annotations/callback.pec");
		}

		[Test]
		public void testCategory()
		{
			compareResourceEME("annotations/category.pec");
		}

		[Test]
		public void testInlined()
		{
			compareResourceEME("annotations/inlined.pec");
		}

	}
}

