using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestJsx : BaseEParserTest
	{

		[Test]
		public void testChildElement()
		{
			compareResourceEME("jsx/childElement.pec");
		}

		[Test]
		public void testCodeAttribute()
		{
			compareResourceEME("jsx/codeAttribute.pec");
		}

		[Test]
		public void testCodeElement()
		{
			compareResourceEME("jsx/codeElement.pec");
		}

		[Test]
		public void testDotName()
		{
			compareResourceEME("jsx/dotName.pec");
		}

		[Test]
		public void testEmpty()
		{
			compareResourceEME("jsx/empty.pec");
		}

		[Test]
		public void testEmptyAttribute()
		{
			compareResourceEME("jsx/emptyAttribute.pec");
		}

		[Test]
		public void testFragment()
		{
			compareResourceEME("jsx/fragment.pec");
		}

		[Test]
		public void testHyphenName()
		{
			compareResourceEME("jsx/hyphenName.pec");
		}

		[Test]
		public void testLiteralAttribute()
		{
			compareResourceEME("jsx/literalAttribute.pec");
		}

		[Test]
		public void testNonAsciiTextElement()
		{
			compareResourceEME("jsx/nonAsciiTextElement.pec");
		}

		[Test]
		public void testSelfClosingDiv()
		{
			compareResourceEME("jsx/selfClosingDiv.pec");
		}

		[Test]
		public void testSelfClosingEmptyAttribute()
		{
			compareResourceEME("jsx/selfClosingEmptyAttribute.pec");
		}

		[Test]
		public void testTextElement()
		{
			compareResourceEME("jsx/textElement.pec");
		}

	}
}

