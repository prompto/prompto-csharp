using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestComment : BaseEParserTest
	{

		[Test]
		public void testComment()
		{
			compareResourceEOE("comment/comment.pec");
		}

	}
}

