using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestGreater : BaseOParserTest
	{

		[Test]
		public void testGtCharacter()
		{
			compareResourceOSO("greater/gtCharacter.poc");
		}

		[Test]
		public void testGtDate()
		{
			compareResourceOSO("greater/gtDate.poc");
		}

		[Test]
		public void testGtDateTime()
		{
			compareResourceOSO("greater/gtDateTime.poc");
		}

		[Test]
		public void testGtDecimal()
		{
			compareResourceOSO("greater/gtDecimal.poc");
		}

		[Test]
		public void testGteCharacter()
		{
			compareResourceOSO("greater/gteCharacter.poc");
		}

		[Test]
		public void testGteDate()
		{
			compareResourceOSO("greater/gteDate.poc");
		}

		[Test]
		public void testGteDateTime()
		{
			compareResourceOSO("greater/gteDateTime.poc");
		}

		[Test]
		public void testGteDecimal()
		{
			compareResourceOSO("greater/gteDecimal.poc");
		}

		[Test]
		public void testGteInteger()
		{
			compareResourceOSO("greater/gteInteger.poc");
		}

		[Test]
		public void testGteText()
		{
			compareResourceOSO("greater/gteText.poc");
		}

		[Test]
		public void testGteTime()
		{
			compareResourceOSO("greater/gteTime.poc");
		}

		[Test]
		public void testGtInteger()
		{
			compareResourceOSO("greater/gtInteger.poc");
		}

		[Test]
		public void testGtText()
		{
			compareResourceOSO("greater/gtText.poc");
		}

		[Test]
		public void testGtTime()
		{
			compareResourceOSO("greater/gtTime.poc");
		}

	}
}

