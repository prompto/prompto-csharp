using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestComment : BaseEParserTest
	{

		[Test]
		public void testComment()
		{
			compareResourceEME("comment/comment.pec");
		}

	}
}

