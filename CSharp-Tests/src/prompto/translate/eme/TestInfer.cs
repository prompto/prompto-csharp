using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestInfer : BaseEParserTest
	{

		[Test]
		public void testInferDict()
		{
			compareResourceEME("infer/inferDict.pec");
		}

		[Test]
		public void testInferList()
		{
			compareResourceEME("infer/inferList.pec");
		}

		[Test]
		public void testInferSet()
		{
			compareResourceEME("infer/inferSet.pec");
		}

	}
}

