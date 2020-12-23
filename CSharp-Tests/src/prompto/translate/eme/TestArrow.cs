using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestArrow : BaseEParserTest
	{

		[Test]
		public void testHasAllFromList()
		{
			compareResourceEME("arrow/hasAllFromList.pec");
		}

		[Test]
		public void testHasAllFromSet()
		{
			compareResourceEME("arrow/hasAllFromSet.pec");
		}

		[Test]
		public void testHasAnyFromList()
		{
			compareResourceEME("arrow/hasAnyFromList.pec");
		}

		[Test]
		public void testHasAnyFromSet()
		{
			compareResourceEME("arrow/hasAnyFromSet.pec");
		}

	}
}

