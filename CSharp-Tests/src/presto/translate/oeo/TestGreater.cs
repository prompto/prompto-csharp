using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestGreater : BaseOParserTest
	{

		[Test]
		public void testGtCharacter()
		{
			compareResourceOEO("greater/gtCharacter.poc");
		}

		[Test]
		public void testGtDate()
		{
			compareResourceOEO("greater/gtDate.poc");
		}

		[Test]
		public void testGtDateTime()
		{
			compareResourceOEO("greater/gtDateTime.poc");
		}

		[Test]
		public void testGtDecimal()
		{
			compareResourceOEO("greater/gtDecimal.poc");
		}

		[Test]
		public void testGteCharacter()
		{
			compareResourceOEO("greater/gteCharacter.poc");
		}

		[Test]
		public void testGteDate()
		{
			compareResourceOEO("greater/gteDate.poc");
		}

		[Test]
		public void testGteDateTime()
		{
			compareResourceOEO("greater/gteDateTime.poc");
		}

		[Test]
		public void testGteDecimal()
		{
			compareResourceOEO("greater/gteDecimal.poc");
		}

		[Test]
		public void testGteInteger()
		{
			compareResourceOEO("greater/gteInteger.poc");
		}

		[Test]
		public void testGteText()
		{
			compareResourceOEO("greater/gteText.poc");
		}

		[Test]
		public void testGteTime()
		{
			compareResourceOEO("greater/gteTime.poc");
		}

		[Test]
		public void testGtInteger()
		{
			compareResourceOEO("greater/gtInteger.poc");
		}

		[Test]
		public void testGtText()
		{
			compareResourceOEO("greater/gtText.poc");
		}

		[Test]
		public void testGtTime()
		{
			compareResourceOEO("greater/gtTime.poc");
		}

	}
}

