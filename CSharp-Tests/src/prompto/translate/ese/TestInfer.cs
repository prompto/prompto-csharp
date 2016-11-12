using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestInfer : BaseEParserTest
	{

		[Test]
		public void testInferDict()
		{
			compareResourceESE("infer/inferDict.pec");
		}

		[Test]
		public void testInferList()
		{
			compareResourceESE("infer/inferList.pec");
		}

		[Test]
		public void testInferSet()
		{
			compareResourceESE("infer/inferSet.pec");
		}

	}
}

