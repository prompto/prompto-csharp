using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestIssues : BaseEParserTest
	{

		[Test]
		public void testMinimal()
		{
			compareResourceEOE("issues/minimal.pec");
		}

		[Test]
		public void testWidget()
		{
			compareResourceEOE("issues/widget.pec");
		}

	}
}

