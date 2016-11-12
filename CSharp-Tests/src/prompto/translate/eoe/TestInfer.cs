using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestInfer : BaseEParserTest
	{

		[Test]
		public void testInferDict()
		{
			compareResourceEOE("infer/inferDict.pec");
		}

		[Test]
		public void testInferList()
		{
			compareResourceEOE("infer/inferList.pec");
		}

		[Test]
		public void testInferSet()
		{
			compareResourceEOE("infer/inferSet.pec");
		}

	}
}

