using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestComment : BaseEParserTest
	{

		[Test]
		public void testComment()
		{
			compareResourceESE("comment/comment.pec");
		}

	}
}

