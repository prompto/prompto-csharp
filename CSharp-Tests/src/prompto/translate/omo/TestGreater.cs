using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestGreater : BaseOParserTest
	{

		[Test]
		public void testGtCharacter()
		{
			compareResourceOMO("greater/gtCharacter.poc");
		}

		[Test]
		public void testGtDate()
		{
			compareResourceOMO("greater/gtDate.poc");
		}

		[Test]
		public void testGtDateTime()
		{
			compareResourceOMO("greater/gtDateTime.poc");
		}

		[Test]
		public void testGtDecimal()
		{
			compareResourceOMO("greater/gtDecimal.poc");
		}

		[Test]
		public void testGteCharacter()
		{
			compareResourceOMO("greater/gteCharacter.poc");
		}

		[Test]
		public void testGteDate()
		{
			compareResourceOMO("greater/gteDate.poc");
		}

		[Test]
		public void testGteDateTime()
		{
			compareResourceOMO("greater/gteDateTime.poc");
		}

		[Test]
		public void testGteDecimal()
		{
			compareResourceOMO("greater/gteDecimal.poc");
		}

		[Test]
		public void testGteInteger()
		{
			compareResourceOMO("greater/gteInteger.poc");
		}

		[Test]
		public void testGteText()
		{
			compareResourceOMO("greater/gteText.poc");
		}

		[Test]
		public void testGteTime()
		{
			compareResourceOMO("greater/gteTime.poc");
		}

		[Test]
		public void testGtInteger()
		{
			compareResourceOMO("greater/gtInteger.poc");
		}

		[Test]
		public void testGtText()
		{
			compareResourceOMO("greater/gtText.poc");
		}

		[Test]
		public void testGtTime()
		{
			compareResourceOMO("greater/gtTime.poc");
		}

	}
}

