using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestInfer : BaseEParserTest
	{

		[Test]
		public void testInferList()
		{
			compareResourceEOE("infer/inferList.pec");
		}

	}
}

