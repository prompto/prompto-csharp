using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestIssues : BaseEParserTest
	{

		[Test]
		public void testMinimal()
		{
			compareResourceESE("issues/minimal.pec");
		}

	}
}

