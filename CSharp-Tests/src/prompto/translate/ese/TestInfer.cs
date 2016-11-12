using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestInfer : BaseEParserTest
	{

		[Test]
		public void testInferList()
		{
			compareResourceESE("infer/inferList.pec");
		}

	}
}

