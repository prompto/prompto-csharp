// generated: 2015-07-05T23:01:01.287
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
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

