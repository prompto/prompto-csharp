using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestIssues : BaseEParserTest
	{

		[Test]
		public void testMinimal()
		{
			compareResourceEOE("issues/minimal.pec");
		}

	}
}

