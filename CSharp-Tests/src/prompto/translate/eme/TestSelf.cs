using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestSelf : BaseEParserTest
	{

		[Test]
		public void testSelfAsParameter()
		{
			compareResourceEME("self/selfAsParameter.pec");
		}

	}
}

