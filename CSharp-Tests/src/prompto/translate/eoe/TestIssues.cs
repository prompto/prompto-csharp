// generated: 2015-07-05T23:01:01.286
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

	}
}

