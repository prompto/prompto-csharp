using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestArrow : BaseEParserTest
	{

		[Test]
		public void testHasAllFromList()
		{
			compareResourceEOE("arrow/hasAllFromList.pec");
		}

		[Test]
		public void testHasAllFromSet()
		{
			compareResourceEOE("arrow/hasAllFromSet.pec");
		}

		[Test]
		public void testHasAnyFromList()
		{
			compareResourceEOE("arrow/hasAnyFromList.pec");
		}

		[Test]
		public void testHasAnyFromSet()
		{
			compareResourceEOE("arrow/hasAnyFromSet.pec");
		}

	}
}

