using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oo
{

	[TestFixture]
	public class TestCondition : BaseOParserTest
	{

		[Test]
		public void testEmbeddedIf()
		{
			compareResourceOO("condition/embeddedIf.poc");
		}

	}
}

